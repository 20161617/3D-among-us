using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using static UIManager;
using static DatabaseManager;

public class PlayerScript : MonoBehaviourPunCallbacks
{
    public bool isImposter = false;
    public bool isAlive = true;
    public bool isDetected = false;
    public SkinnedMeshRenderer color;

    public int colorIndex = -1;
    public string nickName;

    public PhotonView PV;
    public TargetCtrl targetCtrl;
<<<<<<< HEAD
    public TwoDimmentionalAnimationStateController playerAnimation;

=======
    bool waitRoom = false;
    bool isCreateMisson = false;
    bool isReady = false;
>>>>>>> New-메인
    // Start is called before the first frame update
    private void OnEnable()
    {
        if (waitRoom)
        {
            if (!isImposter)
            {
                if (PV.IsMine)
                { 
                    transform.GetComponent<PlayerMission>().createMission();
                    isCreateMisson = true;
                }
                else
                {
                    isCreateMisson = false;
                    isReady = true;
                }
            }
        }
        waitRoom = true;
    }

    void Awake()
    {
       
        PV = photonView;

        color = gameObject.transform.Find("Beta_Surface").GetComponent<SkinnedMeshRenderer>();

        nickName = photonView.Owner.NickName;

        targetCtrl = gameObject.GetComponent<TargetCtrl>();

        playerAnimation = gameObject.GetComponent<TwoDimmentionalAnimationStateController>();

        databaseManager.Players.Add(this);

        DontDestroyOnLoad(gameObject);
      
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine) return;
        if(isReady&&!isCreateMisson&&!isImposter)
        {
            transform.GetComponent<PlayerMission>().createMission();
            isCreateMisson = true;
        }
       
    }

    void OnDestroy()
    {
        databaseManager.Players.Remove(this);
    }

    public void KillingPlayer()
    {

        PV.RPC("DieRPC", RpcTarget.AllViaServer);
    }

    public void OnDetected()
    {
        isDetected = true;
    }
    public void OffDetected()
    {
        isDetected = false;
    }

    [PunRPC]
    void HideCharacter()
    {
        Debug.Log("플레이어 비활성화!");
        gameObject.SetActive(false);
    }

    [PunRPC]
    void SetImpoCrew(bool _isImposter)
    {
        isImposter = _isImposter;
    }

    [PunRPC]
    public void SetColor(int _colorIndex)
    {
        color.material = databaseManager.Colors[_colorIndex];
        colorIndex = _colorIndex;
    }


    [PunRPC]
    void ShowCharacter()
    {
        gameObject.tag = "INTERACTION";
        gameObject.SetActive(true);
    }

    [PunRPC]
    void SetMyPosition(Vector3 postion)
    {
        gameObject.transform.position = postion;
    }

    [PunRPC]
    void DieRPC()
    {
        gameObject.tag = "DEAD";
        isAlive = false;
        playerAnimation.OnDeath();
    }
    [PunRPC]
    void KillRPC()
    {
        playerAnimation.OnKill();
    }
}

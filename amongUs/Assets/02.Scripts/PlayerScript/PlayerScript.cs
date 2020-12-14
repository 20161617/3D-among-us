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

    PhotonView PV;
    public TargetCtrl targetCtrl;
    public TwoDimmentionalAnimationStateController playerAnimation;
    public PlayerMission CurrentMyMission;

    bool waitRoom = false;
    bool isCreateMisson = false;
    bool isReady = false;
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
            else
            {
                int imposterCount = DatabaseManager.databaseManager.Players.Count <= 5 ? 1 : 2; //임포수 5명이하면 1빼기 이상이면 2빼기 
                //게이지 최대 100이라고 봤을떄  미션최대게이지/ 플레이어 수 - 임포수 / 미션수 
                MissionManager.Instance.plusGague = (1.0f / (DatabaseManager.databaseManager.Players.Count - imposterCount)) / (MissionManager.Instance.commonMissionNum + MissionManager.Instance.simpleMissionNum + MissionManager.Instance.difficultMissionNum);
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

        CurrentMyMission = gameObject.GetComponent<PlayerMission>();

        databaseManager.Players.Add(this);

        DontDestroyOnLoad(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine) return;
        if (isReady && !isCreateMisson && !isImposter)
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

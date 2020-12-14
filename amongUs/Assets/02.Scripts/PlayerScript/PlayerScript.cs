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
    public bool isImposter;
    public SkinnedMeshRenderer color;

    public int colorIndex = -1;
    public string nickName;

    public PhotonView PV;
    public TargetCtrl targetCtrl;
    bool waitRoom = false;

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
                }
                else
                {

                }
            }
            else
            {
                if(MissionManager.Instance.plusGague==0.0f) 
                {     //미션 게이지를 받지 못했을떄  인원수에 맞춰 미션 게이지 세팅 
                    int imposterCount = DatabaseManager.databaseManager.Players.Count <= 5 ? 1 : 2;                                                                                       
                    MissionManager.Instance.plusGague = (1.0f / (DatabaseManager.databaseManager.Players.Count - imposterCount)) / (MissionManager.Instance.commonMissionNum + MissionManager.Instance.simpleMissionNum + MissionManager.Instance.difficultMissionNum);
                    //Debug.LogError("미션 게이지 충전 전에 :"+MissionManager.Instance.plusGague);
                    //Debug.LogError("Imposter 일떄  : player " + DatabaseManager.databaseManager.Players.Count);
                    //Debug.LogError("미션 게이지 충전 후에 :" + MissionManager.Instance.plusGague);
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
        
        databaseManager.Players.Add(this);

        DontDestroyOnLoad(gameObject);
      
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine) return;
    }

    void OnDestroy()
    {
        databaseManager.Players.Remove(this);
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
        gameObject.SetActive(true);
    }

    [PunRPC]
    void SetMyPosition(Vector3 postion)
    {
        gameObject.transform.position = postion;
    }
}

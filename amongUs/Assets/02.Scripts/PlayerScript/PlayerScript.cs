using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using static UIManager;
using static DatabaseManager;

public enum PLAYER_STATE
{
    ALIVE,
    DEAD
}

public class PlayerScript : MonoBehaviourPunCallbacks
{
    public bool isImposter = false;
    public bool isAlive = true;
    public bool isDetected = false;
    public SkinnedMeshRenderer color;

    public int colorIndex = -1;
    public string nickName;

    public PLAYER_STATE playerState;

    PhotonView PV;
    public TargetCtrl targetCtrl;
    public TwoDimmentionalAnimationStateController playerAnimation;
    public PlayerMission CurrentMyMission;

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
                    Debug.Log("초기화 실행 ");

                }
                else
                {

                }
            }
            else
            {
                if (MissionManager.Instance.plusGague == 0.0f)
                {     //미션 게이지를 받지 못했을떄  인원수에 맞춰 미션 게이지 세팅 
                    int imposterCount = DatabaseManager.databaseManager.Players.Count <= 5 ? 1 : 2;
                    MissionManager.Instance.plusGague = (1.0f / (DatabaseManager.databaseManager.Players.Count - imposterCount)) / (MissionManager.Instance.commonMissionNum + MissionManager.Instance.simpleMissionNum + MissionManager.Instance.difficultMissionNum);
                    Debug.Log("미션 게이지 실행");

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

        playerState = PLAYER_STATE.ALIVE;

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
    }

    void OnDestroy()
    {
        databaseManager.Players.Remove(this);

        //임시 작용
        if (playerState == PLAYER_STATE.DEAD) photonView.RPC("HideCharacter", RpcTarget.AllViaServer);
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

    public void SetState(PLAYER_STATE playerState)
    {
        this.playerState = playerState;
    }
}

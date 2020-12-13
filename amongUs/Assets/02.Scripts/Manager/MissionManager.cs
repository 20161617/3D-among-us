using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using static PhotonInit;
using static MissionList;


public class MissionManager : MonoBehaviourPun
{
    public static MissionManager Instance;

    PhotonView PV;


    public RectTransform clearText = null;
    public Image missionGauge;

    float missionBarFill { get; set; } //미션게이지 최소 0 최대 1 
    const float MIN_BAR = 0.0f;
    const float MAX_BAR = 1.0f;

    //임무에 해당하는 미니게임들
    public List<GameObject> CommonMissionGame = new List<GameObject>();
    public List<GameObject> SimpleMissionGame = new List<GameObject>();
    public List<GameObject> DifficultMissionGame = new List<GameObject>();

    //현재 임무를 저장하는 리스트
    //초기에 정해준 Num의 수만큼 저장된다
    List<int> CommonMission = new List<int>();
    List<int> SimpleMission = new List<int>();
    List<int> DifficultMission = new List<int>();

    //갯수
    int commonMissionNum { get; set; } //공통임무
    int simpleMissionNum { get; set; } //단순임무
    int difficultMissionNum { get; set; } //복잡임무 


    private void Awake()
    {

        PV = photonView;
        Instance = this;

    }
    private void Start()
    {
        MissionAllocation();
    }


    public void MissionClear(GameObject _object) // 미션을꺳을떄 
    {
        StartCoroutine(MissionClearText(_object));
        // PV.RPC("testFill", RpcTarget.AllViaServer);
    }

    private IEnumerator MissionClearText(GameObject _object) //텍스트 올라왔다 사라지는거 
    {
        float upText = clearText.anchoredPosition.y;
        while (!(clearText.anchoredPosition.y > 0))
        {
            upText += 5;
            clearText.anchoredPosition = new Vector3(0, upText, 0);
            yield return new WaitForSeconds(0.001f);
        }
        StartCoroutine(setMission_End(_object));
        PV_GaugeFill(_object);
        yield return null;
    }

    public void PV_GaugeFill(GameObject _object)
    {
        // getGagueFill = _object.GetComponent<Gague>().setGague * 0.01f;
        PV.RPC("MissionClearGauge", RpcTarget.AllViaServer);
    }

    [PunRPC]
    IEnumerator MissionClearGauge()
    {
        missionGauge.fillAmount += 0.25f;
        // getGagueFill = 0f;

        yield return null;
    }


    private IEnumerator setMission_End(GameObject _object)
    {

        Vector2 setPos = new Vector2(0, -600f);
        clearText.anchoredPosition = setPos;
        Debug.Log("텍스트 위치 초기화 ");
        _object.SetActive(false);
        yield return null;
    }


    public void MissionAllocation() //게임시작시 미션 갯수만큼 임무 분배
    {
        //공통임무 배정
        for (int i = 0; i < commonMissionNum; i++)
        {
            //                                                           ↓Enum에 들어가있는 아이템의 총 갯수
            int RandCommonMission = Random.Range(1, System.Enum.GetValues(typeof(MissionList.COMMON_MISSIONLIST)).Length);
            CommonMission.Add(RandCommonMission);
        }

        //단순임무 배정
        //현재는 테스트를 위해 의도적으로 2를 기입함.
        //나중에 주석처리한 부분으로 사용할 것
        SimpleMission.Add(2);
        /*
        for (int i = 0; i < simpleMissionNum; i++)
        {
            int RandSimpleMission = Random.Range(1, System.Enum.GetValues(typeof(MissionList.SIMPLE_MISSIONLIST)).Length);
            SimpleMission.Add(RandSimpleMission);
        }
        */

        //복잡임무 배정
        for (int i = 0; i < difficultMissionNum; i++)
        {
            int RandDifficultMission = Random.Range(1, System.Enum.GetValues(typeof(MissionList.DIFFUCLT_MISSIONLIST)).Length);
            DifficultMission.Add(RandDifficultMission);
        }
    }

    public void MissionNumInit() //게임시작시 미션 갯수 설정  별도로 원할시 미션 get set 으로 불러와서 지정 
    {
        commonMissionNum = 1;
        simpleMissionNum = 1;
        difficultMissionNum = 2;
    }
    // Start is called before the first frame update

    void EndMission() //미션게이지 다채웠을떄 호출 
    {
        if (missionBarFill == MAX_BAR)
        {
            //call GameManager()->victory  
            Debug.Log("mission Complete");
        }
    }

    void MissionUpdate() //GameManager 에서 MissionUpdate 호출해서 실행 
    {
        EndMission();
    }

    //미션을 MissionType(미션종류) 중, MissionNumber(미션번호)에 맞는 미션을 불러온다.
    public void CallMission(string MissionType, int MissionNumber)
    {
        if (MissionType == "CommonMission")
        {
            // 미션넘버에 -1을 해주는 이유는 배열의 번호는 0부터 시작하지만,
            // 미션넘버는 1부터 시작하기 때문
            CommonMissionGame[MissionNumber - 1].SetActive(true);
        }
        if (MissionType == "SimpleMission")
        {
            SimpleMissionGame[MissionNumber - 1].SetActive(true);
        }
        if (MissionType == "DifficultMission")
        {
            DifficultMissionGame[MissionNumber - 1].SetActive(true);
        }
    }

    //해당하는 임무가 현재 임무에 포함되어있는지 검사
    public bool ContainsMission(string MissionType, int MissionNumber)
    {
        //있으면 true, 없으면 false를 반환
        if (MissionType == "CommonMission")
        {
            return CommonMission.Contains(MissionNumber);
        }
        if (MissionType == "SimpleMission")
        {
            return SimpleMission.Contains(MissionNumber);
        }
        if (MissionType == "DifficultMission")
        {
            return DifficultMission.Contains(MissionNumber);
        }

        return false;
    }
}

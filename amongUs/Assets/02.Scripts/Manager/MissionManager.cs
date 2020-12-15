﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using static PhotonInit;
using static MissionList;
using static DatabaseManager;


public class MissionManager : MonoBehaviourPun
{
    public static MissionManager missionManager;

    PhotonView PV;


    public RectTransform clearText = null;
    public Image missionGauge;


    public List<GameObject> MissionObject = new List<GameObject>();

    List<MissionData> CommonMissionList = new List<MissionData>();
    List<MissionData> SimpleMissionList = new List<MissionData>();
    List<MissionData> DifficultMissionList = new List<MissionData>();


    float missionBarFill { get; set; } //미션게이지 최소 0 최대 1 
    const float MIN_BAR = 0.0f;
    const float MAX_BAR = 1.0f;

    public float plusGague;

    //임무에 해당하는 미니게임들
    public List<GameObject> CommonMissionGame = new List<GameObject>();
    public List<GameObject> SimpleMissionGame = new List<GameObject>();
    public List<GameObject> DifficultMissionGame = new List<GameObject>();
    public GameObject ReportGame;

    MissionData CurrentMissionData;

    //현재 임무를 저장하는 리스트
    //초기에 정해준 Num의 수만큼 저장된다
    public List<MissionData> CommonMission = new List<MissionData>();
    public List<MissionData> SimpleMission = new List<MissionData>();
    public List<MissionData> DifficultMission = new List<MissionData>();

    //// public List<GameObject> HaveMission = new List<GameObject>(); //맵에 보유 미션 
    //public GameObject clearObject { get; set; }
    //public List<GameObject> myMission = new List<GameObject>(); //자신의 미션 
    //public Text displayText;


    public const string MissionCommon = "CommonMission";
    public const string MissionSimple = "SimpleMission";
    public const string MissionDifficult = "DifficultMission";


    //갯수
    public int commonMissionNum { get; set; } //공통임무
    public int simpleMissionNum { get; set; } //단순임무
    public int difficultMissionNum { get; set; } //복잡임무 

    private void Awake()
    {
        PV = photonView;
        missionManager = this;
        MissionNumInit();

        databaseManager.MyPlayer.CurrentMyMission.FindDisplayText();
    }
    private void Start()
    {
        MissionListInit();
        MissionAllocation();
    }

    public void MissionClear(GameObject _object) // 미션을꺳을떄 
    {
        MissionRemove();
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
            Debug.Log("텍스트 올리기");
        }
        StartCoroutine(setMission_End(_object));
        PV_GaugeFill(_object);
        yield return null;
    }

    public void PV_GaugeFill(GameObject _object)
    {
        // getGagueFill = _object.GetComponent<Gague>().setGague * 0.01f;
        //myMission.Remove(clearObject);
        PV.RPC("MissionClearGauge", RpcTarget.AllViaServer);
    }

    [PunRPC]
    IEnumerator MissionClearGauge()
    {
        missionGauge.fillAmount += plusGague;
        Debug.Log("게이지 올리기 ");
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
        int imposterCount = DatabaseManager.databaseManager.Players.Count <= 5 ? 1 : 2; //임포수 5명이하면 1빼기 이상이면 2빼기 
                                                                                        //게이지 최대 100이라고 봤을떄  미션최대게이지/ 플레이어 수 - 임포수 / 미션수 
        plusGague = (1.0f / (DatabaseManager.databaseManager.Players.Count - imposterCount)) / (commonMissionNum + simpleMissionNum + difficultMissionNum);


        //공통임무 배정
        for (int i = 0; i < commonMissionNum; i++)
        {
            int RandCommonMission = Random.Range(1, CommonMissionList.Count);
            while (CommonMission.Contains(CommonMissionList[RandCommonMission - 1]))
            {
                RandCommonMission = Random.Range(1, CommonMissionList.Count);
            }

            CommonMission.Add(CommonMissionList[RandCommonMission - 1]);
        }

        //단순임무 배정
        for (int i = 0; i < simpleMissionNum; i++)
        {
            int RandSimpleMission = Random.Range(1, SimpleMissionList.Count);

            while (SimpleMission.Contains(SimpleMissionList[RandSimpleMission - 1]))
            {
                RandSimpleMission = Random.Range(1, SimpleMissionList.Count);
            }

            SimpleMission.Add(SimpleMissionList[RandSimpleMission - 1]);
        }

        //복잡임무 배정
        for (int i = 0; i < difficultMissionNum; i++)
        {
            int RandDifficultMission = Random.Range(1, DifficultMissionList.Count);

            while (DifficultMission.Contains(DifficultMissionList[RandDifficultMission - 1]))
            {
                RandDifficultMission = Random.Range(1, DifficultMissionList.Count);
            }

            DifficultMission.Add(DifficultMissionList[RandDifficultMission - 1]);
        }
    }

    void MissionListInit()
    {
        //미션종류에 따른 미션리스트를 초기화
        for (int i = 0; i < MissionObject.Count; i++)
        {
            MissionData TempMissionData = MissionObject[i].GetComponent<MissionData>();
            string TempMissionType = TempMissionData.MissionType;

            if (TempMissionType == "CommonMission")
            {
                CommonMissionList.Add(TempMissionData);
            }

            if (TempMissionType == "SimpleMission")
            {
                SimpleMissionList.Add(TempMissionData);
            }

            if (TempMissionType == "DifficultMission")
            {
                DifficultMissionList.Add(TempMissionData);
            }
        }
    }

    void MissionRemove()
    {
        string MissionType = CurrentMissionData.MissionType;

        if (MissionType == "CommonMission")
        {
            CommonMission.Remove(CurrentMissionData);
        }
        if (MissionType == "SimpleMission")
        {
            SimpleMission.Remove(CurrentMissionData);
        }
        if (MissionType == "DifficultMission")
        {
            DifficultMission.Remove(CurrentMissionData);
        }
    }

    public void MissionNumInit() //게임시작시 미션 갯수 설정  별도로 원할시 미션 get set 으로 불러와서 지정 
    {
        commonMissionNum = 1;
        simpleMissionNum = 5;
        difficultMissionNum = 4;
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
    public void CallMission(MissionData TargetMissionData)
    {
        CurrentMissionData = TargetMissionData;

        string MissionType = CurrentMissionData.MissionType;
        int MissionNumber = CurrentMissionData.MissionNumber;

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
        if (MissionType == "EmergencyCall")
        {
            ReportGame.GetComponent<GotoVoteManager>().photonView.RPC("ActivePanel", RpcTarget.AllViaServer, MissionNumber);
        }
    }

    //해당하는 임무가 현재 임무에 포함되어있는지 검사
    public bool ContainsMission(MissionData TargetMissionData, bool isImposter)
    {
        //있으면 true, 없으면 false를 반환
        if (TargetMissionData.MissionType == "CommonMission" && !isImposter)
        {
            return CommonMission.Contains(TargetMissionData);
        }
        if (TargetMissionData.MissionType == "SimpleMission" && !isImposter)
        {
            return SimpleMission.Contains(TargetMissionData);
        }
        if (TargetMissionData.MissionType == "DifficultMission" && !isImposter)
        {
            return DifficultMission.Contains(TargetMissionData);
        }
        if (TargetMissionData.MissionType == "EmergencyCall")
        {
            return true;
        }
        if (TargetMissionData.MissionType == "PLAYER" && isImposter)
        {
            return true;
        }
        return false;
    }



    public string GetInformation(string missionKind, int number)
    {
        string text = "";
        if (missionKind == MissionManager.MissionCommon)
        {
            switch (number)
            {
                case (int)COMMON_MISSIONLIST.ELECTRICFIX:
                    break;
                case (int)COMMON_MISSIONLIST.SCRATCHINGCARD:
                    text = " 카드 긁기 ";
                    break;

            }
        }
        else if (missionKind == MissionManager.MissionSimple)
        {
            switch (number)
            {
                case (int)SIMPLE_MISSIONLIST.SWITCH:
                    text = "스위치 올리기";
                    break;
                case (int)SIMPLE_MISSIONLIST.NAVIGATION:
                    text = "항로 조정하기";
                    break;
                case (int)SIMPLE_MISSIONLIST.DOWNLOADING:
                    text = "파일 다운로드 하기";
                    break;
            }

        }
        else if (missionKind == MissionManager.MissionDifficult)
        {
            switch (number)
            {
                case (int)DIFFUCLT_MISSIONLIST.SHOOTING:
                    text = "행성 파괴하기";
                    break;
                case (int)DIFFUCLT_MISSIONLIST.TRASHING:
                    text = "쓰레기 버리기";
                    break;
                case (int)DIFFUCLT_MISSIONLIST.DNASEARCHING:
                    text = "DNA 수집하세요";
                    break;
                case (int)DIFFUCLT_MISSIONLIST.DISTRIBUTOR:
                    text = "전기를 안정화 시키세요";
                    break;

            }
        }
        return text;
    }
}

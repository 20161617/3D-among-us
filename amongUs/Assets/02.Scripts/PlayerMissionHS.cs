using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using static MissionManager;

public class PlayerMissionHS : MonoBehaviourPun
{
    PhotonView PV;

    public Text displayText;

    List<MissionData> MyCommonMission = new List<MissionData>();
    List<MissionData> MySimpleMission = new List<MissionData>();
    List<MissionData> MyDifficultMission = new List<MissionData>();

    public bool GameStart = false;

    void Start()
    {
        PV = photonView;
    }

    void Update()
    {
        if (!PV.IsMine)
            return;
        if (!GameStart)
            return;

        UpdateMissionList();
        DisplayText();
    }

    public void FindDisplayText()
    {
        displayText = GameObject.FindWithTag("MissionText").GetComponent<Text>();

    }

    public void UpdateMissionList()
    {
        MyCommonMission = missionManager.CommonMission;
        MySimpleMission = missionManager.SimpleMission;
        MyDifficultMission = missionManager.DifficultMission;
    }

    public void DisplayText()
    {
        displayText.text = " 임무 목록 \n";
        for (int i = 0; i < MyCommonMission.Count; i++)
        {
            displayText.text += MyCommonMission[i].MissionPlace + " 에서 " + missionManager.GetInformation(MyCommonMission[i].MissionType, MyCommonMission[i].MissionNumber) + "\n";
        }
        for (int i = 0; i < MySimpleMission.Count; i++)
        {
            displayText.text += MySimpleMission[i].MissionPlace + " 에서 " + missionManager.GetInformation(MySimpleMission[i].MissionType, MySimpleMission[i].MissionNumber) + "\n";
        }
        for (int i = 0; i < MyDifficultMission.Count; i++)
        {
            displayText.text += MyDifficultMission[i].MissionPlace + " 에서 " + missionManager.GetInformation(MyDifficultMission[i].MissionType, MyDifficultMission[i].MissionNumber) + "\n";
        }
    }
}

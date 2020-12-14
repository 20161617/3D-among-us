using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMission : MonoBehaviour
{
    public List<GameObject> myMission = new List<GameObject>(); //자신의 미션 
    public Text displayText;

    public void DisplayText()
    {
        if (myMission.Count <= 0)
            return;
        displayText.text = " 임무 목록 \n";
        for (int i = 0; i < myMission.Count; i++)
        {
            displayText.text += myMission[i].GetComponent<MissionData>().MissionPlace + " 에서 " + MissionManager.Instance.GetInformation(myMission[i].GetComponent<MissionData>().MissionType,
                myMission[i].GetComponent<MissionData>().MissionNumber) + "\n";
        }


    }
    public void createMission()
    {
        
        Debug.Log("create");
<<<<<<< Updated upstream
        displayText = GameObject.FindWithTag("MissionText").GetComponent<Text>();
        int imposterCount = DatabaseManager.databaseManager.Players.Count <= 5 ? 1 : 2; //임포수 5명이하면 1빼기 이상이면 2빼기 
=======
        int imposterCount = DatabaseManager.databaseManager.Players.Count <= 5 ? 1 : 2; //임포수 5명이하면 1빼기 이상이면 2빼기 
         displayText = GameObject.FindWithTag("MissionText").GetComponent<Text>();
>>>>>>> Stashed changes

        ////게이지 최대 100이라고 봤을떄  미션최대게이지/ 플레이어 수 - 임포수 / 미션수 
        MissionManager.Instance.plusGague = (1.0f / (DatabaseManager.databaseManager.Players.Count - imposterCount)) / (MissionManager.Instance.commonMissionNum + MissionManager.Instance.simpleMissionNum + MissionManager.Instance.difficultMissionNum);
        GameObject[] HaveMission = GameObject.FindGameObjectsWithTag("INTERACTION");


        for (int i = 0; i < HaveMission.Length; i++) //설정대로 미션을 돌려줌 
        {
            int x = Random.Range(0, HaveMission.Length);     
            if (MissionManager.Instance.commonMissionNum <= 0 && MissionManager.Instance.simpleMissionNum <= 0 && MissionManager.Instance.difficultMissionNum <= 0)
            {
                DisplayText();
                break;
            }

            if (myMission.Contains(HaveMission[x]))
                continue;

            if (HaveMission[x].GetComponent<MissionData>().MissionType == MissionManager.MissionCommon && MissionManager.Instance.commonMissionNum > 0)
            {

               
                myMission.Add(HaveMission[x]);
                MissionManager.Instance.commonMissionNum--;
            }
            else if (HaveMission[x].GetComponent<MissionData>().MissionType == MissionManager.MissionSimple && MissionManager.Instance.simpleMissionNum > 0)
            {
                myMission.Add(HaveMission[x]);
                MissionManager.Instance.simpleMissionNum--;
            }
            else if (HaveMission[x].GetComponent<MissionData>().MissionType == MissionManager.MissionDifficult && MissionManager.Instance.difficultMissionNum > 0)
            {
                myMission.Add(HaveMission[x]);
                MissionManager.Instance.difficultMissionNum--;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DisplayText();
    }
}

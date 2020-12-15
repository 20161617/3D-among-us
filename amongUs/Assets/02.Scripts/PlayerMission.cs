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

        Debug.LogError("createMission : ");
        int imposterCount = DatabaseManager.databaseManager.Players.Count <= 5 ? 1 : 2; //임포수 5명이하면 1빼기 이상이면 2빼기 
         displayText = GameObject.FindWithTag("MissionText").GetComponent<Text>();

        //게이지 최대 100이라고 봤을떄  미션최대게이지/ 플레이어 수 - 임포수 / 미션수 
        MissionManager.Instance.plusGague = (1.0f / (DatabaseManager.databaseManager.Players.Count - imposterCount)) / (MissionManager.Instance.commonMissionNum + MissionManager.Instance.simpleMissionNum + MissionManager.Instance.difficultMissionNum);
        Debug.LogError(MissionManager.Instance.commonMissionNum + MissionManager.Instance.simpleMissionNum + MissionManager.Instance.difficultMissionNum);

        //INTERACTION이 붙은 모든 게임오브젝트를 HaveMission에 저장
        GameObject[] HaveMission = GameObject.FindGameObjectsWithTag("INTERACTION");


        // HaveMission의 길이 만큼 반복
        for (int i = 0; i < HaveMission.Length; i++) //설정대로 미션을 돌려줌 
        {
            //0부터 HavaMission의 길이 사이의 값을 랜덤으로 지정
            int x = Random.Range(0, HaveMission.Length);
            //미션매니저의 공통미션, 단순미션, 복잡미션의 수가 0보다 작거나 같다면
            if (MissionManager.Instance.commonMissionNum <= 0 && MissionManager.Instance.simpleMissionNum <= 0 && MissionManager.Instance.difficultMissionNum <= 0)
            {
                //DiplayText를 호출하고 반복문을 빠져나감
                DisplayText();
                break;
            }

            //myMission에 HaveMission의 x(랜덤값)번째 값이 있는지 조사
            //있다면 반복문을 한차례 지나감
            if (myMission.Contains(HaveMission[x]))
                continue;

            //x번째 HaveMission의 미션타입과 미션매니저의 공통미션이 같고, 미션매니저의 공통임무 갯수가 0보다 크다면
            if (HaveMission[x].GetComponent<MissionData>().MissionType == MissionManager.MissionCommon && MissionManager.Instance.commonMissionNum > 0)
            {

                //myMission에 x번째 HavaMission을 추가
                //미션매니저의 공통임무 갯수를 줄인다.
                myMission.Add(HaveMission[x]);
                MissionManager.Instance.commonMissionNum--;
            }
            //x번째 HavaMission의 미션타입과 미션매니저의 단순미션이 같고, 미션매니저의 단순임무 갯수가 0보다 크다면
            else if (HaveMission[x].GetComponent<MissionData>().MissionType == MissionManager.MissionSimple && MissionManager.Instance.simpleMissionNum > 0)
            {

                //myMission에 x번째 HavaMission을 추가
                //미션매니저의 단순임무 갯수를 줄인다.
                myMission.Add(HaveMission[x]);
                MissionManager.Instance.simpleMissionNum--;
            }
            //x번째 HavaMission의 미션타입과 미션매니저의 복잡미션이 같고, 미션매니저의 복잡임무 갯수가 0보다 크다면
            else if (HaveMission[x].GetComponent<MissionData>().MissionType == MissionManager.MissionDifficult && MissionManager.Instance.difficultMissionNum > 0)
            {
                //myMission에 x번째 HavaMission을 추가
                //미션매니저의 복잡임무 갯수를 줄인다.
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

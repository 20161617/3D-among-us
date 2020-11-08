using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    
     static MissionManager missionManage = null;
     float missionBarFill { get; set; } //미션게이지 최소 0 최대 1 
    const float MIN_BAR = 0.0f;
    const float MAX_BAR = 1.0f;


    int commonMission { get; set; } //공통임무
    int simpleMission { get; set; } //단순임무
    int difficultMission { get; set; } //복잡임무 

   public void MissionInit() //게임시작시 미션 갯수 설정  별도로 원할시 미션 get set 으로 불러와서 지정 
    {
        commonMission = 1;
        simpleMission = 1;
        difficultMission = 2;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (missionManage == null)
            missionManage = new MissionManager();
    }
    void EndMission() //미션게이지 다채웠을떄 호출 
    {
        if(missionBarFill==MAX_BAR)
        {
            //call GameManager()->victory  
            Debug.Log("mission Complete");
        }
    }
    MissionManager getInstance()
    {
        return missionManage;
    }
    
    void MissionUpdate() //GameManager 에서 MissionUpdate 호출해서 실행 
    {
        EndMission();
    }
    
}

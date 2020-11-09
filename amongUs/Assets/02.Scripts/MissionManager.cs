using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;



public class MissionManager :   MonoBehaviourPun 
{
    public PhotonView PV;
    

    public RectTransform clearText = null;
    public Image missionGauge;
    

    float missionBarFill { get; set; } //미션게이지 최소 0 최대 1 
    const float MIN_BAR = 0.0f;
    const float MAX_BAR = 1.0f;


    int commonMission { get; set; } //공통임무
    int simpleMission { get; set; } //단순임무
    int difficultMission { get; set; } //복잡임무 

    
    private void Awake()
    {


       
    }

    public void setView(PhotonView _pv)
    {
        PV = _pv;
    }



    public void missionClear(GameObject _obejct) // 미션을꺳을떄 
    {


        StartCoroutine(MissionClearText(_obejct));
        StartCoroutine(setMission_End(_obejct));
        MissionClear(_obejct);
    }

    private IEnumerator MissionClearText(GameObject _object) //텍스트 올라왔다 사라지는거 
    {
        float upText = clearText.anchoredPosition.y;
        while (!(clearText.anchoredPosition.y > 0))
        {
            upText+=5;
            clearText.anchoredPosition = new Vector3(0, upText, 0);           
            yield return new WaitForSeconds(0.001f);
        }
        yield return null;
    }
    public void UpGauge()
    {
        missionGauge.fillAmount += 0.25f;
    }
    [PunRPC]
    private void AddMissionGauge(GameObject _object) //게이지 오르는거 
    {
        missionGauge.fillAmount += _object.GetComponent<Gague>().setGague * 0.01f;
    }
    private IEnumerator setMission_End(GameObject _object)
    {
        _object.SetActive(false);
        yield return new WaitForSeconds(1f);
        Vector2 setPos = new Vector2(0, -600f);
        clearText.anchoredPosition = setPos;
        yield return null;
    }

    public void MissionClear(GameObject _object)
    {
        AddMissionGauge(_object);
        PV.RPC("AddMissionGauge", RpcTarget.AllViaServer);
        Debug.Log(_object.GetComponent<Gague>().setGague);

       
    }
   public void MissionInit() //게임시작시 미션 갯수 설정  별도로 원할시 미션 get set 으로 불러와서 지정 
    {
        commonMission = 1;
        simpleMission = 1;
        difficultMission = 2;
    }
    // Start is called before the first frame update
  
    void EndMission() //미션게이지 다채웠을떄 호출 
    {
        if(missionBarFill==MAX_BAR)
        {
            //call GameManager()->victory  
            Debug.Log("mission Complete");
        }
    }
  
    void MissionUpdate() //GameManager 에서 MissionUpdate 호출해서 실행 
    {
        EndMission();
    }

  
}

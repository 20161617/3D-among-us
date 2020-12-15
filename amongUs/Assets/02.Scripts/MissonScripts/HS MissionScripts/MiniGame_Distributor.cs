using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static MissionManager;

public class MiniGame_Distributor : MonoBehaviour //전기 배급 미션 
{
    public GameObject MinigamePanel;
    public DistributorObject[] circle; //노랑 , 파랑 , 하늘색 클릭  전기 배급 

    public float rotationSpeed = 0.1f; // 사이클 회전 속도
    public int select; //현재 몇번쨰인지 나타내는것 
    private int selectMax = 3;

    void OnEnable()
    {
        MissionInit();
        Debug.Log("배선 미니게임 시작 ");
    }
    void MissionInit()
    {
        for (int i = 0; i < circle.Length; i++)
        {
            circle[i].MissionSet();
        }
        select = 0;
    }
    // Update is called once per frame
    void MoveCircle(int _select)
    {
        circle[_select].circle.transform.rotation = Quaternion.Euler(new Vector3(0, 0, (circle[_select].circle.eulerAngles.z) + rotationSpeed));
        //Debug.Log(circle[_select].circle.eulerAngles.z);
    }
    void SuccessCircle() //원이 일치하는가 ? 
    {
        //!circle[select].point.activeSelf
        if (circle[select].circle.transform.eulerAngles.z > 350 || circle[select].circle.transform.eulerAngles.z < 20)//원이 일치하고 point 오브젝트가 비활성화일떄 
        {
            if (!circle[select].point.activeSelf)
            {
                circle[select].LightUP(true);
            }
        }
        else
        {
            if (circle[select].point.activeSelf)
            {
                circle[select].LightUP(false);
            }
        }
    }
    void ClickButton() //버튼 클릭 
    {
        if (circle[select].button.Getbutton && circle[select].point.activeSelf) //버튼을 타이밍에 맞게 눌렀을떄 
        {
            select++;
        }
        else if (circle[select].button.Getbutton && !circle[select].point.activeSelf) //버튼을 잘못눌렀을떄  초기화 
        {
            MissionInit();
        }
        if (select >= selectMax) //미션을 다 완료햇을떄 
        {
            missionManager.MissionClear(MinigamePanel);
            rotationSpeed = 0;
            select = 0;
        }
    }
    void Update()
    {
        MoveCircle(select);
        SuccessCircle();
        ClickButton();
    }
}

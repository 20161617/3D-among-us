using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using static MissionManager;
using static DatabaseManager;

public class TargetCtrl : MonoBehaviourPun
{
    //레이캐스트를 이용해 앞에 있는 오브젝트가 무엇인지 알아내는 클래스

    PhotonView PV;
    //레이에 맞은 오브젝트
    RaycastHit hit;
    //상호작용할 미션데이터
    MissionData TargetMissionData;
    //상호작용 거리
    float MaxDistance = 1.0f;
    //상호작용 오브젝트
    public string InteractionObject = "";

    //이전에 선택한 오브젝트
    private Transform _selection;





    void Start()
    {
        PV = photonView;
    }

    void Update()
    {

        if (!PV.IsMine)
            return;
        //이전에 선택한 오브젝트가 비어있지 않다면
        if (_selection != null)
        {
            //반짝이를 끄고, 이전에 선택한 오브젝트와 상호작용하는 오브젝트의 이름과 현재미션데이터를 비운다.
            GlowObject selectionGlowObject = _selection.GetComponent<GlowObject>();
            selectionGlowObject.OnRaycastExit();
            _selection = null;
            InteractionObject = "";
            TargetMissionData = null;
        }

        //Ray를 볼 수 있게 표시 해준다.
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z), transform.forward * MaxDistance, Color.blue, 0.3f);

        //Ray에 닿은 오브젝트가 있다면
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z), transform.forward, out hit, MaxDistance))
        {
            if (hit.collider.name == "Detect Dead")
                return;
            //닿은 오브젝트가 상호작용 할 수 있는 오브젝트 라면
            if (hit.transform.CompareTag("INTERACTION"))
            {
                //닿은 오브젝트를 선택
                var selection = hit.transform;
                //선택한 오브젝트의 미션데이터를 현재미션데이터에 저장
                TargetMissionData = selection.GetComponent<MissionData>();

                //선택한 오브젝트의 미션데이터가 현재 임무에 포함되어 있다면
                if (MissionManager.Instance.ContainsMission(TargetMissionData.MissionType, TargetMissionData.MissionNumber, databaseManager.MyPlayer.isImposter))
                {
                   
                    //다른미션도 받아들일까봐
                    //현재미션 체크
                    if(!(TargetMissionData.MissionType == "PLAYER")&& !transform.GetComponent<PlayerMission>().myMission.Contains(selection.gameObject))
                      return;

                    //반짝이를 켜준다
                    GlowObject selectionGlowObject = selection.GetComponent<GlowObject>();

                    selectionGlowObject.OnRaycastEnter();

                    //이전에 선택한 오브젝트에 현재 선택한 오브젝트를 넣어준다
                    _selection = selection;

                    //상호작용하는 오브젝트 이름에 현재 충돌하고있는 오브젝트의 이름을 넣어준다
                    InteractionObject = hit.collider.name;

                    MissionManager.Instance.myMission = transform.GetComponent<PlayerMission>().myMission;
                    MissionManager.Instance.clearObject = selection.gameObject;
                }
            }

            Debug.Log(hit.collider.name);

        }
        //충돌하고 있는 오브젝트의 이름을 넣어준다
    }


    //Use버튼을 누르면 현재 미션데이터에 맞는 미션을 불러오는 함수를 호출
    public void TargetUse()
    {
        MissionManager.Instance.CallMission(TargetMissionData.MissionType, TargetMissionData.MissionNumber);
    }
    //임포스터일 경우, Kill 버튼을 누르면 상대방을 죽인다.
    public void TargetKill()
    {
        Debug.Log("죽여!");
        databaseManager.MyPlayer.GetComponent<PhotonView>().RPC("KillRPC", RpcTarget.AllViaServer);
        _selection.GetComponent<PlayerScript>().KillingPlayer();
    }
    //범위안에 죽은 사람이 있을경우, Report 버튼을 누르면 신고한다.
    public void TargetReport()
    {

    }
}

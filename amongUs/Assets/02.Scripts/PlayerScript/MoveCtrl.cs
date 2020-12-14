using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MoveCtrl : MonoBehaviourPunCallbacks, IPunObservable
{
    PhotonView pv;
    public float speed = 5.0f;
    public float turnSpeed = 1.1f;
    private Transform tr;
    Rigidbody rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Start()
    {

        pv = photonView;
        tr = GetComponent<Transform>();
        //  MissionManager.Instance().setView(pv);

    }
    public bool getViewIsMine()
    {
        return photonView.IsMine;
    }
    void Move()
    {
        //controlled locally일 경우 이동(자기 자신의 캐릭터일 때)
        if (photonView.IsMine)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(0.0f, 0.0f, moveVertical);
            tr.Translate(movement * Time.deltaTime * speed);


            Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            bool isMove = moveInput.magnitude != 0;

            if (isMove)
            {

                transform.Rotate(0f, moveHorizontal, 0f);

            }




        }
        else
        {
            //끊어진 시간이 너무 길 경우(텔레포트)
            if ((tr.position - currPos).sqrMagnitude >= 10.0f * 10.0f)
            {
                tr.position = currPos;
                tr.rotation = currRot;
            }
            //끊어진 시간이 짧을 경우(자연스럽게 연결 - 데드레커닝)
            else
            {
                tr.position = Vector3.Lerp(tr.position, currPos, Time.deltaTime * 10.0f);
                tr.rotation = Quaternion.Slerp(tr.rotation, currRot, Time.deltaTime * 10.0f);
            }
        }

    }

    //오브젝트와 충돌했을때, 자동으로 회전되는 것을 막음
    void FixedUpdate()
    {
        FreezeRotation();
        Move();
    }

    void FreezeRotation()
    {
        rigid.angularVelocity = Vector3.zero;
    }

    //클론이 통신을 받는 변수 설정
    private Vector3 currPos;
    private Quaternion currRot;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //통신을 보내는 
        if (stream.IsWriting)
        {
            stream.SendNext(tr.position);
            stream.SendNext(tr.rotation);
        }

        //클론이 통신을 받는 
        else
        {
            currPos = (Vector3)stream.ReceiveNext();
            currRot = (Quaternion)stream.ReceiveNext();
        }
    }
}

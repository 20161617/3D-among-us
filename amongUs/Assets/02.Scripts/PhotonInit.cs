using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

using UnityEngine.SceneManagement;

public class PhotonInit : MonoBehaviourPunCallbacks
{
    public static PhotonInit Instance;
    public PhotonView PV;
    private void Awake()
    {
        Instance = this;
        PV = photonView;
        MissionManager.instance.setView(PV);
        PhotonNetwork.AutomaticallySyncScene = true;
        //방의 모든 클라이언트가 마스터 클라이언트와 동일한 레벨을로드해야하는지 여부를 정의합니다.
    }
    // Start is called before the first frame update
    // Use this for initialization
    void Start()
    {
      
        PhotonNetwork.GameVersion = "1.0";
        PhotonNetwork.ConnectUsingSettings(); //즉시 온라인 상태로 만들어줌 
    }

    //포톤 서버에 접속
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();
        //방의 모든 클라이언트가 마스터 클라이언트와 동일한 레벨을로드해야하는지 여부를 정의합니다.
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //네트워크에 연결 된 이후에 랜덤하게 방에 입장한다. 
        //처음에는 방이 없기 때문에 방입장에 실패할 경우에 새로운 방을 생성한다. 
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 10;
        PhotonNetwork.CreateRoom("amongUs", new RoomOptions { MaxPlayers = 10 });
    }
    //방에 입장한 후에 플레이어 생성
    public override void OnJoinedRoom()
    {

        StartCoroutine(this.CreatePlayer());
        CreateCamera();
        //방에 입장한 이후에 플레이어를 생성한다. 
        //네트워크에 연결 후에 플레이어가 생성되어야 통신이 되기 때문이다. 
        //인스턴스할 플레이어는 project창에 Resources폴더를 만들어서 prefab으로 생성한다. 

    }
    IEnumerator CreatePlayer()
    {
        //PhotonNetwork.Instantiate(player.name, Vector3.zero, Quaternion.identity);
        PhotonNetwork.Instantiate("cube", Vector3.zero, Quaternion.identity);
        yield return null;

    }
    public void CreateCamera()
    {
        //GameObject mainCamera = GameObject.FindWithTag("MainCamera");


    }
    // Update is called once per frame

}

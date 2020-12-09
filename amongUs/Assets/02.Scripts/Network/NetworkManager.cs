using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class NetworkManager : MonoBehaviourPunCallbacks
{
    [Header("LobbyPanel")]
    public GameObject LobbyPanel;
    public InputField NickNameInput;
    public GameObject room;
    public Transform gridTr;

    [Header("RoomPanel")]
    public GameObject RoomPanel;
    public Button[] CellBtn;
    public Text RoomInfoText;

    [Header("ChatPanel")]
    public GameObject ChatPanel;
    public InputField ChatInput;
    public Text[] ChatText;

    [Header("GameSceneManager")]
    public GameSceneManager GSM;

    [Header("ETC")]
    public Text StatusText;
    public PhotonView PV;

    //새로 추가..
    public static NetworkManager NetInstance;
    public PlayerScript MyPlayer;
    public List<PlayerScript> Players = new List<PlayerScript>();


    List<RoomInfo> myList = new List<RoomInfo>();

    #region 방리스트 갱신
    public void MyListClick(int num)
    {
        if (myList.Count > num)
        {
            PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;
            PhotonNetwork.JoinRoom(myList[num].Name);
            MyListRenewal();
        }
    }

    public void OnClickRoom(string roomName)
    {

        PhotonNetwork.JoinRoom(roomName);
    }

    void MyListRenewal()
    {
        //방정보 갱신
        for (int i = 0; i < CellBtn.Length; i++)
        {
            bool isActive = (i < myList.Count) ? true : false;
            CellBtn[i].gameObject.SetActive(isActive);
            // 방 이름
            CellBtn[i].transform.GetChild(0).GetComponent<Text>().text = isActive ? myList[i].Name : "";
            // 임포스터 수
            CellBtn[i].transform.GetChild(2).GetComponent<Text>().text = isActive ? "1" : "";
            // 총 크루원 수
            CellBtn[i].transform.GetChild(4).GetComponent<Text>().text = isActive ? string.Format("{0}/{1}", myList[i].PlayerCount, myList[i].MaxPlayers) : "";
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        int roomCount = roomList.Count;
        for (int i = 0; i < roomCount; i++)
        {
            //방 정보가 없다면..? true를 반환
            if (!roomList[i].RemovedFromList)
            {
                //이미 있으면 넣지 않는다..
                if (!myList.Contains(roomList[i])) myList.Add(roomList[i]);

                //어느 위치에 있는지 검색 후 인덱스를 반환한다..
                else myList[myList.IndexOf(roomList[i])] = roomList[i];

            }

            //검색 후 해당 인스터스가 없으면 -1을 반환, 고로 없을 경우.. 아마 오류를 방지하기 위한 코드..?
            else if (myList.IndexOf(roomList[i]) != -1) myList.RemoveAt(myList.IndexOf(roomList[i]));
        }
        MyListRenewal();
    }

    #endregion

    #region 서버연결
    private void Awake()
    {
        Screen.SetResolution(960, 540, false);
        Connect();

        //싱글톤
        if (NetInstance == null)
        {
            //이 클래스의 인스턴스가 탄생했을 때 전역변수 NetInstance에 게임매니저 인스턴스가 담겨있지 않다면, 자신을 넣어준다
            NetInstance = this;

            //씬 전환이 되더라도 파괴되지 않게 한다.
            //gameObect만으로도 이 스크립트가 컴포넌트로서 붙어 있는 Hierarchy상의 게임오브젝트라는 뜻이지만,
            //헷갈림 방지를 위해 this를 붙여주기도 한다.
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //만약 씬 이동이 되었는데 그 씬에도 Hierarchy에 GameMgr 존재할 수도 있다.
            //그럴 경우엔 이전 씬에서 사용하던 인스턴스를 사용해주는 경우가 많다
            //그래서 이미 전역변수인 NetInstance에 인스턴스가 존재한다면 자신(새로운 씬의 오브젝트)을 삭제해준다.
            Destroy(this.gameObject);
        }

        // PV = photonView;
    }

    private void Update()
    {
        StatusText.text = PhotonNetwork.NetworkClientState.ToString();

        if (ChatPanel != null)
        {
            if (ChatPanel.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Send();
                }
            }
        }
    }

    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        LobbyPanel.SetActive(true);
        RoomPanel.SetActive(false);
        ChatPanel.SetActive(false);
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;
    }

    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        LobbyPanel.SetActive(false);
        RoomPanel.SetActive(false);
        ChatPanel.SetActive(false);
    }

    /*
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("ROOM"))
        {
            Destroy(obj);
        }

        foreach (RoomInfo roomInfo in roomList)
        {
            Debug.Log(roomInfo.Name);
            GameObject _room = Instantiate(room, gridTr);
            RoomData roomData = _room.GetComponent<RoomData>();
            Debug.Log(roomData.roomName);
            roomData.roomName = roomInfo.Name;
            roomData.maxPlayer = roomInfo.MaxPlayers;
            roomData.playerCount = roomInfo.PlayerCount;
            roomData.UpdateInfo();
            roomData.GetComponent<Button>().onClick.AddListener
            (
                delegate
                {
                    OnClickRoom(roomData.roomName);
                }
            );
        }
    }
    */

    /*
    void OnClickRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName, null);
    }
    */

    #endregion

    #region 방
    public void CreateRoom()
    {
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;
        PhotonNetwork.CreateRoom(NickNameInput.text == "" ? "Room" + Random.Range(0, 100) : NickNameInput.text, new RoomOptions { MaxPlayers = 4 });

        LobbyPanel.SetActive(false);
        RoomPanel.SetActive(true);
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnJoinedRoom()
    {
        LobbyPanel.SetActive(false);
        RoomPanel.SetActive(true);
        RoomRenewal();
        ChatInput.text = "";

        for (int i = 0; i < ChatText.Length; i++)
        {
            ChatText[i].text = "";
        }

        MyPlayer = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity).GetComponent<PlayerScript>();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        CreateRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        RoomRenewal();
        ChatRPC("<color=yellow>" + newPlayer.NickName + "님이 참가하셨습니다</color>");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RoomRenewal();
        ChatRPC("<color=yellow>" + otherPlayer.NickName + "님이 퇴장하셨습니다</color>");
    }

    void RoomRenewal()
    {
        RoomInfoText.text = PhotonNetwork.CurrentRoom.Name + " / " + PhotonNetwork.CurrentRoom.PlayerCount + "/" + PhotonNetwork.CurrentRoom.MaxPlayers;
    }

    #endregion

    #region 게임시작
    //새로 추가된 부분
    public void GameStart()
    {
        SetImpoCrew();
        PV.RPC("GameStartRPC", RpcTarget.AllViaServer);
    }


    void SetImpoCrew()
    {
        List<PlayerScript> ImpoList = new List<PlayerScript>(Players);
        for (int i = 0; i < 1; i++)
        {
            int rand = Random.Range(0, ImpoList.Count);
            Debug.Log(rand + "번 플레이어 임포");
            Debug.Log(ImpoList.Count + "명");
            Players[rand].GetComponent<PhotonView>().RPC("SetImpoCrew", RpcTarget.AllViaServer, true);
            ImpoList.RemoveAt(rand);
        }
    }


    [PunRPC]
    void GameStartRPC()
    {
        GSM.GameStart = true;
        GSM.call();
        SceneManager.LoadScene("GameScene");
        print("게임시작");
    }


    #endregion

    #region 채팅
    public void OnChat()
    {
        ChatPanel.SetActive(!ChatPanel.activeSelf);
    }
    public void Send()
    {
        PV.RPC("ChatRPC", RpcTarget.All, PhotonNetwork.NickName + " : " + ChatInput.text);
        ChatInput.text = "";
    }

    [PunRPC] // RPC는 플레이어가 속해있는 방 모든 인원에게 전달한다
    void ChatRPC(string msg)
    {
        bool isInput = false;
        for (int i = 0; i < ChatText.Length; i++)
        {
            if (ChatText[i].text == "")
            {
                isInput = true;
                ChatText[i].text = msg;
                break;
            }
        }

        if (!isInput) // 꽉차면 한칸씩 위로 올림
        {
            for (int i = 1; i < ChatText.Length; i++) ChatText[i - 1].text = ChatText[i].text;
            ChatText[ChatText.Length - 1].text = msg;
        }
    }
    #endregion


}

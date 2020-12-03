using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
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
    //public Text ListText;
    public Text RoomInfoText;

    [Header("ChatPanel")]
    public GameObject ChatPanel;
    public InputField ChatInput;
    public Text[] ChatText;

    [Header("IntroPanel")]
    public GameObject IntroPanel;

    [Header("ETC")]
    public Text StatusText;
    public PhotonView PV;
    public static NetworkManager NM;
    public List<PlayerScript> Players = new List<PlayerScript>();
    public PlayerScript MyPlayer;

    #region 서버연결
    private void Awake()
    {
        NM = this;
        PV = photonView;
        Screen.SetResolution(960, 540, false);
        //NickNameInput.text = "";
        Connect();
    }

    private void Update()
    {
        StatusText.text = PhotonNetwork.NetworkClientState.ToString();

        if (ChatPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Send();
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

    void OnClickRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName, null);
    }
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
        RoomPanel.SetActive(true);
        RoomRenewal();
        ChatInput.text = "";
        for (int i = 0; i < ChatText.Length; i++)
        {
            ChatText[i].text = "";
        }
        MyPlayer = PhotonNetwork.Instantiate("Cube", Vector3.zero, Quaternion.identity).GetComponent<PlayerScript>();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        //RoomInput.text = "";
        CreateRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //RoomInput.text = ""; 
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
        // ListText.text = "";
        //for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        //{
        //   // ListText.text += PhotonNetwork.PlayerList[i].NickName + ((i + 1 == PhotonNetwork.PlayerList.Length) ? "" : ", ");
        //}
        RoomInfoText.text = PhotonNetwork.CurrentRoom.Name + " / " + PhotonNetwork.CurrentRoom.PlayerCount + "/" + PhotonNetwork.CurrentRoom.MaxPlayers;
    }

    public void GameStart()
    {
        PV.RPC("GameStart", RpcTarget.AllViaServer);
    }

    [PunRPC]
    void GameStartRPC()
    {
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
            if (ChatText[i].text == "")
            {
                isInput = true;
                ChatText[i].text = msg;
                break;
            }
        if (!isInput) // 꽉차면 한칸씩 위로 올림
        {
            for (int i = 1; i < ChatText.Length; i++) ChatText[i - 1].text = ChatText[i].text;
            ChatText[ChatText.Length - 1].text = msg;
        }
    }
    #endregion


}

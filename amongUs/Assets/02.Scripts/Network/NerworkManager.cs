using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;


public class NerworkManager : MonoBehaviourPunCallbacks
{
    [Header("LobbyPanel")]
    public GameObject LobbyPanel;
    public InputField NickNameInput;
    public Button[] cellBtn;

    [Header("RoomPanel")]
    public GameObject RoomPanel;
    public Text RoomInfoText;

    [Header("ChatPanel")]
    public GameObject ChatPanel;
    public InputField ChatInput;
    public Text[] ChatText;

    [Header("ETC")]
    public Text StatusText;
    public PhotonView PV;

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

    void MyListRenewal()
    {
        for(int i =0; i< cellBtn.Length; i++)
        {
            cellBtn[i].transform.GetChild(0).GetComponent<Text>().text = (i < myList.Count) ? myList[i].Name : "";
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
      
        RoomInfoText.text = PhotonNetwork.CurrentRoom.Name + " / " + PhotonNetwork.CurrentRoom.PlayerCount + "/" + PhotonNetwork.CurrentRoom.MaxPlayers;
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

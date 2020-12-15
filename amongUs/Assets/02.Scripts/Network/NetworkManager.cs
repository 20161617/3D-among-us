using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static DatabaseManager;


public class NetworkManager : MonoBehaviourPunCallbacks
{
    [Header("LobbyPanel")]
    public GameObject LobbyPanel;
    public InputField NickNameInput;
    public Button[] CellBtn;


    [Header("ETC")]
    public Text StatusText;



    List<RoomInfo> myList = new List<RoomInfo>();

    #region 서버연결
    private void Awake()
    {
        Screen.SetResolution(960, 540, false);
        Connect();
    }

    private void Update()
    {
        if (StatusText != null)
            StatusText.text = PhotonNetwork.NetworkClientState.ToString();
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
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;
    }

    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {

    }

    #endregion

    #region 방
    public void CreateRoom()
    {
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;

        PhotonNetwork.CreateRoom(NickNameInput.text == "" ? "Room" + Random.Range(0, 100) : NickNameInput.text, new RoomOptions { MaxPlayers = 4 });

    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();

        databaseManager.RoomRenewal();
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("WaitingRoom");

        databaseManager.RoomRenewal();

        databaseManager.MyPlayer = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity).GetComponent<PlayerScript>();

        //databaseManager.MyPlayer.photonView.RPC("HideCharacter", RpcTarget.AllViaServer);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        CreateRoom();
    }

    #endregion

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
}

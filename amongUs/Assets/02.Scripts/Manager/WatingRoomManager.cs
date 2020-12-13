using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WatingRoomManager : MonoBehaviourPun
{
    [Header("RoomPanel")]
    public GameObject RoomPanel;
    public Text RoomInfoText;



    void Update()
    {
        RoomRenewal();
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("LobbyScene");
    }



    void RoomRenewal()
    {
        DatabaseManager.databaseManager.RoomRenewal();
        RoomInfoText.text = DatabaseManager.databaseManager.roomInfoText;
    }


    #region 게임시작
    //새로 추가된 부분
    public void GameStart()
    {
        SetImpoCrew();
        photonView.RPC("GameStartRPC", RpcTarget.AllViaServer);
    }


    void SetImpoCrew()
    {
        List<PlayerScript> ImpoList = new List<PlayerScript>(DatabaseManager.databaseManager.Players);

        for (int i = 0; i < 1; i++)
        {
            int rand = Random.Range(0, ImpoList.Count);
            Debug.Log(rand + "번 플레이어 임포");
            Debug.Log(ImpoList.Count + "명");

            DatabaseManager.databaseManager.Players[rand].GetComponent<PhotonView>().RPC("SetImpoCrew", RpcTarget.AllViaServer, true);
            ImpoList.RemoveAt(rand);
        }
    }


    [PunRPC]
    void GameStartRPC()
    {
        SceneManager.LoadScene("GameScene");
        print("게임시작");
    }


    #endregion

}

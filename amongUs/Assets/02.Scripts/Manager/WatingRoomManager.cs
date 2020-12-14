using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static DatabaseManager;

public class WatingRoomManager : MonoBehaviourPun
{
    [Header("RoomPanel")]
    public GameObject RoomPanel;
    public Text RoomInfoText;
    public GameObject[] playerPos;

    private int posIndex = 0;

    private void Awake()
    {
        StartCoroutine("Init");
    }

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
        databaseManager.RoomRenewal();
        RoomInfoText.text = databaseManager.roomInfoText;
    }

    IEnumerator Init()
    {
        yield return new WaitForSeconds(0.05f);

        databaseManager.SetRandColor();

        databaseManager.MyPlayer.photonView.RPC("SetMyPosition", RpcTarget.AllViaServer, playerPos[databaseManager.Players.Count -1].transform.position);

        databaseManager.MyPlayer.GetComponent<PhotonView>().RPC("ShowCharacter", RpcTarget.AllViaServer);
    }

    #region 게임시작
    //새로 추가된 부분
    public void GameStart()
    {
        SetImpoCrew();
        HideCharacter();
        photonView.RPC("GameStartRPC", RpcTarget.AllViaServer);
    }


    void SetImpoCrew()
    {
        List<PlayerScript> ImpoList = new List<PlayerScript>(databaseManager.Players);

        for (int i = 0; i < 1; i++)
        {
            int rand = Random.Range(0, ImpoList.Count);
            Debug.Log(rand + "번 플레이어 임포");
            Debug.Log(ImpoList.Count + "명");

            databaseManager.Players[rand].GetComponent<PhotonView>().RPC("SetImpoCrew", RpcTarget.AllViaServer, true);
            ImpoList.RemoveAt(rand);
        }

    }

    void HideCharacter()
    {
        for (int i = 0; i < databaseManager.Players.Count; i++)
            databaseManager.Players[i].GetComponent<PhotonView>().RPC("HideCharacter", RpcTarget.AllViaServer);
    }


    [PunRPC]
    void GameStartRPC()
    {
        SceneManager.LoadScene("GameScene");
        print("게임시작");
    }


    #endregion

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class DatabaseManager : MonoBehaviourPun
{
    public static DatabaseManager databaseManager;

    public PlayerScript MyPlayer;

    public List<PlayerScript> Players = new List<PlayerScript>();

    public string roomInfoText;

    private void Awake()
    {
        if(databaseManager == null)
        {
            databaseManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void RoomRenewal()
    {
        roomInfoText = PhotonNetwork.CurrentRoom.Name + " / " + PhotonNetwork.CurrentRoom.PlayerCount + "/" + PhotonNetwork.CurrentRoom.MaxPlayers;
    }
}

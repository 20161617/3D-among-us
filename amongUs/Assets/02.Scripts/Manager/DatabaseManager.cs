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

    public List<Material> Colors = new List<Material>();

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


    public void setRandColor()
    {
        List<int> PlayerColors = new List<int>();
        for(int i=0; i <Players.Count; i++)
        {
            PlayerColors.Add(Players[i].colorIndex);
        }

        while (true)
        {
            int rand = Random.Range(0, 12);
            if (!PlayerColors.Contains(rand))
            {
                MyPlayer.GetComponent<PhotonView>().RPC("SetColor", RpcTarget.AllBuffered, rand);
                break;
            }
        }
       
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public enum COLOR
{
    RED,
    BLUE,
    GREEN,
    PINK,
    ORANGE,
    YELLOW,
    GREY,
    WHITE,
    PURPLE,
    BROWN,
    MINT,
    LIGHTGREEN
}


public class DatabaseManager : MonoBehaviourPun
{
    public static DatabaseManager databaseManager;

    public PlayerScript MyPlayer;

    public List<PlayerScript> Players = new List<PlayerScript>();

    public List<Material> Colors = new List<Material>();

    public string roomInfoText;

    public const int VOTE_TIME = 120;

    public int deportMember = -1;

    public int impoCount;

    public bool isEvent;
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


    public void SetRandColor()
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


    public void SortPlayers()
    {
        //Players.Sort((p1, p2) => p1.actor.CompareTo(p2.actor)
    }


    public Color SetColorIcon(int colorIndex)
    {
        Color color = Color.white;

        switch ((COLOR)colorIndex)
        {
            case COLOR.RED:
                color.r = 180.0f/255.0f;
                color.g = 0.0f / 255.0f;
                color.b = 0.0f / 255.0f;
                color.a = 255.0f / 255.0f;
                break;

            case COLOR.BLUE:
                color.r = 0.0f / 255.0f;
                color.g = 80.0f / 255.0f;
                color.b = 180.0f / 255.0f;
                color.a = 255.0f / 255.0f;
                break;

            case COLOR.GREEN:
                color.r = 0.0f / 255.0f;
                color.g = 180.0f / 255.0f;
                color.b = 0.0f / 255.0f;
                color.a = 255.0f / 255.0f;
                break;

            case COLOR.PINK:
                color.r = 220.0f / 255.0f;
                color.g = 80.0f / 255.0f;
                color.b = 170.0f / 255.0f;
                color.a = 255.0f / 255.0f;
                break;

            case COLOR.ORANGE:
                color.r = 255.0f / 255.0f;
                color.g = 100.0f / 255.0f;
                color.b = 0.0f / 255.0f;
                color.a = 255.0f / 255.0f;
                break;

            case COLOR.YELLOW:
                color.r = 225.0f / 255.0f;
                color.g = 230.0f / 255.0f;
                color.b = 0.0f / 255.0f;
                color.a = 255.0f / 255.0f;
                break;

            case COLOR.GREY:
                color.r = 100.0f / 255.0f;
                color.g = 100.0f / 255.0f;
                color.b = 100.0f / 255.0f;
                color.a = 255.0f / 255.0f;
                break;

            case COLOR.WHITE:
                color.r = 255.0f / 255.0f;
                color.g = 255.0f / 255.0f;
                color.b = 255.0f / 255.0f;
                color.a = 255.0f / 255.0f;
                break;

            case COLOR.PURPLE:
                color.r = 110.0f / 255.0f;
                color.g = 0.0f / 255.0f;
                color.b = 150.0f / 255.0f;
                color.a = 255.0f / 255.0f;
                break;

            case COLOR.BROWN:
                color.r = 110.0f / 255.0f;
                color.g = 40.0f / 255.0f;
                color.b = 20.0f / 255.0f;
                color.a = 255.0f / 255.0f;
                break;

            case COLOR.MINT:
                color.r = 20.0f / 255.0f;
                color.g = 220.0f / 255.0f;
                color.b = 200.0f / 255.0f;
                color.a = 255.0f / 255.0f;
                break;

            case COLOR.LIGHTGREEN:
                color.r = 170.0f / 255.0f;
                color.g = 230.0f / 255.0f;
                color.b = 70.0f / 255.0f;
                color.a = 255.0f / 255.0f;
                break;
        }

        return color;
    }



    public void DeportMessage(Text t1, Text t2)
    {

    }
}

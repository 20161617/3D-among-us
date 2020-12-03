using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomData : MonoBehaviour
{
    public string roomName = "";
    public int playerCount = 0;
    public int maxPlayer = 0;

    public Text RoomName;
    public Text ImposterNum;
    public Text CrewNum;

    public void UpdateInfo()
    {
        RoomName.text = roomName;
        ImposterNum.text = "1";
        CrewNum.text = string.Format("{0}/{1}", playerCount, maxPlayer);
    }
}

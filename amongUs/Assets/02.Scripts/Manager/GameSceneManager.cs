using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager GameInstance;
    public GameObject MainCamera;
    public GameObject Map;
    public GameObject UIPanel;
    public GameObject MissionBar;

    GameObject networkManager;


    void Awake()
    {
        GameInstance = this;

        networkManager = GameObject.Find("NetworkManager");
    }
    void Start()
    {
        Destroy(networkManager);
    }


    public void CameraOn()
    {
        Debug.Log("카메라온");
        MainCamera.SetActive(true);
        Map.SetActive(true);
        UIPanel.SetActive(true);
        MissionBar.SetActive(true);
    }

}

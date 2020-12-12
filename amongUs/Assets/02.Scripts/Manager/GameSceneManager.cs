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
        MainCamera.SetActive(true);
        Map.SetActive(true);
        UIPanel.SetActive(true);
    }

}

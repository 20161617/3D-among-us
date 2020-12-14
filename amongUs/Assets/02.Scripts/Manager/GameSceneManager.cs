using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviourPun
{
    public static GameSceneManager GameInstance;
    public GameObject MainCamera;
    public GameObject Map;
    public GameObject UIPanel;
    public GameObject MissionPanel;

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

    private void Update()
    {
        if (DatabaseManager.databaseManager.isEvent)
        {
            DatabaseManager.databaseManager.isEvent = false;
            MainCamera.SetActive(false);
            Map.SetActive(false);
            UIPanel.SetActive(false);
            MissionPanel.SetActive(false);

            //photonView.RPC("NextScene", RpcTarget.AllViaServer);
            SceneManager.LoadScene("DeportScene", LoadSceneMode.Additive);
        }
    }

    public void CameraOn()
    {
        MainCamera.SetActive(true);
        Map.SetActive(true);
        UIPanel.SetActive(true);
        MissionPanel.SetActive(true);
    }

    IEnumerator EventScene()
    {
        yield return new WaitForSeconds(0.1f);

       
    }
    [PunRPC]
    public void NextScene()
    {
        SceneManager.LoadScene("DeportScene", LoadSceneMode.Additive);
    }
}

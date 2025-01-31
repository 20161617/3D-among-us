﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using static NetworkManager;

public class UIManager : MonoBehaviourPun
{

    public static UIManager uiInstance;
    public Button StartBtn;

    void Awake()
    {
        uiInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        if (StartBtn != null)
            ShowStartBtn();
    }
    void ShowStartBtn()
    {
        StartBtn.gameObject.SetActive(true);
        StartBtn.interactable = PhotonNetwork.CurrentRoom.PlayerCount >= 2;
    }
}

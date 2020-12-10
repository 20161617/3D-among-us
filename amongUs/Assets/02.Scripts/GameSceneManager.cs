﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager GameInstance;
    public GameObject MainCamera;
    public GameObject Map;

    void Awake()
    {
        GameInstance = this;
    }


    public void CameraOn()
    {
        MainCamera.SetActive(true);
        Map.SetActive(true);
    }

}

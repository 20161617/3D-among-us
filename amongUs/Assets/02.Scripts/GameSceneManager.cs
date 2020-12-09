using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    public bool GameStart;
    public GameObject gameSceneManager;


    public void call()
    {
        DontDestroyOnLoad(gameSceneManager);
    }
}

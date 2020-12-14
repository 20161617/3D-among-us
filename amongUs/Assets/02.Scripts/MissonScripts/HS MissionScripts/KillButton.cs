using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DatabaseManager;

public class KillButton : MonoBehaviour
{
    Button button;

    void Start()
    {
        Debug.LogError("죽이기실행");
        button = GetComponent<Button>();
        button.onClick.AddListener(() => databaseManager.MyPlayer.targetCtrl.TargetKill());
    }

}

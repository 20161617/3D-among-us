using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DatabaseManager;

public class UseButton : MonoBehaviour
{
    Button button;
    //나의 플레이어 캐릭터의 targetCtrl를 참조.
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => databaseManager.MyPlayer.targetCtrl.TargetUse());
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DatabaseManager;

public class UIPanel : MonoBehaviour
{

    public Button UseButton;
    public Button ReportButton;
    public Button KillButton;
    public Button SabotageButton;
    public Button VentButton;

    bool OneCall = false;

    TargetCtrl PlayerTargetCtrl;

    private Dictionary<string, Button> ButtonList = new Dictionary<string, Button>();

    void Awake()
    {
        PlayerTargetCtrl = databaseManager.MyPlayer.targetCtrl;
    }

    void Start()
    {
        if (databaseManager.MyPlayer.isImposter)
        {
            KillButton.gameObject.SetActive(true);
            SabotageButton.gameObject.SetActive(true);
            ReportButton.gameObject.SetActive(true);
            VentButton.gameObject.SetActive(true);

            ButtonList.Add("Kill", KillButton);
            ButtonList.Add("Sabotage", SabotageButton);
            ButtonList.Add("Report", ReportButton);
            ButtonList.Add("Vent", VentButton);

        }
        else
        {
            UseButton.gameObject.SetActive(true);
            ReportButton.gameObject.SetActive(true);

            ButtonList.Add("Use", UseButton);
            ButtonList.Add("Report", ReportButton);
        }
    }


    void Update()
    {
        if (databaseManager.MyPlayer.isImposter)
            return;

        //Use 버튼 활성화/비활성화
        if (PlayerTargetCtrl.InteractionObject != "" && !OneCall)
        {
            ShowButton("Use");
            OneCall = true;
        }
        else if (PlayerTargetCtrl.InteractionObject == "" && OneCall)
        {
            HideButton("Use");
            OneCall = false;
        }
    }

    void ShowButton(string BtnName)
    {
        ButtonList[BtnName].interactable = true;
    }

    void HideButton(string BtnName)
    {
        ButtonList[BtnName].interactable = false;
    }
}

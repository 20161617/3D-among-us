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

    bool isImposter;
    bool OneCall = false;

    TargetCtrl PlayerTargetCtrl;

    private Dictionary<string, Button> ButtonList = new Dictionary<string, Button>();

    void Awake()
    {
        PlayerTargetCtrl = databaseManager.MyPlayer.targetCtrl;
        isImposter = databaseManager.MyPlayer.isImposter;
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
            ButtonList.Add("Use", UseButton);

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
        //임포스터라면
        if (isImposter)
<<<<<<< HEAD
        {
            //Kill 버튼 활성화/비활성화
            if (PlayerTargetCtrl.InteractionObject != "" && !OneCall)
            {
                if (PlayerTargetCtrl.InteractionObject == "emergencyTable")
                {
                    UseButton.gameObject.SetActive(true);
                    SabotageButton.gameObject.SetActive(false);
                    ShowButton("Use");
                }
                else
                {
                    ShowButton("Kill");
                }

                OneCall = true;
            }
            else if (PlayerTargetCtrl.InteractionObject == "" && OneCall)
            {
                UseButton.gameObject.SetActive(false);
                SabotageButton.gameObject.SetActive(true);
                HideButton("Use");
                HideButton("Kill");
                OneCall = false;
            }
        }

        //임포스터가 아니라면
        if (!isImposter)
        {
            //Use 버튼 활성화/비활성화
            if (PlayerTargetCtrl.InteractionObject != "" && !OneCall)
            {
                ShowButton("Use");
=======
        {
            //Kill 버튼 활성화/비활성화
            if (PlayerTargetCtrl.InteractionObject != "" && !OneCall)
            {
                if (PlayerTargetCtrl.InteractionObject == "emergencyTable")
                {
                    UseButton.gameObject.SetActive(true);
                    SabotageButton.gameObject.SetActive(false);
                    ShowButton("Use");
                }
                else
                {
                    ShowButton("Kill");
                }

>>>>>>> New-메인
                OneCall = true;
            }
            else if (PlayerTargetCtrl.InteractionObject == "" && OneCall)
            {
<<<<<<< HEAD
                HideButton("Use");
=======
                UseButton.gameObject.SetActive(false);
                SabotageButton.gameObject.SetActive(true);
                HideButton("Use");
                HideButton("Kill");
>>>>>>> New-메인
                OneCall = false;
            }
        }

<<<<<<< HEAD
        if (databaseManager.MyPlayer.isDetected)
        {
            ShowButton("Report");
        }
        else
        {
            HideButton("Report");
=======
        //임포스터가 아니라면
        if (!isImposter)
        {
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
>>>>>>> New-메인
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

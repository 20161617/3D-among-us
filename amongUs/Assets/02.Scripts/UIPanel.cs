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

    private Dictionary<Button, string> ButtonList = new Dictionary<Button, string>();

    // Start is called before the first frame update
    void Start()
    {
        if (databaseManager.MyPlayer.isImposter)
        {
            UseButton.gameObject.SetActive(true);
            ReportButton.gameObject.SetActive(true);

            ButtonList.Add(UseButton, "Use");
            ButtonList.Add(ReportButton, "Report");
        }
        else
        {
            KillButton.gameObject.SetActive(true);
            SabotageButton.gameObject.SetActive(true);
            ReportButton.gameObject.SetActive(true);
            VentButton.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ShowButton(string BtnName)
    {

    }
}

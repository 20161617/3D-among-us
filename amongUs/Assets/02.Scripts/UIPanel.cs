using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static NetworkManager;

public class UIPanel : MonoBehaviour
{

    public Button UseButton;
    public Button ReportButton;
    public Button KillButton;
    public Button SabotageButton;
    public Button VentButton;

    // Start is called before the first frame update
    void Start()
    {
        if (NetInstance.MyPlayer.isImposter)
        {
            UseButton.gameObject.SetActive(true);
            ReportButton.gameObject.SetActive(true);
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
}

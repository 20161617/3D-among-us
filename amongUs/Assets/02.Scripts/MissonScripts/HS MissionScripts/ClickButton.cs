using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickButton : MonoBehaviour ,IPointerDownHandler, IPointerUpHandler
{
    public bool Getbutton { get; private set; }
    const string miniGameUpLoad = "Download";
    const string miniGameSmaple = "SmapleButton";
    const string closeButton = "CloseButton";
    public int buttonNumber = 10;
    public void ButtonActive(bool set)
    {
        gameObject.SetActive(set);
    }
    public void OnEnable()
    {
        Getbutton = false;
    }
    public void OnPointerDown(PointerEventData data)
    {
        Debug.Log(data);
        if(gameObject.name== miniGameUpLoad)
        {
            gameObject.transform.parent.GetComponent<MiniGame_UploadData>().startDownload();
            Debug.Log("??");
        }
        else if(gameObject.name== miniGameSmaple)
        {
            gameObject.transform.parent.GetComponent<MiniGame_InspectSample>().StartSample();
            gameObject.SetActive(false);
            Debug.Log("??");
        }
        else if(buttonNumber!=10)
        {
            gameObject.transform.parent.parent.GetComponent<MiniGame_InspectSample>().SelectWater(buttonNumber);
            Debug.Log("??");
        }
        else if(gameObject.name==closeButton)
        {
            transform.parent.GetComponent<Gague>().transform.gameObject.SetActive(false);
            Debug.Log("??");
        }
        Getbutton = true;
    }
    public void OnPointerUp(PointerEventData data)
    {
        Getbutton = false;
    }
    public void ButtonSet()
    {
        Getbutton = false;
    }
}

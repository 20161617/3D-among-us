using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickButton : MonoBehaviour ,IPointerDownHandler, IPointerUpHandler
{
    public bool Getbutton { get; private set; }
    const string miniGameUpLoad = "Download";
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
        if(gameObject.name== miniGameUpLoad)
        {
            gameObject.transform.parent.GetComponent<MiniGame_UploadData>().startDownload();
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

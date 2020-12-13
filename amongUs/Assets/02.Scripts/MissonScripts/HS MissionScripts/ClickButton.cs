using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickButton : MonoBehaviour ,IPointerDownHandler, IPointerUpHandler
{
    public bool Getbutton { get; private set; }

    public void OnEnable()
    {
        Getbutton = false;
    }
    public void OnPointerDown(PointerEventData data)
    {
        Getbutton = true;
        Debug.Log("마우스다운 ");
    }
    public void OnPointerUp(PointerEventData data)
    {
        Getbutton = false;
        Debug.Log("마우스업  ");
    }
    public void ButtonSet()
    {
        Getbutton = false;
    }
}

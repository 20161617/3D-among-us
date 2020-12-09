using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class testMini : MonoBehaviour , IDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position += (Vector3)eventData.delta;
        Debug.Log("실행");
    }

}

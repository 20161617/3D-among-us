using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GarbageGrip : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public GameObject stick;

    public void OnPointerDown(PointerEventData data)
    {
        Debug.Log("마우스다운 ");

    }
    public void OnDrag(PointerEventData data)
    {
        if (data.position.y > 240 || data.position.y < 115)
            return; 
            transform.position = new Vector2(transform.position.x, data.position.y);
      
        Vector3 stickAngle = stick.transform.localEulerAngles;
        Debug.Log("stick" + stick.transform.localEulerAngles);
       Debug.Log("transform" + transform.position.x / 1.38f);
        stickAngle.x = transform.position.y / 1.38f;
        stick.transform.eulerAngles = stickAngle;

        //transform.position = data.position;
        //Debug.Log(data.position.y);
    }

   

}

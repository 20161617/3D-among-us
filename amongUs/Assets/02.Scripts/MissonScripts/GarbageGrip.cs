using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GarbageGrip : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public GameObject stick;
    const int gripMax = 230;
    const int gripMin = 115;
    private bool clear; 
    private void OnEnable()
    {
        transform.position = new Vector2(transform.position.x, gripMax);
        stick.transform.localEulerAngles = new Vector2(180 - ((transform.position.y - gripMin) * 1.44f), 0);
        clear = false;
    }
    public void OnPointerDown(PointerEventData data)
    {
        Debug.Log("마우스다운 ");

    }
    public void OnDrag(PointerEventData data)
    {
        if (clear)
            return; 
        if (data.position.y > gripMax)
        {
            transform.position = new Vector2(transform.position.x, gripMax);
        }
        else if(data.position.y < gripMin  )
        {
            transform.position = new Vector2(transform.position.x, gripMin);
            gameObject.transform.parent.GetComponent<MiniGame_EmptyGarbage>().startGarbage();
            clear = true;
        }
        else
        {
            transform.position = new Vector2(transform.position.x, data.position.y);
        }
        stick.transform.localEulerAngles = new Vector2(180-((transform.position.y- gripMin) *1.44f),0);  
    }
    
   

}

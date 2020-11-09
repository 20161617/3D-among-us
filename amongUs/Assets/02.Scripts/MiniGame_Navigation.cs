using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiniGame_Navigation : MonoBehaviour ,IPointerDownHandler, IDragHandler,IPointerUpHandler
{
    public RectTransform RT;
    public Vector3 mousePos;
    public Vector2 downPosition;
    public GameObject MinigamePanel;
    public GameObject MissionManager;

    public void OnPointerDown(PointerEventData data)
    {
        downPosition = data.position;
    }
    public void OnDrag(PointerEventData data)
    {
        Vector2 offset = data.position - downPosition;
        downPosition = data.position;

        RT.anchoredPosition +=offset;
        
        }



    public void OnPointerUp(PointerEventData data)
    {
      
        if (RT.anchoredPosition.x > -20 && RT.anchoredPosition.x < 20 && RT.anchoredPosition.y > -20 && RT.anchoredPosition.y < 20)
        {
            RT.anchoredPosition = Vector2.zero;
            MissionManager.GetComponent<MissionManager>().missionClear(MinigamePanel);
           // MissionManager.Instance().missionClear(MinigamePanel); 

            Debug.Log("성공");
        }
    }
  
}

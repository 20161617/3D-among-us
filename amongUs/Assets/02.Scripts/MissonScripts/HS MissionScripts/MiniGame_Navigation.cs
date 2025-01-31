﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiniGame_Navigation : MonoBehaviour ,IPointerDownHandler, IDragHandler,IPointerUpHandler
{
    public RectTransform RT;
    public GameObject MinigamePanel;
    public Vector2 firstPos;

    private void Awake()
    {
        RT = GetComponent<RectTransform>();
        firstPos = GetComponent<RectTransform>().position;
    }
     void OnEnable()
    {
        int randX = Random.Range(-200,201);
        int randY = (randX > 0) ? 200 : -200;
        firstPos = new Vector2(randX, randY);
        RT.anchoredPosition = firstPos;

        Debug.Log("항로지정 미니게임 시작 ");
    }
    public void OnPointerDown(PointerEventData data)
    {
        Debug.Log("마우스다운 ");
    }
    public void OnDrag(PointerEventData data)
    {
        RT.transform.position = data.position;
        Debug.Log("드래그 ");
    }

    public void OnPointerUp(PointerEventData data)
    {
        Debug.Log("마우스업  ");
        if (RT.anchoredPosition.x > -20 && RT.anchoredPosition.x < 20 && RT.anchoredPosition.y > -20 && RT.anchoredPosition.y < 20)
        {
            RT.anchoredPosition = Vector2.zero;
            MissionManager.Instance.MissionClear(MinigamePanel);
        }
    }
}

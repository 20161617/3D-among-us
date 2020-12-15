using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MissionManager;

public class MiniGame_EmptyGarbage : MonoBehaviour // 쓰레기버리기 
{
    public GameObject colliderUnder;
    public GameObject MinigamePanel;

    private void OnEnable()
    {
        colliderUnder.SetActive(true);
    }
    public void startGarbage()
    {
        StartCoroutine(Garbage());
    }

    IEnumerator Garbage()
    {

        colliderUnder.SetActive(false);
        yield return new WaitForSeconds(3.0f);
        missionManager.MissionClear(MinigamePanel);

    }


}

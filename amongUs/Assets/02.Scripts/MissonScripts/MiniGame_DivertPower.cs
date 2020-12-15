using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MissionManager;

public class MiniGame_DivertPower : MonoBehaviour
{
    public bool clear { get; private set; }
    public GameObject MinigamePanel;

    private void OnEnable()
    {
        clear = false;
    }
    public void ClearDivertPower()
    {
        clear = true;
        missionManager.MissionClear(MinigamePanel);
    }

}

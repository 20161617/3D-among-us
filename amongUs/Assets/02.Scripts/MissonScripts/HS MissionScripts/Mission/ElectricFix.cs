using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ElectricFix : Mission
{
    public void Awake()
    {
        missionText = placeReturn() + ": 배선 수리하기" + "(0 /" + missionCount + ")";
    }
    public override void setMission()
    {

    }
    public override void getMission()
    {

    }
    public override string getText()
    {
        return missionText;
    }
}

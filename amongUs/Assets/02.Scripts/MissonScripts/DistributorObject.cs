using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistributorObject : MonoBehaviour
{
    public RectTransform circle;
    public Image gague;
    public GameObject point;
    public ClickButton button;

    const float FillMax = 1.0f;
    const float FillMin = 0.1f;
    const int circlePosMax = 160;
    const int circlePosMin = 230;


    public void MissionSet()//초기화 
    {
        circle.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(circlePosMin, circlePosMax)));
        point.SetActive(false);
        gague.fillAmount = FillMin;
        button.ButtonSet();
    }
  
    public void LightUP(bool isUP)
    {
          if(isUP)
        {
            point.SetActive(true);
            gague.fillAmount = FillMax;
        }
        else
        {
            point.SetActive(false);
            gague.fillAmount = FillMin;

        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UploadGague : MonoBehaviour
{
    public Image fillGague;
    public Text gagueText;


    private void OnEnable()
    {
        fillGague.fillAmount = 0;
    }
    public void gagueSet(bool set)
    {
        gameObject.SetActive(set);
    }
    public void FillGague()
    {
        fillGague.fillAmount += 0.1f;
        gagueText.text = (int)(fillGague.fillAmount * 100)+"%";
    }
   
}

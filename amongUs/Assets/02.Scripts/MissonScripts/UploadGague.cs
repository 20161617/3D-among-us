using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UploadGague : MonoBehaviour
{
    public Image fillGague;
    public Text gagueText;


    public void gagueSet(bool set)
    {
        gameObject.SetActive(set);
    }
    public void FillGague()
    {
        fillGague.fillAmount += 0.1f;
        gagueText.text = (int)(fillGague.fillAmount * 100)+"%";
    }
        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

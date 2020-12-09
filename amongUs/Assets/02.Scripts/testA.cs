using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testA : MonoBehaviour
{
    Text _text;
    public Image gage;
    private void Start()
    {
        _text = GetComponent<Text>();
    }
    private void Update()
    {
        _text.text = "Fill Amount: " + gage.fillAmount;
    }
}

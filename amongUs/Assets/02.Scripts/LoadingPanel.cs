using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingPanel : MonoBehaviour
{
    public GameObject panel;

    private void Awake()
    {
        StartCoroutine("PanelActive");
    }

    IEnumerator PanelActive()
    {
        yield return new WaitForSeconds(1.2f);
        panel.SetActive(false);
    }
}

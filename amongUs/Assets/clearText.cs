using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clearText : MonoBehaviour
{
    private RectTransform rect;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }
    public RectTransform GetRect()
    {
        return rect;
    }
    public void setRect(Vector3 _pos)
    {
        rect.transform.position = _pos;
    }
}

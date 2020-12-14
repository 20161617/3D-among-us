using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DivertButton : MonoBehaviour, IDragHandler
{
    public Image gagueFill;
    const int gripMax = 200;
    const int gripMin = 100;

    private Vector2 posInit;

    private void Awake()
    {
        posInit = transform.localPosition;

    }
    private void OnEnable()
    {
        transform.localPosition = posInit;
        gagueFill.fillAmount = 0.5f;

    }
    // Start is called before the first frame update

    public void OnDrag(PointerEventData data)
    {
        if (transform.parent.GetComponent<MiniGame_DivertPower>().clear)
            return;

        if (data.position.y > gripMax)
        {
            transform.position = new Vector2(transform.position.x, gripMax);
            transform.parent.GetComponent<MiniGame_DivertPower>().ClearDivertPower();
        }
        else if (data.position.y < gripMin)
        {
            transform.position = new Vector2(transform.position.x, gripMin);
        }
        else
        {
            transform.position = new Vector2(transform.position.x, data.position.y);
        }
        gagueFill.fillAmount = (transform.position.y - 100) * 0.01f;
    }

}

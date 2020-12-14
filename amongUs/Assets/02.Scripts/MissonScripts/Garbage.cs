using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage : MonoBehaviour
{
    public Vector3 pos;
    private void Awake()
    {
        pos = new Vector3(gameObject.GetComponent<RectTransform>().anchoredPosition.x, gameObject.GetComponent<RectTransform>().anchoredPosition.y, 0);
        Debug.Log("awake");
    }
    private void OnEnable()
    {
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
       // gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        transform.position = pos;
        gameObject.GetComponent<RectTransform>().anchoredPosition = pos;

       // Debug.Log("쓰레기 초기화");
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

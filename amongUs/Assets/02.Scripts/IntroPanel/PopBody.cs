using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopBody : MonoBehaviour
{
    private int intTime;

    public GameObject Pinger;

    // Start is called before the first frame update
    void Start()
    {
        intTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Pinger.transform.eulerAngles.z == 0 && intTime == 0)
            transform.localScale = new Vector3(1.1f, 1.1f, 1);
        else if (Pinger.transform.eulerAngles.z == 0 && intTime == 10)
            transform.localScale = new Vector3(1, 1, 1);
        if (Pinger.transform.eulerAngles.z == 0 && intTime != 11)
            intTime++;

    }
}

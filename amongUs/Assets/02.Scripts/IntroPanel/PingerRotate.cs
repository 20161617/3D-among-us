using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingerRotate : MonoBehaviour
{
    public GameObject SHH;
    private int PingerRotation;
    // Start is called before the first frame update
    void Start()
    {
        PingerRotation = 0;
        transform.rotation = Quaternion.Euler(0, 0, -90);
    }

    // Update is called once per frame
    void Update()
    {
        if (PingerRotation != 151)
            PingerRotation++;
        if (PingerRotation <= 150)
            transform.Rotate(new Vector3(0, 0, 0.6f));
        if (PingerRotation == 150)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            SHH.SetActive(true);
        }
    }
}

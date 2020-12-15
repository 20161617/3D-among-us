using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime,Space.World);

        transform.Rotate(new Vector3(10f, 20f, 30f) * Time.deltaTime, Space.World);
    }
}

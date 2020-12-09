using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightBlackMove : MonoBehaviour
{
    float position = 0.0f;
    float velocity = 0.0f;
    float acceleration = 0.001f;
    private Transform tr;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity += acceleration;
        position += velocity;

        Vector3 movement = new Vector3(position, 0.0f, 0.0f);

        if (position < 60.0f)
            tr.Translate(movement * Time.deltaTime * 0.1f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScreen : MonoBehaviour 
{
    public AsteroidCusor cusor;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)&&Input.mousePosition.x>110&&Input.mousePosition.x<510&&Input.mousePosition.y>100&& Input.mousePosition.y<400)
        {
            cusor.setPos(Input.mousePosition);
            StartCoroutine(colliderCheck());
        }

    }
    IEnumerator colliderCheck()
    {
        cusor.setColider(true);
        yield return new WaitForSeconds(0.1f);
        cusor.setColider(false);
        yield return null;
    }
}

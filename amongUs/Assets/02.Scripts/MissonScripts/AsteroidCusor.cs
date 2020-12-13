using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCusor : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        GetComponent<BoxCollider>().enabled = false;
        transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    }
  
    public void setPos(Vector3 pos)
    {
        transform.position = pos;
    }
    public void setColider(bool set)
    {
        GetComponent<BoxCollider>().enabled = set;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Asteroid" && !other.GetComponent<MiniGame_Asteroid>().dead&& !transform.parent.GetComponent<MiniGame_ClearAsteroids>().isClear)
        {
            other.GetComponent<MiniGame_Asteroid>().destroyAsteroid(); //격추 
            transform.parent.GetComponent<MiniGame_ClearAsteroids>().upScore();
        }
    }


}

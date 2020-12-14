using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame_Asteroid : MonoBehaviour
{
    public Vector3 target = new Vector3(-500, 0, 0);
    private Vector3 posInit = new Vector3(300, 0, 0);
    private Vector2 arrivalPosH = new Vector2(-250, 250);

    private float speed;

    private int select; //현재 스프라이트 
    public bool dead { get; private set; }

    public Sprite [] asteroidSprite;
    public Sprite[] destroysprite ;
    public Sprite explosionSprite;

    private void OnEnable()
    {
        dead = false;
        select = Random.Range(0, asteroidSprite.Length-1);
        
        gameObject.GetComponent<Image>().sprite = asteroidSprite[select];

        posInit.y = Random.Range(-300, 300);
        transform.GetComponent<RectTransform>().anchoredPosition = posInit;
        target.y= Random.Range(arrivalPosH.x, arrivalPosH.y);
        speed = Random.Range(300, 800);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void destroyAsteroid()
    {
        StartCoroutine(startDestroy());
    }
    IEnumerator startDestroy()
    {
        dead = true;
        gameObject.GetComponent<Image>().sprite = explosionSprite;
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<Image>().sprite = destroysprite[select];
        yield return new WaitForSeconds(0.2f);
        OnEnable();
      
        yield return null;
    }
   
    // Update is called once per frame
    void Update()
    {
        if (dead)
            return;
        transform.GetComponent<RectTransform>().anchoredPosition= Vector3.MoveTowards(transform.GetComponent<RectTransform>().anchoredPosition, target, speed * Time.deltaTime);
        if (transform.GetComponent<RectTransform>().anchoredPosition.x <= target.x)
            OnEnable();
    }

  
}

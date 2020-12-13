using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionTimer : MonoBehaviour
{
    
    public static float time;

    private bool start;
    public bool timeEnd;
  
    public void TimerSet(float _time)
    {
        time = _time;
        start = true;
    }
    public int TimeGet()
    {
        return (int)time;
    }
    // Start is called before the first frame update
    void Start()
    {
        start = false;
        timeEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(start)
        {
            if(time>0)
            time -= Time.deltaTime;
            else
            {
                time = 0;
                timeEnd = true;
                start = false;
            }    

        }
    }
}

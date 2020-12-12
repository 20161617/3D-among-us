using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MiniGame_Distributor : MonoBehaviour
{
    [SerializeField]
    public DistributorObject yellow;
    public DistributorObject blue;
    public DistributorObject skyBlue;

    public int a = 0; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        a++;
        yellow.circle.transform.rotation = Quaternion.Euler(new Vector3(0, 0,a ));

    }
}

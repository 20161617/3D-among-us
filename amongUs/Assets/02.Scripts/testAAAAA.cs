using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using static PhotonInit;


public class testAAAAA : MonoBehaviourPun
{
    public PhotonView PV;

    private void Awake()
    {
        PV = photonView;
    }

    [PunRPC]
    void testA()
    {
        int a = 0;
        Debug.Log("시바아아아아알");
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //PV.RPC("testA", RpcTarget.AllViaServer);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using static NetworkManager;

public class SpawnCenter : MonoBehaviour
{


    Vector3 Center;

    int playerNum = NetInstance.Players.Count;
    // Start is called before the first frame update
    void Start()
    {

        float radian = 0;

        Center = gameObject.transform.position;
        for (int i = 0; i < playerNum; i++)
        {
            radian = (360 / playerNum * i) * Mathf.PI / 180;

            NetInstance.Players[i].GetComponent<PhotonView>().RPC("SetMyPosition",
                RpcTarget.AllViaServer,
                new Vector3(Center.x + 3 * Mathf.Cos(radian), Center.y, Center.z + 3 * Mathf.Sin(radian)));
            //Instantiate(SpawnPoint, new Vector3(Center.x + 3 * Mathf.Cos(radian), Center.y, Center.z + 3 * Mathf.Sin(radian)), Quaternion.identity);
        }
        Debug.Log(Center.x + 3 * Mathf.Cos(radian));
        Debug.Log(Center.z + 3 * Mathf.Sin(radian));
        //반지름 3을 기준으로 플레이어 수만큼 스폰포인트 소환
    }

    // Update is called once per frame
    void Update()
    {

    }
}

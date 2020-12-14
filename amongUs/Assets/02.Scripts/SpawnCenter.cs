using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using static DatabaseManager;

public class SpawnCenter : MonoBehaviour
{
    Vector3 Center;

    int playerNum;

    // Start is called before the first frame update
    void Start()
    {
        playerNum = databaseManager.Players.Count;

        float radian = 0;

        Center = gameObject.transform.position;

        for (int i = 0; i < playerNum; i++)
        {
            radian = (360 / playerNum * i) * Mathf.PI / 180;

            databaseManager.Players[i].GetComponent<PhotonView>().RPC("SetMyPosition",
                RpcTarget.AllViaServer,
                new Vector3(Center.x + 3 * Mathf.Cos(radian), Center.y, Center.z + 3 * Mathf.Sin(radian)));

        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetPosition()
    {
        playerNum = databaseManager.Players.Count;

        float radian = 0;

        Center = gameObject.transform.position;

        for (int i = 0; i < playerNum; i++)
        {
            radian = (360 / playerNum * i) * Mathf.PI / 180;

            databaseManager.Players[i].GetComponent<PhotonView>().RPC("SetMyPosition",
                RpcTarget.AllViaServer,
                new Vector3(Center.x + 3 * Mathf.Cos(radian), Center.y, Center.z + 3 * Mathf.Sin(radian)));

        }
    }
}

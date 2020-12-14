using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using static DatabaseManager;

public class DetectDead : MonoBehaviourPunCallbacks
{
    PhotonView PV;

    void Awake()
    {
        PV = photonView;
    }


    private void OnTriggerEnter(Collider Target)
    {
        if (!PV.IsMine)
            return;
        Debug.Log("Enter");
        if (Target.CompareTag("DEAD"))
        {
            databaseManager.MyPlayer.OnDetected();
        }
    }

    private void OnTriggerExit(Collider Target)
    {
        if (!PV.IsMine)
            return;
        Debug.Log("Exit");
        if (Target.CompareTag("DEAD"))
        {
            databaseManager.MyPlayer.OffDetected();
        }
    }

    private void OnTriggerStay(Collider Target)
    {
        if (!PV.IsMine)
            return;
        Debug.Log("Stay");
        if (Target.CompareTag("DEAD"))
        {
            databaseManager.MyPlayer.OnDetected();
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using static UIManager;
using static NetworkManager;

public class PlayerScript : MonoBehaviourPunCallbacks
{
    PhotonView PV;
    // Start is called before the first frame update
    void Start()
    {
        PV = photonView;
        NetInstance.Players.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine) return;
    }
    void OnDestroy()
    {
        NetInstance.Players.Remove(this);
    }
}

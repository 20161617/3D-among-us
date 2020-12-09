using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using static UIManager;
using static NetworkManager;

public class PlayerScript : MonoBehaviourPunCallbacks
{
    public bool isImposter;
    PhotonView PV;
    // Start is called before the first frame update
    void Awake()
    {
        PV = photonView;
        NetInstance.Players.Add(this);
       // gameObject.SetActive(false);
        DontDestroyOnLoad(gameObject);
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
    [PunRPC]
    void SetImpoCrew(bool _isImposter)
    {
        isImposter = _isImposter;
    }
}

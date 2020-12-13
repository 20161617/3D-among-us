using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using static UIManager;
using static NetworkManager;

public class TestPlayerScript : MonoBehaviourPunCallbacks
{
    public bool isImposter;
    PhotonView PV;
    // Start is called before the first frame update
    void Awake()
    {
        PV = photonView;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine) return;
    }
    [PunRPC]
    void ShowCharacter()
    {
        gameObject.SetActive(true);
    }

    [PunRPC]
    void SetImpoCrew(bool _isImposter)
    {
        isImposter = _isImposter;
    }

    [PunRPC]
    void SetMyPosition(Vector3 postion)
    {
        gameObject.transform.position = postion;
    }
}

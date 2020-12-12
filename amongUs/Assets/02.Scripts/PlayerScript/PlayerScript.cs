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
    public SkinnedMeshRenderer color;
    public int colorIndex = -1;

    PhotonView PV;

    // Start is called before the first frame update
    void Awake()
    {
        PV = photonView;
        DatabaseManager.databaseManager.Players.Add(this);

        color = gameObject.transform.Find("Beta_Surface").GetComponent<SkinnedMeshRenderer>();

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine) return;
    }

    void OnDestroy()
    {
        DatabaseManager.databaseManager.Players.Remove(this);
    }

    [PunRPC]
    void SetImpoCrew(bool _isImposter)
    {
        isImposter = _isImposter;
    }

    [PunRPC]
    public void SetColor(int _colorIndex)
    {
        color.material = DatabaseManager.databaseManager.Colors[_colorIndex];
        colorIndex = _colorIndex;
    }
}

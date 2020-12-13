using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using static UIManager;
using static DatabaseManager;

public class PlayerScript : MonoBehaviourPunCallbacks
{
    public bool isImposter;
    public SkinnedMeshRenderer color;

    public int colorIndex = -1;
    public string nickName;

    PhotonView PV;

    // Start is called before the first frame update
    void Awake()
    {
        PV = photonView;
   

        color = gameObject.transform.Find("Beta_Surface").GetComponent<SkinnedMeshRenderer>();

        nickName = photonView.Owner.NickName;

        databaseManager.Players.Add(this);

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine) return;
    }

    void OnDestroy()
    {
        databaseManager.Players.Remove(this);
    }

    [PunRPC]
    void SetImpoCrew(bool _isImposter)
    {
        isImposter = _isImposter;
    }

    [PunRPC]
    public void SetColor(int _colorIndex)
    {
        color.material = databaseManager.Colors[_colorIndex];
        colorIndex = _colorIndex;
    }

    [PunRPC]
    void GameStartRPC()
    {
        gameObject.SetActive(false);
    }

    [PunRPC]
    void ShowCharacter()
    {
        gameObject.SetActive(true);
    }

    [PunRPC]
    void SetMyPosition(Vector3 postion)
    {
        gameObject.transform.position = postion;
    }
}

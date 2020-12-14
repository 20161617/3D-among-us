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

    PhotonView PV;
    public TargetCtrl targetCtrl;

    // Start is called before the first frame update
    void Awake()
    {
        PV = photonView;
        databaseManager.Players.Add(this);

        color = gameObject.transform.Find("Beta_Surface").GetComponent<SkinnedMeshRenderer>();

        targetCtrl = gameObject.GetComponent<TargetCtrl>();

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
    void HideCharacter()
    {
        Debug.Log("플레이어 비활성화!");
        gameObject.SetActive(false);
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

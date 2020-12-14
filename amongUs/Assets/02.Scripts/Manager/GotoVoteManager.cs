using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GotoVoteManager : MonoBehaviourPun
{
    public List<GameObject> ReportPanel = new List<GameObject>();


    IEnumerator GotoVote()
    {
        yield return new WaitForSeconds(1.0f);
        PhotonNetwork.LoadLevel("VoteScene");
    }

    [PunRPC]
    public void ActivePanel(int index)
    {
        ReportPanel[index].SetActive(true);
        StartCoroutine("GotoVote");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class GotoVoteManager : MonoBehaviourPun
{
    public List<GameObject> ReportPanel = new List<GameObject>();

    int index;

    public void Report(int i)
    {
        photonView.RPC("ActivePanel", RpcTarget.AllViaServer, i);
    }

    IEnumerator GotoVote()
    {
        yield return new WaitForSeconds(1.0f);
        ReportPanel[index].SetActive(false);
        SceneManager.LoadScene("VoteScene", LoadSceneMode.Additive);
    }

    [PunRPC]
    public void ActivePanel(int index)
    {
        this.index = index;
        ReportPanel[index].SetActive(true);
        StartCoroutine("GotoVote");
    }
}

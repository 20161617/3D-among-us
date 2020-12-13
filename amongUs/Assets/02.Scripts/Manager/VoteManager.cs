using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class VoteManager : MonoBehaviourPun
{
    [Header("VotePanel")]
    public GameObject VotePanel;
    public GameObject loadingPandel;
    public List<GameObject> VoteComponents = new List<GameObject>();
    public Button SkipVoteBtn;


    private int reporterIndex = 0;
    private int[] voteIndex = new int[11];


    private bool isAllVoted;
    private int voteCount = 0;


    private void Update()
    {
        AllVotedCheck();

        if (isAllVoted)
        {
            ExitVote();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            OnVotePanel();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitVote();
        }
    }

    public void SortPlayer()
    {
        DatabaseManager.databaseManager.Players.Sort((p1, p2) => p1.colorIndex.CompareTo(p2.colorIndex));
    }

    public void OnVotePanel()
    {
        StartCoroutine("Loading");

        VotePanel.SetActive(true);

        SortPlayer();


        for (int i=0; i< VoteComponents.Count; i++)
        {
            bool isExist = i < DatabaseManager.databaseManager.Players.Count;
            VoteComponents[i].SetActive(isExist);

            if (isExist)
            {
                GameObject icon = VoteComponents[i].transform.GetChild(0).gameObject;
                
                icon.GetComponentInChildren<Text>().text = DatabaseManager.databaseManager.Players[i].nickName;
                icon.GetComponent<Image>().color = DatabaseManager.databaseManager.SetColorIcon(DatabaseManager.databaseManager.Players[i].colorIndex);
       
                if (reporterIndex == i) VoteComponents[i].transform.GetChild(3).gameObject.SetActive(true);
            }
        }

    }

    public void ExitVote()
    {
        VotePanel.SetActive(false);

        for(int i=0; i< VoteComponents.Count; i++)
        {
            VoteComponents[i].SetActive(false);
        }
    }


    IEnumerator Loading()
    {
        loadingPandel.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        loadingPandel.SetActive(false);
    }


    //프로필 파넬 선택
    public void SelectProfile(int num)
    {
        GameObject icon = VoteComponents[num].transform.GetChild(0).gameObject;

        if (icon.GetComponentInChildren<Text>().text != DatabaseManager.databaseManager.MyPlayer.nickName)
        {
            VoteComponents[num].transform.GetChild(1).gameObject.SetActive(true);
            VoteComponents[num].transform.GetChild(2).gameObject.SetActive(true);

            if (reporterIndex == num) VoteComponents[num].transform.GetChild(3).gameObject.SetActive(false);
        }
    }

    //투표
    public void OnVote(int num)
    {
        voteIndex[num] += 1;

        for (int i = 0; i < DatabaseManager.databaseManager.Players.Count; i++)
        {
            GameObject icon = VoteComponents[i].transform.GetChild(0).gameObject;
   
            if (icon.GetComponentInChildren<Text>().text == PhotonNetwork.NickName)
            {
                photonView.RPC("IVoted", RpcTarget.AllBuffered, i);
            }

            VoteComponents[i].GetComponent<Button>().interactable = false;
        }

        SkipVoteBtn.interactable = false;
    }

    //투표 취소
    public void VoteCancel(int num)
    {
        VoteComponents[num].transform.GetChild(1).gameObject.SetActive(false);
        VoteComponents[num].transform.GetChild(2).gameObject.SetActive(false);

        if (reporterIndex == num) VoteComponents[num].transform.GetChild(3).gameObject.SetActive(true);
    }

    void AllVotedCheck()
    {
        if (voteCount >= DatabaseManager.databaseManager.Players.Count)
        {
            isAllVoted = true;
        }
    }

    void ShowVoteResukt()
    {

    }

    [PunRPC]
    public void IVoted(int num)
    {
        VoteComponents[num].transform.GetChild(4).gameObject.SetActive(true);
        voteCount++;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using static DatabaseManager;
using static GameSceneManager;

public class VoteManager : MonoBehaviourPun
{
    [Header("VotePanel")]
    public GameObject VotePanel;
    public GameObject loadingPandel;
    public List<GameObject> VoteComponents = new List<GameObject>();
    public Button SkipVoteBtn;
    public Text timerText;

    private int voteTimer;

    private Dictionary<int, int> voteDictionary;
    private int reporterIndex;
    private bool isAllVoted;
    private int voteCount;

    //임시 변수임;;;;
    private int dieCount;

    private void Awake()
    {
        voteCount = 0;
        reporterIndex = 0;
        voteDictionary = new Dictionary<int, int>();
        OnVotePanel();
    }


    private void Update()
    {
        AllVotedCheck();

        if (isAllVoted)
        {
            voteCount = 0;
            isAllVoted = false;

            StopCoroutine("VoteTimerCo");
            timerText.text = "투표 발표";

            photonView.RPC("ShowVoteResult", RpcTarget.AllViaServer);


        }
    }

    public void SortPlayer()
    {
        databaseManager.Players.Sort((p1, p2) => p1.colorIndex.CompareTo(p2.colorIndex));
        databaseManager.Players.Sort((p1, p2) => p1.playerState.CompareTo(p2.playerState));
    }

    public void OnVotePanel()
    {

        StartCoroutine("Loading");

        for(int i=0; i<databaseManager.Players.Count; i++)
        {
            if (databaseManager.Players[i].playerState == PLAYER_STATE.DEAD || !databaseManager.Players[i].isAlive)
            {
                databaseManager.Players[i].photonView.RPC("SetDie", RpcTarget.AllBuffered);
            }
        }

        VotePanel.SetActive(true);

        SortPlayer();

        voteTimer = VOTE_TIME;

        StartCoroutine("VoteTimerCo");

        for (int i = 0; i < VoteComponents.Count; i++)
        {
            bool isExist = i < databaseManager.Players.Count;
            VoteComponents[i].SetActive(isExist);


            if (isExist)
            {
                if (databaseManager.MyPlayer.playerState == PLAYER_STATE.DEAD || !databaseManager.MyPlayer.isAlive)
                {

                    VoteComponents[i].GetComponent<Button>().interactable = false;
                }


                GameObject icon = VoteComponents[i].transform.GetChild(0).gameObject;
                icon.GetComponentInChildren<Text>().text = databaseManager.Players[i].nickName;
                icon.GetComponent<Image>().color = databaseManager.SetColorIcon(databaseManager.Players[i].colorIndex);

                if (reporterIndex == i) VoteComponents[i].transform.GetChild(3).gameObject.SetActive(true);

                if (databaseManager.Players[i].playerState == PLAYER_STATE.DEAD)
                {
                    dieCount++;

                    icon.transform.GetChild(2).gameObject.SetActive(true);
                    VoteComponents[i].GetComponent<Button>().interactable = false;
                }
            }
        }
    }

    public void ExitVote()
    {
        VotePanel.SetActive(false);

        for (int i = 0; i < VoteComponents.Count; i++)
        {
            VoteComponents[i].SetActive(false);
        }
    }


    IEnumerator Loading()
    {
        loadingPandel.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        loadingPandel.SetActive(false);
    }


    //프로필 파넬 선택
    public void SelectProfile(int num)
    {
        GameObject icon = VoteComponents[num].transform.GetChild(0).gameObject;

        if (icon.GetComponentInChildren<Text>().text != databaseManager.MyPlayer.nickName)
        {
            VoteComponents[num].transform.GetChild(1).gameObject.SetActive(true);
            VoteComponents[num].transform.GetChild(2).gameObject.SetActive(true);

            if (reporterIndex == num) VoteComponents[num].transform.GetChild(3).gameObject.SetActive(false);
        }
    }

    //투표
    public void OnVote(int num)
    {
        for (int i = 0; i < databaseManager.Players.Count; i++)
        {
            GameObject icon = VoteComponents[i].transform.GetChild(0).gameObject;

            if (icon.GetComponentInChildren<Text>().text == PhotonNetwork.NickName)
            {
                photonView.RPC("IVoted", RpcTarget.AllBuffered, num, i);
            }

            VoteComponents[i].transform.GetChild(1).gameObject.SetActive(false);
            VoteComponents[i].transform.GetChild(2).gameObject.SetActive(false);

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
        if (voteCount >= databaseManager.Players.Count - dieCount)
        {
            isAllVoted = true;
        }
    }

    [PunRPC]
    public void ShowVoteResult()
    {
        StartCoroutine("ShowVoteResultCorou");
    }


    [PunRPC]
    public void IVoted(int youIndex, int myIndex)
    {

        //결과 시 보여줄 icon
        GameObject iconPrefab = Resources.Load("Icon") as GameObject;
        GameObject temp = Instantiate(iconPrefab);

        //마지막 인덱스는 skipBtn이 들어감
        if (youIndex == VoteComponents.Count)
        {
            temp.transform.SetParent(SkipVoteBtn.transform.GetChild(1));
        }
        else
        {
            temp.transform.SetParent(VoteComponents[youIndex].transform.GetChild(5));
        }

        // 사이즈 1로 초기화
        temp.transform.localScale = Vector3.one;

        //컬러 설정
        temp.GetComponent<Image>().color = databaseManager.SetColorIcon(databaseManager.Players[myIndex].colorIndex);

        //비활성화
        temp.gameObject.SetActive(false);

        //voteIcon 표시
        VoteComponents[myIndex].transform.GetChild(4).gameObject.SetActive(true);

        //총 투표 인원
        voteCount++;
    }


    IEnumerator ShowVoteResultCorou()
    {
        for (int i = 0; i < databaseManager.Players.Count; i++)
        {
            for (int j = 0; j < VoteComponents.Count; j++)
            {
                if (VoteComponents[j].transform.GetChild(5).childCount > i)
                {
                    VoteComponents[j].transform.GetChild(5).GetChild(i).gameObject.SetActive(true);
                }
            }

            if (SkipVoteBtn.transform.GetChild(1).childCount > i)
            {
                SkipVoteBtn.transform.GetChild(1).GetChild(i).gameObject.SetActive(true);
            }

            //딜레이
            yield return new WaitForSeconds(0.5f);
        }

        //값 저장
        for (int i = 0; i < databaseManager.Players.Count; i++)
        {
            voteDictionary.Add(i, VoteComponents[i].transform.GetChild(5).childCount);
        }

        //값 정렬
        var max = voteDictionary.OrderByDescending(x => x.Value);

        //스킵 수와 비교
        bool exit;


        if (max.ElementAt(0).Value == max.ElementAt(1).Value) exit = false;
        else exit = max.First().Value > SkipVoteBtn.transform.GetChild(1).childCount;

        //스킵 수보다 많다면 퇴출
        if (exit)
        {
            databaseManager.deportMember = max.First().Key;
            //DatabaseManager.databaseManager.Players[max.First().Key].SetState(PLAYER_STATE.DEAD);
        }
        else
        {
            databaseManager.deportMember = -1;
        }

        databaseManager.isEvent = true;

        for (int i = 0; i < databaseManager.Players.Count; i++)
            databaseManager.Players[i].GetComponent<PhotonView>().RPC("HideCharacter", RpcTarget.AllViaServer);

        GameInstance.CameraOn();
        Scene scene = SceneManager.GetSceneByBuildIndex(5);
        SceneManager.UnloadSceneAsync(scene);
    }


    IEnumerator VoteTimerCo()
    {
        int _voteTimer = voteTimer;

        for (int i = _voteTimer; i >= 0; i--)
        {
            voteTimer = i;
            timerText.text = $"투표 종료까지: {voteTimer}초";
            yield return new WaitForSeconds(1);
        }

        timerText.text = "투표 발표";
        for (int i = 0; i < databaseManager.Players.Count; i++)
        {
            VoteComponents[i].GetComponent<Button>().interactable = false;
        }

        photonView.RPC("ShowVoteResult", RpcTarget.AllViaServer);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static DatabaseManager;
using static GameSceneManager;

public class ChatController : MonoBehaviour
{
    public Text ChatText;
    public Text ChatText2;

    public string writerText = "";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Loading());
    }

    IEnumerator Loading()
    {
        yield return new WaitForSeconds(3f);

        int impoCount = 0;

        for (int i = 0; i < databaseManager.Players.Count; i++)
        {
            if (databaseManager.Players[i].isImposter && databaseManager.Players[i].playerState != PLAYER_STATE.DEAD)
            {
                impoCount++;
            }
        }
        databaseManager.impoCount = impoCount;

        if (databaseManager.deportMember == -1)
        {
            StartCoroutine(NormalChat("아무도 추방되지 않았습니다..", "임포스터는 " + databaseManager.impoCount.ToString() + "명 남았습니다.."));
        }
        else
        {
            int _deportMember = databaseManager.deportMember;

            string name = databaseManager.Players[_deportMember].nickName + "님은 ";

            string impo;

            if (databaseManager.Players[_deportMember].isImposter)
            {
                impo = "임포스터 입니다..";
                databaseManager.impoCount--;
            }
            else
            {
                impo = "임포스터가 아닙니다..";
            }

            databaseManager.Players[_deportMember].SetState(PLAYER_STATE.DEAD);

            StartCoroutine(NormalChat(name + impo, "임포스터는 " + databaseManager.impoCount.ToString() + "명 남았습니다.."));
        }
    }

    IEnumerator NormalChat(string narration, string narration2)
    {
        int a = 0;
        writerText = "";

        //텍스트 타이핑 효과
        for (a = 0; a < narration.Length; a++)
        {
            writerText += narration[a];
            ChatText.text = writerText;
            yield return new WaitForSeconds(0.15f);
        }


        writerText = "";
        //텍스트 타이핑 효과
        for (a = 0; a < narration2.Length; a++)
        {
            writerText += narration2[a];
            ChatText2.text = writerText;
            yield return new WaitForSeconds(0.15f);
        }

        yield return new WaitForSeconds(2f);

        for (int i = 0; i < databaseManager.Players.Count; i++)
            databaseManager.Players[i].GetComponent<PhotonView>().RPC("ShowCharacter", RpcTarget.AllViaServer);


        GameInstance.CameraOn();
        Scene scene = SceneManager.GetSceneByBuildIndex(6);
        SceneManager.UnloadSceneAsync(scene);
    }
}

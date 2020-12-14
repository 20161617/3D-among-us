using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using static NetworkManager;

public class IntroPanel : MonoBehaviour
{
    public GameObject GameSceneCamera;
    // Start is called before the first frame update
    void Start()
    {
        //5초후 팀배정
        Invoke("AssignTeam", 5);
    }


    //팀배정 함수
    void AssignTeam()
    {
        //내가 임포스터라면
        if (DatabaseManager.databaseManager.MyPlayer.isImposter)
        {
            //임포스터 팀 씬 로드
            SceneManager.LoadScene("Impo Team", LoadSceneMode.Additive);
        }
        //내가 크루원이라면
        else
        {
            //크루원 팀 씬 로드
            SceneManager.LoadScene("Crew Team", LoadSceneMode.Additive);
        }
        GameSceneCamera.SetActive(false);
        Destroy(this.gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static MissionManager;

public class MiniGame_ClearAsteroids : MonoBehaviour
{
    public GameObject[] obj;
    public GameObject MinigamePanel;
    public Text DestroyedText;
    public const int scoreMax = 20;
    public int scoreNow;
    public bool isClear { get; private set; }
    // Start is called before the first frame update
    private void OnEnable()
    {
        scoreNow = 0;
        isClear = false;
        StartCoroutine(startClearAsteroids());
        DestroyedText.text = "Destroyed : " + scoreNow + " / " + scoreMax;
    }
    public void upScore()
    {
        Debug.Log("upscore");
        scoreNow++;
        DestroyedText.text = "Destroyed : " + scoreNow + " / " + scoreMax;
        if (scoreNow >= scoreMax)
        {
            missionManager.MissionClear(MinigamePanel);
            isClear = true;

            for (int i = 0; i < obj.Length; i++)
                obj[i].SetActive(false);


        }
    }
    IEnumerator startClearAsteroids()
    {
        for (int i = 0; i < obj.Length; i++)
        {
            obj[i].SetActive(true);
            yield return new WaitForSeconds(2.5f); //생성속도
        }
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

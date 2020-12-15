using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static MissionManager;

public class MiniGame_InspectSample : MonoBehaviour
{
    private enum Status
    {
        BeforeStart, //시작전 
        Waiting, //시간 기다리기 
        Select, //선택 
        End, //끝남 
    }
    public GameObject syringe;
    public GameObject startButton;
    public GameObject selectButton;
    public Text underText;
    public Text overText;

    public Image[] waterImage;
    public GameObject box;
    public int painWater { get; private set; }
    public GameObject MinigamePanel;
    public Vector3 pos;
    Vector3 posInit;
    private Status status;
    private bool move;
    public bool down;
    const string underBeforeText = "터치를 해서 시작 하세요 ->";
    const string underStartingText = "시약추가중";
    const string underWaitingText = "커피나 한잔 하고 오시죠";
    const string underEndText = "이상 표본을 선택 하세요";

    private void Awake()
    {
        status = Status.BeforeStart;
        posInit = syringe.transform.localPosition;
    }
    private void OnEnable()
    {
        if (status == Status.BeforeStart || status == Status.End) //아직 아무것도 안눌렀을떄 
        {

            syringe.transform.localPosition = posInit;
            selectButton.SetActive(false);
            startButton.SetActive(true);
            overText.text = " ";
            underText.text = underBeforeText;
            for (int i = 0; i < waterImage.Length; i++)
            {
                waterImage[i].color = new Color(255, 255, 255);
            }
        }
        else if (status == Status.Waiting && transform.parent.GetComponent<MissionTimer>().timeEnd)
        {
            selectWater();
        }

    }
    public void StartSample()
    {
        underText.text = underStartingText;
        StartCoroutine(AddWater());
    }
    IEnumerator timeSet()
    {
        yield return null;
    }
    IEnumerator AddWater()
    {
        Vector3 target = syringe.transform.localPosition;
        for (int i = 0; i < 5; i++)
        {
            syringe.transform.localPosition = target;
            waterImage[i].color = new Color(0, 0, 255); //파란색 시약 추가 
            target.x += 120;
            yield return new WaitForSeconds(0.5f);
        }
        target.x = -240;
        syringe.transform.localPosition = target;
        pos = new Vector3(box.GetComponent<RectTransform>().anchoredPosition.x, box.GetComponent<RectTransform>().anchoredPosition.y - 500, 0);
        transform.parent.GetComponent<MissionTimer>().TimerSet(60f);
        down = move = true;
        underText.text = underWaitingText;
        status = Status.Waiting;
        yield return null;
    }
    public void SelectWater(int num)
    {
        if (num != painWater) //틀렷을때 
        {

            syringe.transform.localPosition = posInit;
            selectButton.SetActive(false);
            startButton.SetActive(true);
            overText.text = " ";
            underText.text = underBeforeText;
            for (int i = 0; i < waterImage.Length; i++)
            {
                waterImage[i].color = new Color(255, 255, 255);
            }
            transform.parent.GetComponent<MissionTimer>().timeEnd = false;

            status = Status.BeforeStart;
        }
        else //맞췄을떄 
        {
            missionManager.MissionClear(MinigamePanel);
            status = Status.End;
            transform.parent.GetComponent<MissionTimer>().timeEnd = false;


        }
    }
    public void moveBox()
    {
        if (!move)
            return;

        if (down)
        {
            box.GetComponent<RectTransform>().anchoredPosition = Vector3.MoveTowards(box.GetComponent<RectTransform>().anchoredPosition, pos, 300 * Time.deltaTime);
            if (box.GetComponent<RectTransform>().anchoredPosition.y <= -500)
                move = false;
        }
        else
        {
            box.GetComponent<RectTransform>().anchoredPosition = Vector3.MoveTowards(box.GetComponent<RectTransform>().anchoredPosition, pos, 300 * Time.deltaTime);
            if (box.GetComponent<RectTransform>().anchoredPosition.y >= 0)
                move = false;
        }


    }
    public void selectWater()
    {
        move = true;
        down = false;
        pos.y = 0;
        underText.text = underEndText;
        painWater = Random.Range(0, waterImage.Length);
        waterImage[painWater].color = new Color(255, 0, 0);
        status = Status.Select;
        selectButton.SetActive(true);
        overText.text = " ";
    }

    void timeCheck()
    {
        if (status == Status.Waiting)
        {
            overText.text = "Time : " + transform.parent.GetComponent<MissionTimer>().TimeGet();
            if (transform.parent.GetComponent<MissionTimer>().timeEnd)
            {
                selectWater();
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        timeCheck();
        moveBox();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame_UploadData : MonoBehaviour
{
    public ClickButton downloadButton;
    public Transform fileImage;
    public UploadGague gague; //게이지 아웃라인 , 게이지바 , 텍스트 
    public Image []Cover;//커버 이미지 
    public GameObject MinigamePanel;

    public Vector3 position;
    public bool end; //테스트용
    public bool runningLeft;
    public bool runningRight;

    [SerializeField]
    private Sprite[] coverSprite;
    private int seletSprite;


    private void OnEnable()
    {
        gague.gagueSet(false);
        downloadButton.ButtonActive(true);
        position = fileImage.transform.localPosition;
        seletSprite = 0;
        end = false;
        runningLeft = true;
        runningRight = true; 
    }
    public void startDownload()
    {
        StartCoroutine(Download());
    }
    private void CoverSpriteSize(int select,int size)
    {
        if(size==0)
        {
            Cover[select].GetComponent<RectTransform>().sizeDelta = new Vector2(270, 160);
        }
        else if(size==1)
        {
            Cover[select].GetComponent<RectTransform>().sizeDelta = new Vector2(340, 160);
        }
        else if(size==2)
        {
            Cover[select].GetComponent<RectTransform>().sizeDelta = new Vector2(350, 160);
        }
        else if(size==3)
        {
            Cover[select].GetComponent<RectTransform>().sizeDelta = new Vector2(360, 160);
        }
        Cover[select].sprite = coverSprite[size];
    }
    public void  FileMove()
    {
        position.x += 7;
        fileImage.localPosition = position;
        if (position.x >= 345)
        {
            StartCoroutine(UpGague());
            position.x = -345;
        }
      
    }
    IEnumerator UpGague()
    {
        gague.FillGague();
        runningLeft = true;
        runningRight = true;
        yield return null;
    }
    IEnumerator changeSprite()
    {
        if(runningLeft)
        {
            if(fileImage.localPosition.x<0)
            {
                runningLeft = false;
                for(int size= 0; size<coverSprite.Length;size++)
                {
                   
                    CoverSpriteSize(0, size);
                   yield return  new WaitForSeconds(0.05f);
                }
                for (int size = coverSprite.Length - 1; size >= 0; size--)
                {

                    CoverSpriteSize(0, size);
                    yield return new WaitForSeconds(0.05f);
                }
            }
        } 
      if(runningRight)
        {
            if (fileImage.localPosition.x > 0&& fileImage.localPosition.x < 50)
            {
                runningRight = false;
                for (int size = 0; size < coverSprite.Length; size++)
                {
                    CoverSpriteSize(1,size);
                    yield return new WaitForSeconds(0.05f);
                }
                for (int size = coverSprite.Length - 1; size >= 0; size--)
                {

                    CoverSpriteSize(1, size);
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }
    
            yield return null;
    }
    public IEnumerator Download()
    {
       
        gague.gagueSet(true);
        downloadButton.ButtonActive(false);
  
        while (!(gague.fillGague.fillAmount==1.0f)||end)
        {
          
            FileMove();
            StartCoroutine(changeSprite());

            yield return new WaitForSeconds(0.001f);
        }
        MissionManager.Instance.MissionClear(MinigamePanel);
        Debug.Log("다운로드 종료");
        yield return null;
    }
    // Update is called once per frame
    void Update()
    {
      


        // fileImage.transform.Translate(new Vector3(0.01f,fileImage.position.y,fileImage.position.z));   
    }
}

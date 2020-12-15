using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using static DatabaseManager;

public class ColorManager : MonoBehaviourPun
{
    [Header("LobbyPanel")]
    public GameObject ColorPanel;
    public GameObject PlayerCopy;
    public Button[] ColorBtn;


    private SkinnedMeshRenderer copyPlayerMaterial;
    private RaycastHit hit = new RaycastHit();
    private Ray ray;

    private void Awake()
    {
        StartCoroutine("Init");
    }


    private void Update()
    {
        if (ColorPanel.activeSelf)
        {
            ShowSelectedColors();

            if (Input.GetMouseButtonDown(0))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    Button btn = hit.transform.GetComponent<Button>();

                    if (btn != null && btn.interactable)
                    {
                        btn.onClick.Invoke();
                    }
                }
            }
        }


        //임시 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPanel();
        }
    }

    IEnumerator Init()
    {
        yield return new WaitForSeconds(0.3f);

        int index = databaseManager.MyPlayer.colorIndex;
        copyPlayerMaterial = PlayerCopy.transform.Find("Beta_Surface").GetComponent<SkinnedMeshRenderer>();
        copyPlayerMaterial.material = databaseManager.Colors[index];
    }


    public void OnPanel()
    {
        ColorPanel.SetActive(true);
    }

    public void CancelPanel()
    {
        ColorPanel.SetActive(false);
    }

    public void SelectColor(int index)
    {
        databaseManager.MyPlayer.GetComponent<PhotonView>().RPC("SetColor", RpcTarget.AllBuffered, index);
        copyPlayerMaterial.material = databaseManager.Colors[index];
    }

    public void ShowSelectedColors()
    {
        List<int> PlayerColors = new List<int>();
        for (int i = 0; i < databaseManager.Players.Count; i++)
        {
            PlayerColors.Add(databaseManager.Players[i].colorIndex);
        }

        for (int i = 0; i < ColorBtn.Length; i++)
        {
            if (!PlayerColors.Contains(i))
            {
                ColorBtn[i].interactable = true;
            }
            else
            {
                ColorBtn[i].interactable = false;
            }
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

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
        int index = DatabaseManager.databaseManager.MyPlayer.colorIndex;
        copyPlayerMaterial = PlayerCopy.transform.Find("Beta_Surface").GetComponent<SkinnedMeshRenderer>();
        copyPlayerMaterial.material = DatabaseManager.databaseManager.Colors[index];
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
        DatabaseManager.databaseManager.MyPlayer.GetComponent<PhotonView>().RPC("SetColor", RpcTarget.AllBuffered, index);
        copyPlayerMaterial.material = DatabaseManager.databaseManager.Colors[index];
    }

    public void ShowSelectedColors()
    {
        List<int> PlayerColors = new List<int>();
        for (int i = 0; i < DatabaseManager.databaseManager.Players.Count; i++)
        {
            PlayerColors.Add(DatabaseManager.databaseManager.Players[i].colorIndex);
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

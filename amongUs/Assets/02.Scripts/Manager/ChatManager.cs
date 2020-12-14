using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using static NetworkManager;

public class ChatManager : MonoBehaviourPun
{
    public static ChatManager CM;

    public GameObject ChatPanel;
    public InputField ChatInput;
    public GameObject ContentParent;


    //프리펩으로 생성된 오브젝트를 담음
    private List<GameObject> ChatList = new List<GameObject>();

    private void Awake()
    {
        CM = this;
        ChatEnable();
    }

    private void Update()
    {
        //방이 생성되어 있지 않으면 나감
        if (!PhotonNetwork.InRoom) return;

        //채팅패널 활성화시
        if (ChatPanel.activeSelf)
        {
            //엔터 키 눌렀을 때
            if (Input.GetKeyDown(KeyCode.Return))
            {
                //메시지 보내기
                OnSend();
            }
        }
    }

    [PunRPC]
    void ChatRPC(string name, string msg)
    {
        GameObject prefab;

        //자신일 경우 
        if (PhotonNetwork.NickName == name )
        {
            prefab = Resources.Load("MyChatContent") as GameObject;
        }
        //타인일 경우
        else
        {
            prefab = Resources.Load("YouChatContent") as GameObject;
        }

        //프리펩 생성
        GameObject temp = Instantiate(prefab);

        //자식으로 가지고 있는 프로필 가지고 와서 설정
        GameObject Profile = temp.transform.GetChild(0).gameObject;

        //자신의 닉네임
        Profile.transform.GetChild(0).GetComponent<Text>().text = name;

        //메시지
        Profile.transform.GetChild(1).GetComponent<Text>().text = msg;

        int colorIndex = GetIconColor(name);

        //아이콘 색깔
        Profile.transform.GetChild(2).GetComponent<Image>().color = DatabaseManager.databaseManager.SetColorIcon(colorIndex);

        //부모 설정
        temp.transform.SetParent(ContentParent.transform);

        //사이즈 버그가 있어서 1로 초기화
        temp.transform.localScale = Vector3.one;

        //판넬 사이즈 설정 판넬 
        //width = 380, 텍스트 fontSize = 20 -> 한줄 총 19자 지만.. 20자까지 들어감...띠용??
        int line = Profile.transform.GetChild(1).GetComponent<Text>().text.Length / 20;
        int size = Profile.transform.GetChild(1).GetComponent<Text>().fontSize * line;
        float height = temp.GetComponent<RectTransform>().rect.height + size;
        temp.GetComponent<RectTransform>().sizeDelta = new Vector2(temp.GetComponent<RectTransform>().rect.width, height);

        //리스트에 넣기
        ChatList.Add(temp);
    }

    public void OnSend()
    {
        string name = PhotonNetwork.NickName;
        string msg = ChatInput.text;

        photonView.RPC("ChatRPC", RpcTarget.All, name, msg);
        ChatInput.text = "";
        ChatEnable();
    }

    public void OnChat()
    {
        ChatPanel.SetActive(!ChatPanel.activeSelf);

        if (ChatPanel.activeSelf) ChatEnable();

    }

    public void ChatEnable()
    {
        //채팅 입력 필드 활성화
        ChatInput.ActivateInputField();
        ChatInput.Select();
    }

    public void ChatClear()
    {
        //방에서 나가면 채팅 삭제
        for (int i = 0; i < ChatList.Count; i++)
        {
            Destroy(ChatList[i]);
        }

        ChatList.Clear();
    }

    public int GetIconColor(string name)
    {
        for(int i=0; i < DatabaseManager.databaseManager.Players.Count; i++)
        {
            if(DatabaseManager.databaseManager.Players[i].nickName == name)
            {
                return DatabaseManager.databaseManager.Players[i].colorIndex;
            }
        }

        return 0;
    }
}

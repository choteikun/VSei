using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatRoomDisplay : MonoBehaviour
{
    public GameObject ChatRoom1;
    public GameObject ChatRoom2;
    public GameObject ChatRoom3;
    public GameObject ChatRoom4;
    // Start is called before the first frame update
    void Start()
    {
        ChatRoomClose(ChatRoom1);

        ChatRoom1.SetActive(false); //ChatRoom方塊隱藏
        ChatRoom2.SetActive(false);
        ChatRoom3.SetActive(false);
        ChatRoom4.SetActive(false);

        Invoke("Chat_Show1", 1.0f); //ChatRoom出現時間
        Invoke("Chat_Show2", 2.0f);
        Invoke("Chat_Show3", 3.0f);
        Invoke("Chat_Show4", 4.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Chat_Show1()
    {
        ChatRoom1.SetActive(true); //ChatRoom方塊出現
        ChatRoomOpen(ChatRoom1);
    }

    void Chat_Show2()
    {
        ChatRoom2.SetActive(true);
    }

    void Chat_Show3()
    {
        ChatRoom3.SetActive(true);
    }

    void Chat_Show4()
    {
        ChatRoom4.SetActive(true);
    }
    public void ChatRoomClose(GameObject obj)//UI關閉
    {
        obj.GetComponent<CanvasGroup>().alpha = 0;
        obj.GetComponent<CanvasGroup>().interactable = false;
        obj.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
    public void ChatRoomOpen(GameObject obj)//UI打開
    {
        obj.GetComponent<CanvasGroup>().alpha = 1;
        obj.GetComponent<CanvasGroup>().interactable = true;
        obj.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}

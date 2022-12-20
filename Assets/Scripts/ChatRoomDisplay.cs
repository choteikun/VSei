using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatRoomDisplay : MonoBehaviour
{
    public GameObject ChatRoom1; //6
    public GameObject ChatRoom2; //7
    public GameObject ChatRoom3; //8
    public GameObject ChatRoom4; //9
    public GameObject ChatRoom5; //10
    // Start is called before the first frame update
    void Start()
    {
        ChatRoomClose(ChatRoom1); //ChatRoom方塊隱藏
        ChatRoomClose(ChatRoom2);
        ChatRoomClose(ChatRoom3);
        ChatRoomClose(ChatRoom4);
        ChatRoomClose(ChatRoom5);

        Invoke("Chat_Show1", 5.0f); //ChatRoom出現時間
        Invoke("Chat_Show2", 4.0f);
        Invoke("Chat_Show3", 3.0f);
        Invoke("Chat_Show4", 2.0f);
        Invoke("Chat_Show5", 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Chat_Show1()
    {
        ChatRoomOpen(ChatRoom1); //ChatRoom方塊出現
    }

    void Chat_Show2()
    {
        ChatRoomOpen(ChatRoom2);
    }

    void Chat_Show3()
    {
        ChatRoomOpen(ChatRoom3);
    }

    void Chat_Show4()
    {
        ChatRoomOpen(ChatRoom4);
    }

    void Chat_Show5()
    {
        ChatRoomOpen(ChatRoom5);
    }

    public void ChatRoomClose(GameObject obj)
    {
        obj.GetComponent<CanvasGroup>().alpha = 0;
        obj.GetComponent<CanvasGroup>().interactable = false;
        obj.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void ChatRoomOpen(GameObject obj)
    {
        obj.GetComponent<CanvasGroup>().alpha = 1;
        obj.GetComponent<CanvasGroup>().interactable = true;
        obj.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}

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
        ChatRoom1.SetActive(false);
        ChatRoom2.SetActive(false);
        ChatRoom3.SetActive(false);
        ChatRoom4.SetActive(false);

        Invoke("Chat_Show1", 1.0f);
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
        ChatRoom1.SetActive(true);
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
}
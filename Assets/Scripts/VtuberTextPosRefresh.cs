using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VtuberTextPosRefresh : MonoBehaviour
{
    public GameObject DialogueBox;
    public GameObject NpcSubtitlePanel;

    void Start()
    {
        DialogueBox = GameObject.Find("DialogueBox").gameObject;
    }
    void Update()
    {
        Invoke("FindNpcSubtitlePanel", 0.1f);
    }
    public void FindNpcSubtitlePanel()
    {
        if (NpcSubtitlePanel == null)
        {
            NpcSubtitlePanel = GameObject.Find("NPC Subtitle Panel").gameObject;
            NpcSubtitlePanel.GetComponent<RectTransform>().pivot = new Vector2(DialogueBox.GetComponent<RectTransform>().pivot.x, DialogueBox.GetComponent<RectTransform>().pivot.y);
            NpcSubtitlePanel.GetComponent<RectTransform>().position = new Vector3(DialogueBox.GetComponent<RectTransform>().position.x, DialogueBox.GetComponent<RectTransform>().position.y, DialogueBox.GetComponent<RectTransform>().position.z);
        }
    }
}

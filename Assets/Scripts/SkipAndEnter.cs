using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkipAndEnter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(ChangeScene); //點擊觸發ChangeScene
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene()
    {
        Destroy(GameObject.FindGameObjectWithTag("DialogueManager"));
        SceneManager.LoadScene("GameScene");
    }
}

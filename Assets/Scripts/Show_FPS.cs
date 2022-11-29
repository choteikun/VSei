using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class Show_FPS : MonoBehaviour
{
    public TMP_Text fpsText;
    float deltaTime;

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = "Fps : " + Mathf.Ceil(fps).ToString();
    }
}

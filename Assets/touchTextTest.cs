using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class touchTextTest : MonoBehaviour
{
    public TMP_Text tmp_text;
    
    
    void Update()
    {
        tmp_text.text = "TouchCount : " + Input.touchCount.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FeverBar : MonoBehaviour
{

    public RythmGameCanvas rythmGameCanvas;
    public Slider feverSlider;
    public Slider slider2;
    TMP_Text hpText;

    void Start()
    {
        hpText = transform.GetChild(transform.childCount - 1).GetComponent<TMP_Text>();

        
    }


    void Update()
    {
        hpText.text = rythmGameCanvas.curSpecialCount + "/" + rythmGameCanvas.feverNeedPoint;

        feverSlider.value = (float)rythmGameCanvas.curSpecialCount / 100;

        slider2.value = Mathf.Lerp(slider2.value, (float)rythmGameCanvas.curSpecialCount / 100, Time.deltaTime * 10);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHPGauge : MonoBehaviour
{
    public MyAccount myAccount;
    public RythmGameCanvas rythmGameCanvas;
    public Slider hpSlider;
    public Slider slider2;

    void Start()
    {
        if (!myAccount)
        {
            myAccount= Resources.Load<MyAccount>("CharacterScriptObjsInfo/MyAccount"); 
        }
    }


    void Update()
    {
        hpSlider.value = (float)rythmGameCanvas.curCharHp / 1000;

        slider2.value = Mathf.Lerp(slider2.value, (float)rythmGameCanvas.curCharHp / 1000, Time.deltaTime * 10);
    }
}

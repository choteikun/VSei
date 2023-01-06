using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FeverBar : MonoBehaviour
{
    public MyAccount myAccount;
    public Sprite[] characterrSprites;
    public RythmGameCanvas rythmGameCanvas;
    public GameObject feverBg;
    public Slider feverSlider;
    public Slider slider2;
    TMP_Text hpText;

    void Start()
    {
        hpText = transform.GetChild(transform.childCount - 1).GetComponent<TMP_Text>();

        switch (myAccount.curCharacterUse)
        {
            case MyAccount.CurCharacterUse.FelbelemAlice:
                feverBg.GetComponent<Image>().sprite = characterrSprites[0];
                break;
            case MyAccount.CurCharacterUse.AikaAmimi:
                feverBg.GetComponent<Image>().sprite = characterrSprites[1];
                break;
            case MyAccount.CurCharacterUse.MalibetaRorem:
                feverBg.GetComponent<Image>().sprite = characterrSprites[2];
                break;
            case MyAccount.CurCharacterUse.Nameless:
                feverBg.GetComponent<Image>().sprite = characterrSprites[3];
                break;
            case MyAccount.CurCharacterUse.ShiorhaiYai:
                feverBg.GetComponent<Image>().sprite = characterrSprites[4];
                break;
            default:
                break;
        }

    }


    void Update()
    {
        hpText.text = rythmGameCanvas.curSpecialCount + "/" + rythmGameCanvas.feverNeedPoint;

        feverSlider.value = (float)rythmGameCanvas.curSpecialCount / 30;

        slider2.value = Mathf.Lerp(slider2.value, (float)rythmGameCanvas.curSpecialCount / 30, Time.deltaTime * 1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHPGauge : MonoBehaviour
{
    public MyAccount myAccount;
    //public CharactersInfo curMyCharacter;
    public RythmGameCanvas rythmGameCanvas;
    public Slider hpSlider;
    public Slider slider2;
    TMP_Text hpText;

    void Start()
    {
        hpText = transform.GetChild(transform.childCount-1).GetComponent<TMP_Text>();
        if (!myAccount)
        {
            myAccount= Resources.Load<MyAccount>("CharacterScriptObjsInfo/MyAccount"); 
        }

        //switch (myAccount.curCharacterUse)
        //{
        //    case MyAccount.CurCharacterUse.FelbelemAlice:
        //        curMyCharacter = Resources.Load<CharactersInfo>("CharacterScriptObjsInfo/FelbelemAlice");
        //        break;
        //    case MyAccount.CurCharacterUse.AikaAmimi:
        //        curMyCharacter = Resources.Load<CharactersInfo>("CharacterScriptObjsInfo/AikaAmimi");
        //        break;
        //    case MyAccount.CurCharacterUse.MalibetaRorem:
        //        curMyCharacter = Resources.Load<CharactersInfo>("CharacterScriptObjsInfo/MalibetaRorem");
        //        break;
        //    case MyAccount.CurCharacterUse.Nameless:
        //        curMyCharacter = Resources.Load<CharactersInfo>("CharacterScriptObjsInfo/Nameless");
        //        break;
        //    case MyAccount.CurCharacterUse.ShiorhaiYai:
        //        curMyCharacter = Resources.Load<CharactersInfo>("CharacterScriptObjsInfo/ShiorhaiYai");
        //        break;
        //    default:
        //        break;
        //}
        //Debug.Log(rythmGameCanvas.curCharHp);
    }


    void Update()
    {
        hpText.text = rythmGameCanvas.curCharHp + "/" + rythmGameCanvas.charInfoDictionary[myAccount.curCharacterUse].charHp;

        hpSlider.value = rythmGameCanvas.curCharHp / 100;

        slider2.value = Mathf.Lerp(slider2.value, rythmGameCanvas.curCharHp / 100, Time.deltaTime * 10);
    }
}

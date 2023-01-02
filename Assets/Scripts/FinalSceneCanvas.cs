using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinalSceneCanvas : MonoBehaviour
{
    public Image[] characterBgImages;
    public Image[] songNameImages;
    public MyAccount myAccount;
    public SongsInfo songsInfo;


    [Header("音遊分數跳動總次數")]
    public int CurRythmPointTextRate;
    [Header("音遊分數幾秒跳動一次")]
    public float CurRythmPointJumpTime;//相乘就是花了幾秒跳完

    [Header("獲得代幣跳動總次數")]
    public int MyTokenTextRate;
    [Header("獲得代幣數字幾秒跳動一次")]
    public float MyTokenJumpTime;

    TMP_Text perfectCountText;
    TMP_Text goodCountText;
    TMP_Text missCountText;

    TMP_Text myTokenText;
    TMP_Text curRythmPointText1;
    TMP_Text curRythmPointText2;


    int myCurRythmPointShowNum;
    int myTokenShowNum;
    int myTokenCalculate;
    int myChargeToken;

    Color alpha0 = new Color(1.0f, 1.0f, 1.0f, 0);//透明
    Color alpha1 = new Color(1.0f, 1.0f, 1.0f, 1.0f);//不透明

    private void Start()
    {
        myTokenCalculate = (int)Mathf.Round(myAccount.CurRythmPoint * 0.009f);//總分換算代幣
        if (myTokenCalculate > 0)
        {
            myChargeToken = myTokenCalculate;//換算後的充值代幣
        }
        else
        {
            myChargeToken = 0;
        }
        myAccount.MyToken += myChargeToken;//加進玩家的代幣包包


        perfectCountText = transform.Find("PerfectCountText(TMP)").GetComponent<TMP_Text>();
        goodCountText = transform.Find("GoodCountText(TMP)").GetComponent<TMP_Text>();
        missCountText = transform.Find("MissCountText(TMP)").GetComponent<TMP_Text>();

        myTokenText = transform.Find("MyTokenText(TMP)").GetComponent<TMP_Text>();
        curRythmPointText1 = transform.Find("CurRythmPointText(TMP)(1)").GetComponent<TMP_Text>();
        curRythmPointText2 = transform.Find("CurRythmPointText(TMP)(2)").GetComponent<TMP_Text>();

        perfectCountText.text = myAccount.CurPerfectCount.ToString();
        goodCountText.text = myAccount.CurGoodCount.ToString();
        missCountText.text = myAccount.CurMissCount.ToString();
        curRythmPointText2.text = myAccount.CurRythmPoint.ToString();

        for (int i = 0; i < characterBgImages.Length; i++)
        {
            characterBgImages[i].GetComponent<CanvasGroup>().alpha = 0;
        }
        switch (myAccount.curCharacterUse)
        {
            case MyAccount.CurCharacterUse.FelbelemAlice:
                characterBgImages[0].GetComponent<CanvasGroup>().alpha = 1;
                break;
            case MyAccount.CurCharacterUse.AikaAmimi:
                characterBgImages[1].GetComponent<CanvasGroup>().alpha = 1;
                break;
            case MyAccount.CurCharacterUse.MalibetaRorem:
                characterBgImages[2].GetComponent<CanvasGroup>().alpha = 1;
                break;
            case MyAccount.CurCharacterUse.Nameless:
                characterBgImages[3].GetComponent<CanvasGroup>().alpha = 1;
                break;
            case MyAccount.CurCharacterUse.ShiorhaiYai:
                characterBgImages[4].GetComponent<CanvasGroup>().alpha = 1;
                break;
            default:
                break;
        }
        for (int i = 0; i < songNameImages.Length; i++)
        {
            songNameImages[i].GetComponent<CanvasGroup>().alpha = 0;
        }
        if (songsInfo.difficultySelection == "Normal")
        {
            switch (songsInfo.songNumIs)
            {
                case 1:
                    songNameImages[0].GetComponent<CanvasGroup>().alpha = 1;
                    break;
                case 2:
                    songNameImages[2].GetComponent<CanvasGroup>().alpha = 1;
                    break;
                default:
                    break;
            }
        }
        if (songsInfo.difficultySelection == "Hard")
        {
            switch (songsInfo.songNumIs)
            {
                case 1:
                    songNameImages[1].GetComponent<CanvasGroup>().alpha = 1;
                    break;
                case 2:
                    songNameImages[3].GetComponent<CanvasGroup>().alpha = 1;
                    break;
                default:
                    break;
            }
        }


        Invoke("DelayTimeShowMyTokenNum", 0f);
        Invoke("DelayTimeShowMyCurRythmPointShowNum", 0f);
    }

    public IEnumerator MyTokenJump()
    {
        int delta = myChargeToken / MyTokenTextRate;
        for (int i = 0; i < MyTokenTextRate; i++)
        {
            myTokenShowNum += delta;
            myTokenText.text = myTokenShowNum.ToString();
            yield return new WaitForSeconds(MyTokenJumpTime);
            //yield return 1;
        }
        myTokenShowNum = myChargeToken;
        myTokenText.text = myTokenShowNum.ToString();
        StopCoroutine(MyTokenJump());
    }
    public IEnumerator CurRythmPointJump()
    {
        int delta = myAccount.CurRythmPoint / CurRythmPointTextRate;
        for (int i = 0; i < CurRythmPointTextRate; i++)
        {
            myCurRythmPointShowNum -= delta;
            curRythmPointText1.text = myCurRythmPointShowNum.ToString();
            yield return new WaitForSeconds(CurRythmPointJumpTime);
            //yield return 1;
        }

        myCurRythmPointShowNum = myAccount.CurRythmPoint;
        curRythmPointText1.text = myCurRythmPointShowNum.ToString();
        StopCoroutine(CurRythmPointJump());
    }

    public void DelayTimeShowMyTokenNum()
    {
        StartCoroutine(MyTokenJump());
    }
    public void DelayTimeShowMyCurRythmPointShowNum()
    {
        StartCoroutine(CurRythmPointJump());
    }
    public void ResetAllPoint()//重設結算分數
    {
        myAccount.CurPerfectCount = 0;
        myAccount.CurGoodCount = 0;
        myAccount.CurMissCount = 0;
        myAccount.CurRythmPoint = 0;
    }
}

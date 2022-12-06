using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbySliderCtrl : MonoBehaviour
{
    public Scrollbar scrollbar;
    public ScrollRect scrollRect;
    public GameObject warningTextPrefab;
    public MyAccount myAccount;


    private float targetValue;

    public bool needMove = false;

    private bool allowGamePlay;

    //private const float MOVE_SPEED = 1F;

    private const float SMOOTH_TIME = 0.2F;

    private float moveSpeed = 0f;


    public void OnPointerDown()
    {
        needMove = false;
        Debug.Log("OnPointerDown");
    }

    void Start()
    {
        allowGamePlay = true;
    }
    public void OnButtonClick(int value)
    {
        switch (value)
        {
            case 1:
                targetValue = 0;
                break;
            case 2:
                targetValue = 0.25f;
                break;
            case 3:
                targetValue = 0.5f;
                break;
            case 4:
                targetValue = 0.75f;
                break;
            case 5:
                targetValue = 1.0f;
                break;
            default:
                Debug.LogError("!!!!!");
                break;
        }
        needMove = true;
        Debug.Log("OnButtonClick");
    }

    void Update()
    {
        if (needMove)
        {
            if (Mathf.Abs(scrollbar.value - targetValue) < 0.01f)
            {
                scrollbar.value = targetValue;
                needMove = false;
                return;
            }
            scrollbar.value = Mathf.SmoothDamp(scrollbar.value, targetValue, ref moveSpeed, SMOOTH_TIME);
        }
        if (Input.touchCount == 1 && (Input.GetTouch(0).phase == TouchPhase.Ended)) //������}�ù�
        {
            //�P�_��e�����Ӱ϶��A�]�m�۰ʷưʦܪ���m
            if (scrollbar.value <= 0.125f)
            {
                targetValue = 0;
                if (myAccount.FelbelemAlice)
                {
                    myAccount.curCharacterUse = MyAccount.CurCharacterUse.FelbelemAlice;
                    allowGamePlay = true;
                }
            }
            else if (scrollbar.value <= 0.375f)
            {
                targetValue = 0.25f;
                if (myAccount.AikaAmimi)
                {
                    myAccount.curCharacterUse = MyAccount.CurCharacterUse.AikaAmimi;
                    allowGamePlay = true;
                }
                else { allowGamePlay = false; }
            }
            else if (scrollbar.value <= 0.625f)
            {
                targetValue = 0.5f;
                if (myAccount.MalibetaRorem)
                {
                    myAccount.curCharacterUse = MyAccount.CurCharacterUse.MalibetaRorem;
                    allowGamePlay = true;
                }
                else { allowGamePlay = false; }
            }
            else if (scrollbar.value <= 0.875f)
            {
                targetValue = 0.75f;
                if (myAccount.Nameless)
                {
                    myAccount.curCharacterUse = MyAccount.CurCharacterUse.Nameless;
                    allowGamePlay = true;
                }
                else { allowGamePlay = false; }
            }
            else if (scrollbar.value <= 1.125f)
            {
                targetValue = 1.0f;
                if (myAccount.ShiorhaiYai)
                {
                    myAccount.curCharacterUse = MyAccount.CurCharacterUse.ShiorhaiYai;
                    allowGamePlay = true;
                }
                else { allowGamePlay = false; }
            }
            needMove = true;
            moveSpeed = 0;
        }
    }
    public void AllowGamePlay()
    {
        if (allowGamePlay)
        {
            SceneManager.LoadScene("SongSelectScene");
        }
        else
        {
            PoolManager.Release(warningTextPrefab);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LobbySliderCtrl : MonoBehaviour
{
    public MyAccount myAccount;
    public Scrollbar scrollbar;
    public ScrollRect scrollRect;
    public GameObject warningTextPrefab;
    public TMP_Text playerTokenText;


    private float targetValue;

    public bool needMove = false;

    private bool allowGamePlay;

    //private const float MOVE_SPEED = 1F;

    private const float SMOOTH_TIME = 0.2F;

    private float moveSpeed = 0f;

    enum slideVector { nullVector, left, right };
    private Vector2 lastPos;//上一個位置
    private Vector2 currentPos;//下一個位置
    private slideVector currentVector = slideVector.nullVector;//當前滑動方向
    private float timer;//時間計數器
    public float offsetTime = 0.01f;//判斷的時間間隔


    public void OnPointerDown()
    {
        needMove = false;
        Debug.Log("OnPointerDown");
    }

    void Start()
    {
        allowGamePlay = true;
        playerTokenText = GameObject.Find("MyToken(Lobby_TMP)").GetComponent<TMP_Text>();
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
        playerTokenText.text = myAccount.MyToken.ToString();
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
        if (Input.touchCount == 1)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                lastPos = Input.touches[0].position;
                currentPos = Input.touches[0].position;
                timer = 0;
                //Debug.Log("Click begin && Drag begin");
            }
            if (Input.touches[0].phase == TouchPhase.Moved)
            {
                currentPos = Input.touches[0].position;
                timer += Time.deltaTime;
                if (timer > offsetTime)
                {
                    if (currentPos.x < lastPos.x)
                    {
                        if (currentVector == slideVector.left)
                        {
                            return;
                        }
                        //TODO trun Left event
                        currentVector = slideVector.left;
                        //Debug.Log("Turn left");
                    }
                    if (currentPos.x > lastPos.x)
                    {
                        if (currentVector == slideVector.right)
                        {
                            return;
                        }
                        //TODO trun right event
                        currentVector = slideVector.right;
                        //Debug.Log("Turn right");
                    }
                    lastPos = currentPos;
                    timer = 0;
                }
            }
            //if (Input.touches[0].phase == TouchPhase.Ended)
            //{//滑動結束
            //    currentVector = slideVector.nullVector;
            //    Debug.Log("Click over && Drag over");
            //}
        }
        if (Input.touchCount == 1 && (Input.GetTouch(0).phase == TouchPhase.Ended)) //手指離開螢幕
        {
            //判斷當前位於哪個區間，設置自動滑動至的位置
            if (scrollbar.value <= 0.075f || (currentVector == slideVector.right && scrollbar.value <= 0.175f))//125
            {
                allowGamePlay = true;
                targetValue = 0;
                if (myAccount.FelbelemAlice)
                {
                    myAccount.curCharacterUse = MyAccount.CurCharacterUse.FelbelemAlice;
                }
            }
            else if (scrollbar.value <= 0.325f || (currentVector == slideVector.right && scrollbar.value <= 0.425f && scrollbar.value > 0.175f))//375
            {
                allowGamePlay = true;
                targetValue = 0.25f;
                if (myAccount.AikaAmimi)
                {
                    myAccount.curCharacterUse = MyAccount.CurCharacterUse.AikaAmimi;
                }
                else { allowGamePlay = false; }
            }
            else if (scrollbar.value <= 0.575f || (currentVector == slideVector.right && scrollbar.value <= 0.675f && scrollbar.value > 0.425f))//625
            {
                allowGamePlay = true;
                targetValue = 0.5f;
                if (myAccount.MalibetaRorem)
                {
                    myAccount.curCharacterUse = MyAccount.CurCharacterUse.MalibetaRorem;
                    
                }
                else { allowGamePlay = false; }
            }
            else if (scrollbar.value <= 0.825f || (currentVector == slideVector.right && scrollbar.value <= 0.925f && scrollbar.value > 0.675f))//875
            {
                allowGamePlay = true;
                targetValue = 0.75f;
                if (myAccount.Nameless)
                {
                    myAccount.curCharacterUse = MyAccount.CurCharacterUse.Nameless;
                }
                else { allowGamePlay = false; }
            }
            else if (scrollbar.value <= 1.075f)//1125
            {
                allowGamePlay = true;
                targetValue = 1.0f;
                if (myAccount.ShiorhaiYai)
                {
                    myAccount.curCharacterUse = MyAccount.CurCharacterUse.ShiorhaiYai;
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
            switch (myAccount.curCharacterUse)
            {
                case MyAccount.CurCharacterUse.FelbelemAlice:
                    SceneManager.LoadScene("AliceStart");
                    break;
                case MyAccount.CurCharacterUse.AikaAmimi:
                    SceneManager.LoadScene("AmimiStart");
                    break;
                case MyAccount.CurCharacterUse.MalibetaRorem:
                    SceneManager.LoadScene("LorenStart");
                    break;
                case MyAccount.CurCharacterUse.Nameless:
                    SceneManager.LoadScene("NonameStart");
                    break;
                case MyAccount.CurCharacterUse.ShiorhaiYai:
                    SceneManager.LoadScene("YaiStart");
                    break;
                default:
                    break;
            }
            
        }
        else
        {
            PoolManager.Release(warningTextPrefab);
        }
        
    }
}

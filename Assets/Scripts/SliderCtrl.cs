using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderCtrl : MonoBehaviour
{
    public Scrollbar scrollbar;
    public ScrollRect scrollRect;

    private float targetValue;

    public bool needMove = false;

    //private const float MOVE_SPEED = 1F;

    private const float SMOOTH_TIME = 0.2F;

    private float moveSpeed = 0f;


    public void OnPointerDown()
    {
        needMove = false;
        Debug.Log("OnPointerDown");
    }


    //public void OnPointerUp()
    //{
    //    //判斷當前位於哪個區間，設置自動滑動至的位置
    //    if (scrollbar.value <= 0.182f)
    //    {
    //        targetValue = 0;
    //    }
    //    else if (scrollbar.value <= 0.548f)
    //    {
    //        targetValue = 0.365f;
    //    }
    //    else if (scrollbar.value <= 0.914f)
    //    {
    //        targetValue = 0.731f;
    //    }
    //    needMove = true;
    //    moveSpeed = 0;
    //    Debug.Log("OnPointerUp");
    //}

    public void OnButtonClick(int value)
    {
        switch (value)
        {
            case 1:
                targetValue = 0;
                break;
            case 2:
                targetValue = 0.365f;
                break;
            case 3:
                targetValue = 0.731f;
                break;
            //case 4:
            //    targetValue = 0.388f;
            //    break;
            //case 5:
            //    targetValue = 0.518f;
            //    break;
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
        if (Input.touchCount == 1 && (Input.GetTouch(0).phase == TouchPhase.Ended)) //手指離開螢幕
        {
            //判斷當前位於哪個區間，設置自動滑動至的位置
            if (scrollbar.value <= 0.182f)
            {
                targetValue = 0;
            }
            else if (scrollbar.value <= 0.548f)
            {
                targetValue = 0.365f;
            }
            else if (scrollbar.value <= 1.5f)
            {
                targetValue = 0.731f;
            }
            needMove = true;
            moveSpeed = 0;
        }
    }
    
}

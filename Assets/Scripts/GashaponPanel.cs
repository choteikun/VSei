using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GashaponPanel : MonoBehaviour
{
    //public CanvasGroup gashaCanvasGroup;
    //public CanvasGroup[] canvasChildGroups;//抽卡介面子物件的CanvasGroup
    Animation gashaPanelAnim;



    void Start()
    {
        gashaPanelAnim = GetComponent<Animation>();

        //gashaCanvasGroup = GetComponent<GashaponPanel>().gameObject.GetComponent<CanvasGroup>();

        //if (canvasChildGroups.Length >= 4)
        //{
        //    canvasChildGroups[3] = transform.GetChild(transform.childCount - 4).gameObject.GetComponent<CanvasGroup>();
        //    canvasChildGroups[2] = transform.GetChild(transform.childCount - 3).gameObject.GetComponent<CanvasGroup>();
        //    canvasChildGroups[1] = transform.GetChild(transform.childCount - 2).gameObject.GetComponent<CanvasGroup>();
        //    canvasChildGroups[0] = transform.GetChild(transform.childCount - 1).gameObject.GetComponent<CanvasGroup>();
        //}
        //else
        //{
        //    throw new ArgumentNullException("請輸入正確的索引值(點擊此訊息查看上面的數組長度並在inspector中輸入比他大的數值)，讓UI功能正常運行");
        //}

    }


    void Update()
    {
        
    }
    public void OneGasha()
    {

    }
    public void TenGasha()
    {

    }
    public void GashaPanelFadeIn()//過場
    {
        gashaPanelAnim.Play();
    }
    public void Test()
    {
        Debug.Log("test");
    }
}

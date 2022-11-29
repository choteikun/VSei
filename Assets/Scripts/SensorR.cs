using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorR : MonoBehaviour
{   
    RythmGameCanvas gameCanvas;

    //記錄右側螢幕中間的X軸
    float rightMidScreenPosX;
    //記錄螢幕中間的X軸
    float midScreenPosX;
    //記錄螢幕中間的Y軸
    float midScreenPosY;

    Touch firstTouch;
    Touch secondTouch;

    //bool touched;

    //public bool playerTouched { get => touched; }
    public GameObject rootUI;
    public int testBonusPoint;

    public bool firstTouchedLastFrame = false;
    public bool secondTouchedLastFrame = false;

    public enum JugeArea
    {
        None,
        Perfect,
        Good,
        Miss
    }
    public JugeArea jugeArea;

    void Start()
    {
        Input.multiTouchEnabled = true;
        gameCanvas = rootUI.GetComponent<RythmGameCanvas>();
        //touched = false;
        //右側螢幕中間X軸
        rightMidScreenPosX = Screen.width * 3 / 4f;
        //初始螢幕中間X軸
        midScreenPosX = Screen.width / 2f;
        //螢幕中間的Y軸
        midScreenPosY = Screen.height / 2;
        jugeArea = JugeArea.None;
    }
    void Update()
    {
        if (firstTouchedLastFrame && Input.touchCount == 0)
        {
            firstTouchedLastFrame = false;
        }
        if (secondTouchedLastFrame && Input.touchCount == 1)
        {
            secondTouchedLastFrame = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("MissArea"))
        {
            jugeArea = JugeArea.Miss;
            //Debug.Log("Miss");
        }
        else if (other.transform.CompareTag("GoodArea") && jugeArea == JugeArea.Miss)
        {
            jugeArea = JugeArea.Good;
            //Debug.Log("Good");
        }
        else if (other.transform.CompareTag("PerfectArea") && jugeArea == JugeArea.Good)
        {
            jugeArea = JugeArea.Perfect;
            //Debug.Log("Perfect");
        }
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.transform.CompareTag("MissArea") && Input.touchCount > 0)
    //    {
    //        firstTouch = Input.GetTouch(0);

    //        if (firstTouch.position.x > midScreenPosX && firstTouch.position.x < rightMidScreenPosX && !touched)
    //        {
    //            Debug.Log("Perfect" + "，觸碰位置: " + firstTouch.position);
    //            testBonusPoint++;
    //            touched = true;
    //            gameCanvas.PerfectEffect();
    //            other.gameObject.SetActive(false);
    //        }
    //        StartCoroutine(DelayedTriggerExit());
    //    }
    //}
    private void OnTriggerStay(Collider other)
    {
        if (secondTouchedLastFrame && Input.touchCount == 1)
        {
            secondTouchedLastFrame = false;
        }
        if (firstTouchedLastFrame && Input.touchCount == 0)
        {
            firstTouchedLastFrame = false;
        }
        else if (!firstTouchedLastFrame && Input.touchCount == 1)
        {
            //這裡只會偵測第一次按下
            firstTouch = Input.GetTouch(0);
            if (firstTouch.position.x > midScreenPosX && firstTouch.position.x < rightMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Perfect)
            {
                Debug.Log("Perfect" + "，觸碰位置: " + firstTouch.position);
                gameCanvas.rythmPoint += 5;

                gameCanvas.PerfectEffect();
                jugeArea = JugeArea.None;
                other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
            }
            else if (firstTouch.position.x > midScreenPosX && firstTouch.position.x < rightMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Good)
            {
                Debug.Log("Good" + "，觸碰位置: " + firstTouch.position);
                gameCanvas.rythmPoint += 1;

                gameCanvas.GoodEffect();
                jugeArea = JugeArea.None;
                other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
            }
            else if (firstTouch.position.x > midScreenPosX && firstTouch.position.x < rightMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Miss)
            {
                Debug.Log("Miss" + "，觸碰位置: " + firstTouch.position);

                jugeArea = JugeArea.None;
                other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
            }
            firstTouchedLastFrame = true;
            //StartCoroutine(DelayedTriggerExit());
        }
        else if (firstTouchedLastFrame && Input.touchCount == 2 && !secondTouchedLastFrame)//第一下還按著接著到第二下進來
        {
            secondTouch = Input.GetTouch(1);
            if (secondTouch.position.x > midScreenPosX && secondTouch.position.x < rightMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Perfect)
            {
                Debug.Log("Perfect" + "，觸碰位置: " + firstTouch.position);
                gameCanvas.rythmPoint += 5;

                gameCanvas.PerfectEffect();
                jugeArea = JugeArea.None;
                other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
            }
            else if (secondTouch.position.x > midScreenPosX && secondTouch.position.x < rightMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Good)
            {
                Debug.Log("Good" + "，觸碰位置: " + firstTouch.position);
                gameCanvas.rythmPoint += 1;

                gameCanvas.GoodEffect();
                jugeArea = JugeArea.None;
                other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
            }
            else if (secondTouch.position.x > midScreenPosX && secondTouch.position.x < rightMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Miss)
            {
                Debug.Log("Miss" + "，觸碰位置: " + firstTouch.position);

                jugeArea = JugeArea.None;
                other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
            }
            secondTouchedLastFrame = true;
        }
    }
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.transform.CompareTag("MissArea") && Input.touchCount > 0)
    //    {
    //        firstTouch = Input.GetTouch(0);
            
    //        if (firstTouch.position.x > midScreenPosX && firstTouch.position.x < rightMidScreenPosX && !touched)
    //        {
    //            Debug.Log("Good" + "，觸碰位置: " + firstTouch.position);
    //            testBonusPoint++;
    //            touched = true;
    //            gameCanvas.GoodEffect();
    //            other.gameObject.SetActive(false);
    //        }
    //        StartCoroutine(DelayedTriggerExit());
    //    }

    //}
    //IEnumerator DelayedTriggerExit()
    //{
    //    yield return new WaitForSeconds(0.2f);
    //    touched = false;
    //}
}

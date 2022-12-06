using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorL : MonoBehaviour
{ 
    RythmGameCanvas gameCanvas;

    //[SerializeField]bool touched;
    //public bool playerTouched { get => touched; }
    public GameObject gameSceneRootUI;
    public MyAccount myAccount;
    public int testBonusPoint;

    public bool firstTouchedLastFrame = false;
    public bool secondTouchedLastFrame = false;
    public bool thirdTouchedLastFrame = false;
    public bool fourthTouchedLastFrame = false;
    Touch firstTouch;
    Touch secondTouch;
    Touch thirTouch;
    Touch fourthTouch;
    Touch touch;

    //記錄左側螢幕中間的X軸
    float leftMidScreenPosX;
    //記錄螢幕中間的X軸
    float midScreenPosX;
    //記錄螢幕中間的Y軸
    float midScreenPosY;

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
        gameCanvas = gameSceneRootUI.GetComponent<RythmGameCanvas>();
        //touched = false;
        //左側螢幕中間X軸
        leftMidScreenPosX = Screen.width / 4f;
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
        if (thirdTouchedLastFrame && Input.touchCount == 2)
        {
            thirdTouchedLastFrame = false;
        }
        if (fourthTouchedLastFrame && Input.touchCount == 3)
        {
            fourthTouchedLastFrame = false;
        }
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                touch = Input.GetTouch(i);
            }
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
    private void OnTriggerStay(Collider other)
    {
        if (!firstTouchedLastFrame && Input.touchCount == 1)
        {
            //這裡只會偵測第一次按下
            firstTouch = Input.GetTouch(0);
            if (firstTouch.position.x < midScreenPosX && firstTouch.position.x > leftMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Perfect)
            {
                Debug.Log("Perfect" + "，觸碰位置: " + firstTouch.position);
                if (other.name == "HardBeat")
                {

                }
                gameCanvas.rythmPoint += other.GetComponentInParent<ObstaclePrefabDrop>().perfectPoint;

                gameCanvas.PerfectEffect();
                jugeArea = JugeArea.None;
                other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
            }
            else if (firstTouch.position.x < midScreenPosX && firstTouch.position.x > leftMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Good)
            {
                Debug.Log("Good" + "，觸碰位置: " + firstTouch.position);
                gameCanvas.rythmPoint += other.GetComponentInParent<ObstaclePrefabDrop>().goodPoint;

                gameCanvas.GoodEffect();
                jugeArea = JugeArea.None;
                other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
            }
            else if (firstTouch.position.x < midScreenPosX && firstTouch.position.x > leftMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Miss)
            {
                Debug.Log("Miss" + "，觸碰位置: " + firstTouch.position);
                gameCanvas.rythmPoint -= other.GetComponentInParent<ObstaclePrefabDrop>().missPoint;

                jugeArea = JugeArea.None;
                other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
            }
            firstTouchedLastFrame = true;
            //StartCoroutine(DelayedTriggerExit());
        }
        else if (firstTouchedLastFrame && Input.touchCount == 2 && !secondTouchedLastFrame)//第一下還按著接著到第二下進來
        {
            secondTouch = Input.GetTouch(1);
            if (secondTouch.position.x < midScreenPosX && secondTouch.position.x > leftMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Perfect)
            {
                Debug.Log("Perfect" + "，觸碰位置: " + firstTouch.position);
                gameCanvas.rythmPoint += other.GetComponentInParent<ObstaclePrefabDrop>().perfectPoint;

                gameCanvas.PerfectEffect();
                jugeArea = JugeArea.None;
                other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
            }
            else if (secondTouch.position.x < midScreenPosX && secondTouch.position.x > leftMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Good)
            {
                Debug.Log("Good" + "，觸碰位置: " + firstTouch.position);
                gameCanvas.rythmPoint += other.GetComponentInParent<ObstaclePrefabDrop>().goodPoint;

                gameCanvas.GoodEffect();
                jugeArea = JugeArea.None;
                other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
            }
            else if (secondTouch.position.x < midScreenPosX && secondTouch.position.x > leftMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Miss)
            {
                Debug.Log("Miss" + "，觸碰位置: " + firstTouch.position);
                gameCanvas.rythmPoint -= other.GetComponentInParent<ObstaclePrefabDrop>().missPoint;

                jugeArea = JugeArea.None;
                other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
            }
            secondTouchedLastFrame = true;
        }
        else if (secondTouchedLastFrame && Input.touchCount == 3 && !thirdTouchedLastFrame)//第二下還按著接著到第三下進來
        {
            thirTouch = Input.GetTouch(2);
            if (thirTouch.position.x < midScreenPosX && secondTouch.position.x > leftMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Perfect)
            {
                Debug.Log("Perfect" + "，觸碰位置: " + firstTouch.position);
                gameCanvas.rythmPoint += other.GetComponentInParent<ObstaclePrefabDrop>().perfectPoint;

                gameCanvas.PerfectEffect();
                jugeArea = JugeArea.None;
                other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
            }
            else if (thirTouch.position.x < midScreenPosX && secondTouch.position.x > leftMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Good)
            {
                Debug.Log("Good" + "，觸碰位置: " + firstTouch.position);
                gameCanvas.rythmPoint += other.GetComponentInParent<ObstaclePrefabDrop>().goodPoint;

                gameCanvas.GoodEffect();
                jugeArea = JugeArea.None;
                other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
            }
            else if (thirTouch.position.x < midScreenPosX && secondTouch.position.x > leftMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Miss)
            {
                Debug.Log("Miss" + "，觸碰位置: " + firstTouch.position);
                gameCanvas.rythmPoint -= other.GetComponentInParent<ObstaclePrefabDrop>().missPoint;

                jugeArea = JugeArea.None;
                other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
            }
            thirdTouchedLastFrame = true;
        }
        else if (thirdTouchedLastFrame && Input.touchCount == 4 && !fourthTouchedLastFrame)//第三下還按著接著到第四下進來
        {
            fourthTouch = Input.GetTouch(3);
            if (fourthTouch.position.x < midScreenPosX && secondTouch.position.x > leftMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Perfect)
            {
                Debug.Log("Perfect" + "，觸碰位置: " + firstTouch.position);
                gameCanvas.rythmPoint += other.GetComponentInParent<ObstaclePrefabDrop>().perfectPoint;

                gameCanvas.PerfectEffect();
                jugeArea = JugeArea.None;
                other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
            }
            else if (fourthTouch.position.x < midScreenPosX && secondTouch.position.x > leftMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Good)
            {
                Debug.Log("Good" + "，觸碰位置: " + firstTouch.position);
                gameCanvas.rythmPoint += other.GetComponentInParent<ObstaclePrefabDrop>().goodPoint;

                gameCanvas.GoodEffect();
                jugeArea = JugeArea.None;
                other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
            }
            else if (fourthTouch.position.x < midScreenPosX && secondTouch.position.x > leftMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Miss)
            {
                Debug.Log("Miss" + "，觸碰位置: " + firstTouch.position);
                gameCanvas.rythmPoint -= other.GetComponentInParent<ObstaclePrefabDrop>().missPoint;

                jugeArea = JugeArea.None;
                other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
            }
            fourthTouchedLastFrame = true;
        }

    }
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.transform.CompareTag("MissArea") && Input.touchCount > 0)
    //    {
    //        firstTouch = Input.GetTouch(0);
            
    //        if (firstTouch.position.x < midScreenPosX && firstTouch.position.x > leftMidScreenPosX && !touched)
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

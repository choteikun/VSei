using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorLL : MonoBehaviour
{
    RythmGameCanvas gameCanvas;
    //[SerializeField] bool touched;
    //public bool playerTouched { get => touched; }
    public GameObject gameSceneRootUI;
    public MyAccount myAccount;

    public bool firstTouchedLastFrame = false;
    public bool secondTouchedLastFrame = false;
    public bool thirdTouchedLastFrame = false;
    public bool fourthTouchedLastFrame = false;
    Touch firstTouch;
    Touch secondTouch;
    Touch thirTouch;
    Touch fourthTouch;

    Ray ray;
    RaycastHit hit;
    bool touchSensor;

    //記錄左側螢幕中間的X軸
    float leftMidScreenPosX;
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
        //螢幕中間的Y軸
        midScreenPosY = Screen.height / 2;
        jugeArea = JugeArea.None;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                if (touch.phase == TouchPhase.Began)
                {
                    ray = Camera.main.ScreenPointToRay(touch.position);
                    if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("SensorLL"))
                    {
                        touchSensor = true;
                    }
                    else
                    {
                        Debug.Log("return false");
                    }
                }
            }
        }
        else
        {
            touchSensor = false;
        }
        //if (firstTouchedLastFrame && Input.touchCount == 0)//第一根手指離開&&沒有任何觸碰
        //{
        //    firstTouchedLastFrame = false;
        //}
        //if (secondTouchedLastFrame && Input.touchCount == 1)//第二根手指離開&&第一根手指還沒離開
        //{
        //    secondTouchedLastFrame = false;
        //}
        //if (thirdTouchedLastFrame && Input.touchCount == 2)//第三根手指離開&&第二根手指還沒離開
        //{
        //    thirdTouchedLastFrame = false;
        //}
        //if (fourthTouchedLastFrame && Input.touchCount == 3)//第四根手指離開&&第三根手指還沒離開
        //{
        //    fourthTouchedLastFrame = false;
        //}
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
        if (touchSensor)
        {
            print(hit.collider.transform.name);
            if (jugeArea == JugeArea.Perfect)
            {
                Debug.Log("Perfect" + "，觸碰位置: " + firstTouch.position);
                gameCanvas.rythmPoint += (int)Mathf.Round(other.GetComponentInParent<ObstaclePrefabDrop>().perfectPoint * gameCanvas.PerfectPointBounsMulti);

                gameCanvas.PerfectEffect();
                jugeArea = JugeArea.None;
                touchSensor = false;
                other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();

            }
            else if (jugeArea == JugeArea.Good)
            {
                Debug.Log("Perfect" + "，觸碰位置: " + firstTouch.position);
                gameCanvas.rythmPoint += other.GetComponentInParent<ObstaclePrefabDrop>().goodPoint;

                gameCanvas.GoodEffect();
                jugeArea = JugeArea.None;
                touchSensor = false;
                other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
            }
            else if (jugeArea == JugeArea.Miss)
            {
                Debug.Log("Perfect" + "，觸碰位置: " + firstTouch.position);
                
                if (gameCanvas.CurCharMissShield <= 0) 
                {
                    gameCanvas.CurCharMissShield = 0;
                    gameCanvas.rythmPoint += other.GetComponentInParent<ObstaclePrefabDrop>().missPoint; 
                }
                else
                {
                    gameCanvas.CurCharMissShield -= 1;
                }

                jugeArea = JugeArea.None;
                touchSensor = false;
                other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
            }
        }
        //if (!firstTouchedLastFrame && Input.touchCount == 1)//從沒有觸碰到第一根手指按下
        //{
        //    firstTouch = Input.GetTouch(0);
        //    if (firstTouch.position.x < leftMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Perfect)
        //    {
        //        Debug.Log("Perfect" + "，觸碰位置: " + firstTouch.position);
        //        gameCanvas.rythmPoint += other.GetComponentInParent<ObstaclePrefabDrop>().perfectPoint;

        //        gameCanvas.PerfectEffect();
        //        jugeArea = JugeArea.None;
        //        other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
        //    }
        //    else if (firstTouch.position.x < leftMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Good)
        //    {
        //        Debug.Log("Good" + "，觸碰位置: " + firstTouch.position);
        //        gameCanvas.rythmPoint += other.GetComponentInParent<ObstaclePrefabDrop>().goodPoint;

        //        gameCanvas.GoodEffect();
        //        jugeArea = JugeArea.None;
        //        other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
        //    }
        //    else if (firstTouch.position.x < leftMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Miss)
        //    {
        //        Debug.Log("Miss" + "，觸碰位置: " + firstTouch.position);
        //        gameCanvas.rythmPoint -= other.GetComponentInParent<ObstaclePrefabDrop>().missPoint;

        //        jugeArea = JugeArea.None;
        //        other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
        //    }
        //    firstTouchedLastFrame = true;
        //    //StartCoroutine(DelayedTriggerExit());
        //}
        //else if (firstTouchedLastFrame && Input.touchCount == 2 && !secondTouchedLastFrame)//第一下還按著接著到第二下進來
        //{
        //    //(!secondTouchedLastFrame && Input.touchCount == 1) || (!secondTouchedLastFrame && Input.touchCount == 2)
        //    secondTouch = Input.GetTouch(1);
        //    if (secondTouch.position.x < leftMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Perfect)
        //    {
        //        Debug.Log("Perfect" + "，觸碰位置: " + firstTouch.position);
        //        gameCanvas.rythmPoint += other.GetComponentInParent<ObstaclePrefabDrop>().perfectPoint;

        //        gameCanvas.PerfectEffect();
        //        jugeArea = JugeArea.None;
        //        other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
        //    }
        //    else if (secondTouch.position.x < leftMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Good)
        //    {
        //        Debug.Log("Good" + "，觸碰位置: " + firstTouch.position);
        //        gameCanvas.rythmPoint += other.GetComponentInParent<ObstaclePrefabDrop>().goodPoint;

        //        gameCanvas.GoodEffect();
        //        jugeArea = JugeArea.None;
        //        other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
        //    }
        //    else if (secondTouch.position.x < leftMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Miss)
        //    {
        //        Debug.Log("Miss" + "，觸碰位置: " + firstTouch.position);
        //        gameCanvas.rythmPoint -= other.GetComponentInParent<ObstaclePrefabDrop>().missPoint;

        //        jugeArea = JugeArea.None;
        //        other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
        //    }
        //    secondTouchedLastFrame = true;
        //}
        //else if (secondTouchedLastFrame && Input.touchCount == 3 && !thirdTouchedLastFrame)//第二下還按著接著到第三下進來
        //{
        //    thirTouch = Input.GetTouch(2);
        //    if (thirTouch.position.x < leftMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Perfect)
        //    {
        //        Debug.Log("Perfect" + "，觸碰位置: " + firstTouch.position);
        //        gameCanvas.rythmPoint += other.GetComponentInParent<ObstaclePrefabDrop>().perfectPoint;

        //        gameCanvas.PerfectEffect();
        //        jugeArea = JugeArea.None;
        //        other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
        //    }
        //    else if (thirTouch.position.x < leftMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Good)
        //    {
        //        Debug.Log("Good" + "，觸碰位置: " + firstTouch.position);
        //        gameCanvas.rythmPoint += other.GetComponentInParent<ObstaclePrefabDrop>().goodPoint;

        //        gameCanvas.GoodEffect();
        //        jugeArea = JugeArea.None;
        //        other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
        //    }
        //    else if (thirTouch.position.x < leftMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Miss)
        //    {
        //        Debug.Log("Miss" + "，觸碰位置: " + firstTouch.position);
        //        gameCanvas.rythmPoint -= other.GetComponentInParent<ObstaclePrefabDrop>().missPoint;

        //        jugeArea = JugeArea.None;
        //        other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
        //    }
        //    thirdTouchedLastFrame = true;
        //}
        //else if (thirdTouchedLastFrame && Input.touchCount == 4 && !fourthTouchedLastFrame)//第三下還按著接著到第四下進來
        //{
        //    fourthTouch = Input.GetTouch(3);
        //    if (fourthTouch.position.x < leftMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Perfect)
        //    {
        //        Debug.Log("Perfect" + "，觸碰位置: " + firstTouch.position);
        //        gameCanvas.rythmPoint += other.GetComponentInParent<ObstaclePrefabDrop>().perfectPoint;

        //        gameCanvas.PerfectEffect();
        //        jugeArea = JugeArea.None;
        //        other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
        //    }
        //    else if (fourthTouch.position.x < leftMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Good)
        //    {
        //        Debug.Log("Good" + "，觸碰位置: " + firstTouch.position);
        //        gameCanvas.rythmPoint += other.GetComponentInParent<ObstaclePrefabDrop>().goodPoint;

        //        gameCanvas.GoodEffect();
        //        jugeArea = JugeArea.None;
        //        other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
        //    }
        //    else if (fourthTouch.position.x < leftMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Miss)
        //    {
        //        Debug.Log("Miss" + "，觸碰位置: " + firstTouch.position);
        //        gameCanvas.rythmPoint -= other.GetComponentInParent<ObstaclePrefabDrop>().missPoint;

        //        jugeArea = JugeArea.None;
        //        other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
        //    }
        //    fourthTouchedLastFrame = true;
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform.CompareTag("PerfectArea") && jugeArea == JugeArea.Perfect)
        {
            jugeArea = JugeArea.Good;
        }
        else if(other.transform.CompareTag("GoodArea") && jugeArea == JugeArea.Good)
        {
            jugeArea = JugeArea.Miss;
        }
        else if(other.transform.CompareTag("MissArea") && jugeArea == JugeArea.Miss)
        {
            jugeArea = JugeArea.None;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorLL : MonoBehaviour
{
    RythmGameCanvas gameCanvas;

    //記錄左側螢幕中間的X軸
    float leftMidScreenPosX;

    Touch firstTouch;

    [SerializeField] bool touched;
    public bool playerTouched { get => touched; }
    public GameObject rootUI;
    public int testBonusPoint;

    bool touchedLastFrame = false;

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

        gameCanvas = rootUI.GetComponent<RythmGameCanvas>();
        touched = false;
        //左側螢幕中間X軸
        leftMidScreenPosX = Screen.width / 4f;
        jugeArea = JugeArea.None;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(jugeArea);
        //if (touchedLastFrame && Input.touchCount == 0)
        //{
        //    touchedLastFrame = false;
        //}
        //else if (!touchedLastFrame && Input.touchCount > 0)
        //{
        //    //這裡只會偵測第一次按下

        //    touchedLastFrame = true;
        //}
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.transform.CompareTag("Obstacle") && Input.touchCount > 0)
    //    {
    //        firstTouch = Input.GetTouch(0);
    //        if (firstTouch.position.x < leftMidScreenPosX && !touched)
    //        {
    //            Debug.Log("Good" + "，觸碰位置: " + firstTouch.position);
    //            testBonusPoint++;
    //            touched = true;
    //            gameCanvas.GoodEffect();
    //            //Destroy(other.gameObject);
    //            other.gameObject.SetActive(false);
    //        }
    //        StartCoroutine(DelayedTriggerExit());
    //    }
    //}
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
        if (touchedLastFrame && Input.touchCount == 0)
        {
            touchedLastFrame = false;
        }
        else if (!touchedLastFrame && Input.touchCount > 0)
        {
            //這裡只會偵測第一次按下

            firstTouch = Input.GetTouch(0);
            if (firstTouch.position.x < leftMidScreenPosX && !touched && jugeArea == JugeArea.Perfect)
            {
                Debug.Log("Perfect" + "，觸碰位置: " + firstTouch.position);
                testBonusPoint += 5;
                touched = true;
                gameCanvas.PerfectEffect();
                jugeArea = JugeArea.None;
                other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
            }
            else if (firstTouch.position.x < leftMidScreenPosX && !touched && jugeArea == JugeArea.Good)
            {
                Debug.Log("Good" + "，觸碰位置: " + firstTouch.position);
                testBonusPoint += 1;
                touched = true;
                gameCanvas.GoodEffect();
                jugeArea = JugeArea.None;
                other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
            }
            else if (firstTouch.position.x < leftMidScreenPosX && !touched && jugeArea == JugeArea.Miss)
            {
                Debug.Log("Miss" + "，觸碰位置: " + firstTouch.position);
                touched = true;
                jugeArea = JugeArea.None;
                other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj(); 
            }
            StartCoroutine(DelayedTriggerExit());
            touchedLastFrame = true;
        } 
    }
    //private void OnTriggerExit(Collider other)
    //{
    //    if (touchedLastFrame && Input.touchCount == 0)
    //    {
    //        touchedLastFrame = false;
    //    }
    //    else if (!touchedLastFrame && Input.touchCount > 0)
    //    {
    //        //這裡只會偵測第一次按下

    //        firstTouch = Input.GetTouch(0);
    //       if (firstTouch.position.x < leftMidScreenPosX && !touched)
    //        {
    //            Debug.Log("Miss" + "，觸碰位置: " + firstTouch.position);
    //            touched = true;
    //            jugeArea = JugeArea.None;
    //            other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
    //        }
    //        StartCoroutine(DelayedTriggerExit());
    //        touchedLastFrame = true;
    //    }
    //}
    
    IEnumerator DelayedTriggerExit()
    {
        yield return new WaitForSeconds(0.2f);
        touched = false;
    }

}

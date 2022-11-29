using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorR : MonoBehaviour
{   
    RythmGameCanvas gameCanvas;

    //�O���k���ù�������X�b
    float rightMidScreenPosX;
    //�O���ù�������X�b
    float midScreenPosX;
    //�O���ù�������Y�b
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
        //�k���ù�����X�b
        rightMidScreenPosX = Screen.width * 3 / 4f;
        //��l�ù�����X�b
        midScreenPosX = Screen.width / 2f;
        //�ù�������Y�b
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
    //            Debug.Log("Perfect" + "�AĲ�I��m: " + firstTouch.position);
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
            //�o�̥u�|�����Ĥ@�����U
            firstTouch = Input.GetTouch(0);
            if (firstTouch.position.x > midScreenPosX && firstTouch.position.x < rightMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Perfect)
            {
                Debug.Log("Perfect" + "�AĲ�I��m: " + firstTouch.position);
                gameCanvas.rythmPoint += 5;

                gameCanvas.PerfectEffect();
                jugeArea = JugeArea.None;
                other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
            }
            else if (firstTouch.position.x > midScreenPosX && firstTouch.position.x < rightMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Good)
            {
                Debug.Log("Good" + "�AĲ�I��m: " + firstTouch.position);
                gameCanvas.rythmPoint += 1;

                gameCanvas.GoodEffect();
                jugeArea = JugeArea.None;
                other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
            }
            else if (firstTouch.position.x > midScreenPosX && firstTouch.position.x < rightMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Miss)
            {
                Debug.Log("Miss" + "�AĲ�I��m: " + firstTouch.position);

                jugeArea = JugeArea.None;
                other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
            }
            firstTouchedLastFrame = true;
            //StartCoroutine(DelayedTriggerExit());
        }
        else if (firstTouchedLastFrame && Input.touchCount == 2 && !secondTouchedLastFrame)//�Ĥ@�U�٫��۱��ۨ�ĤG�U�i��
        {
            secondTouch = Input.GetTouch(1);
            if (secondTouch.position.x > midScreenPosX && secondTouch.position.x < rightMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Perfect)
            {
                Debug.Log("Perfect" + "�AĲ�I��m: " + firstTouch.position);
                gameCanvas.rythmPoint += 5;

                gameCanvas.PerfectEffect();
                jugeArea = JugeArea.None;
                other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
            }
            else if (secondTouch.position.x > midScreenPosX && secondTouch.position.x < rightMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Good)
            {
                Debug.Log("Good" + "�AĲ�I��m: " + firstTouch.position);
                gameCanvas.rythmPoint += 1;

                gameCanvas.GoodEffect();
                jugeArea = JugeArea.None;
                other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
            }
            else if (secondTouch.position.x > midScreenPosX && secondTouch.position.x < rightMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Miss)
            {
                Debug.Log("Miss" + "�AĲ�I��m: " + firstTouch.position);

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
    //            Debug.Log("Good" + "�AĲ�I��m: " + firstTouch.position);
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

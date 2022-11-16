using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorRR : MonoBehaviour
{
    RythmGameCanvas gameCanvas;

    //�O���k���ù�������X�b
    float rightMidScreenPosX;

    Touch firstTouch;

    bool touched;

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
        //�k���ù�����X�b
        rightMidScreenPosX = Screen.width * 3 / 4f;
        jugeArea = JugeArea.None;
    }
    void Update()
    {

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

    //        if (firstTouch.position.x > rightMidScreenPosX && !touched)
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
        if (touchedLastFrame && Input.touchCount == 0)
        {
            touchedLastFrame = false;
        }
        else if (!touchedLastFrame && Input.touchCount > 0)
        {
            //�o�̥u�|�����Ĥ@�����U

            firstTouch = Input.GetTouch(0);
            if (firstTouch.position.x > rightMidScreenPosX && !touched && jugeArea == JugeArea.Perfect)
            {
                Debug.Log("Perfect" + "�AĲ�I��m: " + firstTouch.position);
                testBonusPoint += 5;
                touched = true;
                gameCanvas.PerfectEffect();
                jugeArea = JugeArea.None;
                other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
            }
            else if (firstTouch.position.x > rightMidScreenPosX && !touched && jugeArea == JugeArea.Good)
            {
                Debug.Log("Good" + "�AĲ�I��m: " + firstTouch.position);
                testBonusPoint += 1;
                touched = true;
                gameCanvas.GoodEffect();
                jugeArea = JugeArea.None;
                other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
            }
            else if (firstTouch.position.x > rightMidScreenPosX && !touched && jugeArea == JugeArea.Miss)
            {
                Debug.Log("Miss" + "�AĲ�I��m: " + firstTouch.position);
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
    //    if (other.transform.CompareTag("MissArea") && Input.touchCount > 0)
    //    {
    //        firstTouch = Input.GetTouch(0);

    //        if (firstTouch.position.x > rightMidScreenPosX && !touched)
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
    IEnumerator DelayedTriggerExit()
    {
        yield return new WaitForSeconds(0.2f);
        touched = false;
    }
}

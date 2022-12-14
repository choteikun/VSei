using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorRR : MonoBehaviour
{
    RythmGameCanvas gameCanvas;

    public GameObject gameSceneRootUI;
    public MyAccount myAccount;

    //�O���k���ù�������X�b
    float rightMidScreenPosX;
    //�O���ù�������Y�b
    float midScreenPosY;

    Ray ray;
    RaycastHit hit;
    bool touchSensor;

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
        //�k���ù�����X�b
        rightMidScreenPosX = Screen.width * 3 / 4f;
        //�ù�������Y�b
        midScreenPosY = Screen.height / 2;
        jugeArea = JugeArea.None;
    }
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
                    if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("SensorRR"))
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
            if (jugeArea == JugeArea.Perfect)
            {
                Debug.Log("PerfectRR" + "�AĲ�I��m: " + hit.transform.position);
                gameCanvas.rythmPoint += (int)Mathf.Round(other.GetComponentInParent<ObstaclePrefabDrop>().perfectPoint * gameCanvas.PerfectPointBounsMulti);

                if (other.GetComponentInParent<ObstaclePrefabDrop>().name == "SpecialBeat(Clone)")
                {
                    gameCanvas.curSpecialCount++;
                }

                gameCanvas.PerfectEffect();
                jugeArea = JugeArea.None;
                touchSensor = false;
                other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
                
            }
            else if (jugeArea == JugeArea.Good)
            {
                Debug.Log("GoodRR" + "�AĲ�I��m: " + hit.transform.position);
                gameCanvas.rythmPoint += other.GetComponentInParent<ObstaclePrefabDrop>().goodPoint;
                if (other.GetComponentInParent<ObstaclePrefabDrop>().name == "SpecialBeat(Clone)")
                {
                    gameCanvas.curSpecialCount++;
                }

                gameCanvas.GoodEffect();
                jugeArea = JugeArea.None;
                touchSensor = false;
                other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
                
            }
            else if (jugeArea == JugeArea.Miss)
            {
                Debug.Log("MissRR" + "�AĲ�I��m: " + hit.transform.position);
                if (gameCanvas.CurCharMissShield <= 0)
                {
                    gameCanvas.CurCharMissShield = 0;
                    gameCanvas.rythmPoint += other.GetComponentInParent<ObstaclePrefabDrop>().missPoint;
                }
                else
                {
                    gameCanvas.CurCharMissShield -= 1;
                }

                gameCanvas.MissEffect();
                jugeArea = JugeArea.None;
                touchSensor = false;
                other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
            }
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("PerfectArea") && jugeArea == JugeArea.Perfect)
        {
            jugeArea = JugeArea.Good;
        }
        else if (other.transform.CompareTag("GoodArea") && jugeArea == JugeArea.Good)
        {
            jugeArea = JugeArea.Miss;
        }
        else if (other.transform.CompareTag("MissArea") && jugeArea == JugeArea.Miss)
        {
            jugeArea = JugeArea.None;
        }
    }
}

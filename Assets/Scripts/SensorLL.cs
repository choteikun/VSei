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

    //�O�������ù�������X�b
    float leftMidScreenPosX;
    //�O���ù�������Y�b
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
        //�����ù�����X�b
        leftMidScreenPosX = Screen.width / 4f;
        //�ù�������Y�b
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
        //if (firstTouchedLastFrame && Input.touchCount == 0)//�Ĥ@�ڤ�����}&&�S������Ĳ�I
        //{
        //    firstTouchedLastFrame = false;
        //}
        //if (secondTouchedLastFrame && Input.touchCount == 1)//�ĤG�ڤ�����}&&�Ĥ@�ڤ���٨S���}
        //{
        //    secondTouchedLastFrame = false;
        //}
        //if (thirdTouchedLastFrame && Input.touchCount == 2)//�ĤT�ڤ�����}&&�ĤG�ڤ���٨S���}
        //{
        //    thirdTouchedLastFrame = false;
        //}
        //if (fourthTouchedLastFrame && Input.touchCount == 3)//�ĥ|�ڤ�����}&&�ĤT�ڤ���٨S���}
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
                Debug.Log("Perfect" + "�AĲ�I��m: " + firstTouch.position);
                gameCanvas.rythmPoint += (int)Mathf.Round(other.GetComponentInParent<ObstaclePrefabDrop>().perfectPoint * gameCanvas.PerfectPointBounsMulti);

                gameCanvas.PerfectEffect();
                jugeArea = JugeArea.None;
                touchSensor = false;
                other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();

            }
            else if (jugeArea == JugeArea.Good)
            {
                Debug.Log("Perfect" + "�AĲ�I��m: " + firstTouch.position);
                gameCanvas.rythmPoint += other.GetComponentInParent<ObstaclePrefabDrop>().goodPoint;

                gameCanvas.GoodEffect();
                jugeArea = JugeArea.None;
                touchSensor = false;
                other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
            }
            else if (jugeArea == JugeArea.Miss)
            {
                Debug.Log("Perfect" + "�AĲ�I��m: " + firstTouch.position);
                
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
        //if (!firstTouchedLastFrame && Input.touchCount == 1)//�q�S��Ĳ�I��Ĥ@�ڤ�����U
        //{
        //    firstTouch = Input.GetTouch(0);
        //    if (firstTouch.position.x < leftMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Perfect)
        //    {
        //        Debug.Log("Perfect" + "�AĲ�I��m: " + firstTouch.position);
        //        gameCanvas.rythmPoint += other.GetComponentInParent<ObstaclePrefabDrop>().perfectPoint;

        //        gameCanvas.PerfectEffect();
        //        jugeArea = JugeArea.None;
        //        other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
        //    }
        //    else if (firstTouch.position.x < leftMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Good)
        //    {
        //        Debug.Log("Good" + "�AĲ�I��m: " + firstTouch.position);
        //        gameCanvas.rythmPoint += other.GetComponentInParent<ObstaclePrefabDrop>().goodPoint;

        //        gameCanvas.GoodEffect();
        //        jugeArea = JugeArea.None;
        //        other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
        //    }
        //    else if (firstTouch.position.x < leftMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Miss)
        //    {
        //        Debug.Log("Miss" + "�AĲ�I��m: " + firstTouch.position);
        //        gameCanvas.rythmPoint -= other.GetComponentInParent<ObstaclePrefabDrop>().missPoint;

        //        jugeArea = JugeArea.None;
        //        other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
        //    }
        //    firstTouchedLastFrame = true;
        //    //StartCoroutine(DelayedTriggerExit());
        //}
        //else if (firstTouchedLastFrame && Input.touchCount == 2 && !secondTouchedLastFrame)//�Ĥ@�U�٫��۱��ۨ�ĤG�U�i��
        //{
        //    //(!secondTouchedLastFrame && Input.touchCount == 1) || (!secondTouchedLastFrame && Input.touchCount == 2)
        //    secondTouch = Input.GetTouch(1);
        //    if (secondTouch.position.x < leftMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Perfect)
        //    {
        //        Debug.Log("Perfect" + "�AĲ�I��m: " + firstTouch.position);
        //        gameCanvas.rythmPoint += other.GetComponentInParent<ObstaclePrefabDrop>().perfectPoint;

        //        gameCanvas.PerfectEffect();
        //        jugeArea = JugeArea.None;
        //        other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
        //    }
        //    else if (secondTouch.position.x < leftMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Good)
        //    {
        //        Debug.Log("Good" + "�AĲ�I��m: " + firstTouch.position);
        //        gameCanvas.rythmPoint += other.GetComponentInParent<ObstaclePrefabDrop>().goodPoint;

        //        gameCanvas.GoodEffect();
        //        jugeArea = JugeArea.None;
        //        other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
        //    }
        //    else if (secondTouch.position.x < leftMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Miss)
        //    {
        //        Debug.Log("Miss" + "�AĲ�I��m: " + firstTouch.position);
        //        gameCanvas.rythmPoint -= other.GetComponentInParent<ObstaclePrefabDrop>().missPoint;

        //        jugeArea = JugeArea.None;
        //        other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
        //    }
        //    secondTouchedLastFrame = true;
        //}
        //else if (secondTouchedLastFrame && Input.touchCount == 3 && !thirdTouchedLastFrame)//�ĤG�U�٫��۱��ۨ�ĤT�U�i��
        //{
        //    thirTouch = Input.GetTouch(2);
        //    if (thirTouch.position.x < leftMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Perfect)
        //    {
        //        Debug.Log("Perfect" + "�AĲ�I��m: " + firstTouch.position);
        //        gameCanvas.rythmPoint += other.GetComponentInParent<ObstaclePrefabDrop>().perfectPoint;

        //        gameCanvas.PerfectEffect();
        //        jugeArea = JugeArea.None;
        //        other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
        //    }
        //    else if (thirTouch.position.x < leftMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Good)
        //    {
        //        Debug.Log("Good" + "�AĲ�I��m: " + firstTouch.position);
        //        gameCanvas.rythmPoint += other.GetComponentInParent<ObstaclePrefabDrop>().goodPoint;

        //        gameCanvas.GoodEffect();
        //        jugeArea = JugeArea.None;
        //        other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
        //    }
        //    else if (thirTouch.position.x < leftMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Miss)
        //    {
        //        Debug.Log("Miss" + "�AĲ�I��m: " + firstTouch.position);
        //        gameCanvas.rythmPoint -= other.GetComponentInParent<ObstaclePrefabDrop>().missPoint;

        //        jugeArea = JugeArea.None;
        //        other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
        //    }
        //    thirdTouchedLastFrame = true;
        //}
        //else if (thirdTouchedLastFrame && Input.touchCount == 4 && !fourthTouchedLastFrame)//�ĤT�U�٫��۱��ۨ�ĥ|�U�i��
        //{
        //    fourthTouch = Input.GetTouch(3);
        //    if (fourthTouch.position.x < leftMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Perfect)
        //    {
        //        Debug.Log("Perfect" + "�AĲ�I��m: " + firstTouch.position);
        //        gameCanvas.rythmPoint += other.GetComponentInParent<ObstaclePrefabDrop>().perfectPoint;

        //        gameCanvas.PerfectEffect();
        //        jugeArea = JugeArea.None;
        //        other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
        //    }
        //    else if (fourthTouch.position.x < leftMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Good)
        //    {
        //        Debug.Log("Good" + "�AĲ�I��m: " + firstTouch.position);
        //        gameCanvas.rythmPoint += other.GetComponentInParent<ObstaclePrefabDrop>().goodPoint;

        //        gameCanvas.GoodEffect();
        //        jugeArea = JugeArea.None;
        //        other.GetComponentInParent<ObstaclePrefabDrop>().SetActiveFalseObj();
        //    }
        //    else if (fourthTouch.position.x < leftMidScreenPosX && firstTouch.position.y < midScreenPosY && jugeArea == JugeArea.Miss)
        //    {
        //        Debug.Log("Miss" + "�AĲ�I��m: " + firstTouch.position);
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

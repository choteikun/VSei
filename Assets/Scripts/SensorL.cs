using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorL : MonoBehaviour
{ 
    RythmGameCanvas gameCanvas;

    //記錄左側螢幕中間的X軸
    float leftMidScreenPosX;
    //記錄螢幕中間的X軸
    float midScreenPosX;


    Touch firstTouch;

    [SerializeField]bool touched;
    public bool playerTouched { get => touched; }
    public GameObject rootUI;
    public int testBonusPoint;
   


    // Start is called before the first frame update
    void Start()
    {
        gameCanvas = rootUI.GetComponent<RythmGameCanvas>();
        touched = false;
        //左側螢幕中間X軸
        leftMidScreenPosX = Screen.width / 4f;
        //初始螢幕中間X軸
        midScreenPosX = Screen.width / 2f;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Obstacle" && Input.touchCount > 0)
        {
            firstTouch = Input.GetTouch(0);

            if (firstTouch.position.x < midScreenPosX && firstTouch.position.x > leftMidScreenPosX && !touched) 
            {
                Debug.Log("Good" + "，觸碰位置: " + firstTouch.position);
                testBonusPoint++;
                touched = true;
                gameCanvas.GoodEffect();
                other.gameObject.SetActive(false);
            }
            StartCoroutine(DelayedTriggerExit());
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Obstacle" && Input.touchCount > 0)
        {
            firstTouch = Input.GetTouch(0);
            
            if (firstTouch.position.x < midScreenPosX && firstTouch.position.x > leftMidScreenPosX && !touched)
            {
                Debug.Log("Perfect" + "，觸碰位置: " + firstTouch.position);
                testBonusPoint++;
                touched = true;
                gameCanvas.PerfectEffect();
                other.gameObject.SetActive(false);
            }
            StartCoroutine(DelayedTriggerExit());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Obstacle" && Input.touchCount > 0)
        {
            firstTouch = Input.GetTouch(0);
            
            if (firstTouch.position.x < midScreenPosX && firstTouch.position.x > leftMidScreenPosX && !touched)
            {
                Debug.Log("Good" + "，觸碰位置: " + firstTouch.position);
                testBonusPoint++;
                touched = true;
                gameCanvas.GoodEffect();
                other.gameObject.SetActive(false);
            }
            StartCoroutine(DelayedTriggerExit());
        }
    }
    IEnumerator DelayedTriggerExit()
    {
        yield return new WaitForSeconds(0.2f);
        touched = false;
    }
}

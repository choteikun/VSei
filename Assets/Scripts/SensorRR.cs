using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorRR : MonoBehaviour
{
    RythmGameCanvas gameCanvas;

    //°O¿ý¥k°¼¿Ã¹õ¤¤¶¡ªºX¶b
    float rightMidScreenPosX;

    Touch firstTouch;

    bool touched;

    public bool playerTouched { get => touched; }
    public GameObject rootUI;
    public int testBonusPoint;

    // Start is called before the first frame update
    void Start()
    {
        gameCanvas = rootUI.GetComponent<RythmGameCanvas>();
        touched = false;
        //¥k°¼¿Ã¹õ¤¤¶¡X¶b
        rightMidScreenPosX = Screen.width * 3 / 4f;
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

            if (firstTouch.position.x > rightMidScreenPosX && !touched)
            {
                Debug.Log("Good" + "¡AÄ²¸I¦ì¸m: " + firstTouch.position);
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

            if (firstTouch.position.x > rightMidScreenPosX && !touched)
            {
                Debug.Log("Perfect" + "¡AÄ²¸I¦ì¸m: " + firstTouch.position);
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

            if (firstTouch.position.x > rightMidScreenPosX && !touched)
            {
                Debug.Log("Good" + "¡AÄ²¸I¦ì¸m: " + firstTouch.position);
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

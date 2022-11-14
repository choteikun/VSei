using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorLL : MonoBehaviour
{
    RythmGameCanvas gameCanvas;

    //°O¿ý¥ª°¼¿Ã¹õ¤¤¶¡ªºX¶b
    float leftMidScreenPosX;

    Touch firstTouch;

    [SerializeField] bool touched;
    public bool playerTouched { get => touched; }
    public GameObject rootUI;
    public int testBonusPoint;
   


    // Start is called before the first frame update
    void Start()
    {
        gameCanvas = rootUI.GetComponent<RythmGameCanvas>();
        touched = false;
        //¥ª°¼¿Ã¹õ¤¤¶¡X¶b
        leftMidScreenPosX = Screen.width / 4f;
        
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

            if (firstTouch.position.x < leftMidScreenPosX && !touched)
            {
                Debug.Log("Good" + "¡AÄ²¸I¦ì¸m: " + firstTouch.position);
                testBonusPoint++;
                touched = true;
                gameCanvas.GoodEffect();
                //Destroy(other.gameObject);
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

            if (firstTouch.position.x < leftMidScreenPosX && !touched)
            {
                Debug.Log("Perfect" + "¡AÄ²¸I¦ì¸m: " + firstTouch.position);
                testBonusPoint++;
                touched = true;
                gameCanvas.PerfectEffect();
                //Destroy(other.gameObject);
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

            if (firstTouch.position.x < leftMidScreenPosX && !touched)
            {
                Debug.Log("Good" + "¡AÄ²¸I¦ì¸m: " + firstTouch.position);
                testBonusPoint++;
                touched = true;
                gameCanvas.GoodEffect();
                //Destroy(other.gameObject);
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

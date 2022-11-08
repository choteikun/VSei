using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorR : MonoBehaviour
{
    public int testBonusPoint;
    //�O���ù�������X�b
    private float midScreenPosX;
    Touch firstTouch;
    bool touched;
    public bool playerTouched { get => touched; }

    // Start is called before the first frame update
    void Start()
    {
        touched = false;
        //��l�ù�����X�b
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
            
            if (firstTouch.position.x > midScreenPosX && !touched)
            {
                Debug.Log("Good" + "�AĲ�I��m: " + firstTouch.position);
                testBonusPoint++;
                touched = true;
                Destroy(other.gameObject);
            }
            StartCoroutine(DelayedTriggerExit());
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Obstacle" && Input.touchCount > 0)
        {
            firstTouch = Input.GetTouch(0);
            
            if (firstTouch.position.x > midScreenPosX && !touched)
            {
                Debug.Log("Perfect" + "�AĲ�I��m: " + firstTouch.position);
                testBonusPoint++;
                touched = true;
                Destroy(other.gameObject);
            }
            StartCoroutine(DelayedTriggerExit());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Obstacle" && Input.touchCount > 0)
        {
            firstTouch = Input.GetTouch(0);
            
            if (firstTouch.position.x > midScreenPosX && !touched)
            {
                Debug.Log("Good" + "�AĲ�I��m: " + firstTouch.position);
                testBonusPoint++;
                touched = true;
                Destroy(other.gameObject);
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
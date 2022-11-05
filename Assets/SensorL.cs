using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorL : MonoBehaviour
{
    public int testBonusPoint;
    //�O���ù�������X�b
    private float midScreenPosX;
    Touch firstTouch;
    bool touched;

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
            Debug.Log("Ĳ�I�˸m��" + "�AĲ�I��m: " + firstTouch.position);
            if(firstTouch.position.x < midScreenPosX && !touched)
            {
                testBonusPoint++;
                touched = true;
            }    
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Obstacle" && Input.touchCount > 0)
        {
            firstTouch = Input.GetTouch(0);
            Debug.Log("Ĳ�I�˸m��" + "�AĲ�I��m: " + firstTouch.position);
            if (firstTouch.position.x < midScreenPosX && !touched)
            {
                testBonusPoint++;
                touched = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Obstacle" && Input.touchCount > 0)
        {
            firstTouch = Input.GetTouch(0);
            Debug.Log("Ĳ�I�˸m��" + "�AĲ�I��m: " + firstTouch.position);
            if (firstTouch.position.x < midScreenPosX && !touched)
            {
                testBonusPoint++;
                touched = true;
            }
        }
        if (other.transform.tag == "Obstacle")
        {
            StartCoroutine(DelayedTriggerExit());
        }
    }
    IEnumerator DelayedTriggerExit()
    {
        yield return new WaitForSeconds(0.1f);
        touched = false;
    }
}

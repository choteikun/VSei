using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMovement : MonoBehaviour
{
    public float movementSpeed;

    public float minPosX;
    public float maxPosX;

    public int testBonusPoint;
    //�O���ù�������X�b
    private float midScreenPosX;


    void Start()
    {
        //��l�ù�����X�b
        midScreenPosX = Screen.width / 2f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float movement = 0f;

        if (Input.touchCount > 0)//���a���bĲ���˸m
        {
            Touch firstTouch = Input.GetTouch(0); //���o���a�Ĥ@��Ĳ���I
            Debug.Log("Ĳ�I�˸m��"+"�AĲ�I��m: "+ firstTouch.position);    

            //�ˬdĲ���I����m�b�ù��������٬O�k��A�íp��X���ʭ�
            movement = (firstTouch.position.x < midScreenPosX ? -1f : 1f) * movementSpeed * Time.deltaTime;

            Vector3 newPos = new Vector3(Mathf.Clamp(transform.position.x + movement, minPosX, maxPosX),transform.position.y, transform.position.z);
            transform.position = newPos;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Obstacle")
        {
            testBonusPoint++;
        }
    }
}

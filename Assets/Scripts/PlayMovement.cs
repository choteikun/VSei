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
    float midScreenPosX;
    Vector2 vecDeltaArea;


    void Start()
    {
        //��l�ù�����X�b
        midScreenPosX = Screen.width / 2f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float movement = 0f;

        //if (Input.touchCount > 0)//���a���bĲ���˸m
        //{
        //    Touch firstTouch = Input.GetTouch(0); //���o���a�Ĥ@��Ĳ���I
        //    Debug.Log("Ĳ�I�˸m��"+"�AĲ�I��m: "+ firstTouch.position);    

        //    //�ˬdĲ���I����m�b�ù��������٬O�k��A�íp��X���ʭ�
        //    movement = (firstTouch.position.x < midScreenPosX ? -1f : 1f) * movementSpeed * Time.deltaTime;

        //    Vector3 newPos = new Vector3(Mathf.Clamp(transform.position.x + movement, minPosX, maxPosX),transform.position.y, transform.position.z);
        //    transform.position = newPos;
        //}
        //if (Input.touches[0].phase == TouchPhase.Moved)//���a���b�ưʸ˸m
        //{
        //    Touch firstTouch = Input.GetTouch(0); //���o���a�Ĥ@��Ĳ���I
        //    if (Input.touches[0].phase == TouchPhase.Ended)
        //    {
        //        Touch endedTouch = Input.GetTouch(0); //���o���a�̫�@��Ĳ���I

        //        movement = (endedTouch.position.x < firstTouch.position.x ? -1f : 1f) * movementSpeed * Time.deltaTime;//�ˬdĲ���I����m�b�ù��������٬O�k��A�íp��X���ʭ�

        //        Vector3 newPos = new Vector3(Mathf.Clamp(transform.position.x + movement, minPosX, maxPosX), transform.position.y, transform.position.z);
        //        transform.position = newPos;
        //    }   
        //}

        if (Input.touchCount == 1 && (Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            vecDeltaArea = Vector2.zero;           
        }
        if (Input.touchCount == 1 && (Input.GetTouch(0).phase == TouchPhase.Moved))
        {
            vecDeltaArea.x += Input.GetTouch(0).deltaPosition.x;
            if (vecDeltaArea.x > 50)
            {
                movement = movementSpeed * Time.deltaTime;
                Vector3 newPos = new Vector3(Mathf.Clamp(transform.position.x + movement, minPosX, maxPosX), transform.position.y, transform.position.z);
                transform.position = newPos;
                Debug.Log("�k��");
            }
            else if (vecDeltaArea.x < -50)
            {
                movement = -movementSpeed * Time.deltaTime;
                Vector3 newPos = new Vector3(Mathf.Clamp(transform.position.x + movement, minPosX, maxPosX), transform.position.y, transform.position.z);
                transform.position = newPos;
                Debug.Log("����");
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Obstacle")
        {
            testBonusPoint++;
        }
    }
    //IEnumerator RetrunPosZero()
    //{
    //    yield return new WaitForSeconds(0.1f);
    //    transform.position = new Vector3(0, -9, 0);
    //}
}

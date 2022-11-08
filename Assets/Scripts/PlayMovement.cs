using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMovement : MonoBehaviour
{
    public float movementSpeed;

    public float minPosX;
    public float maxPosX;

    public int testBonusPoint;
    //記錄螢幕中間的X軸
    float midScreenPosX;
    Vector2 vecDeltaArea;


    void Start()
    {
        //初始螢幕中間X軸
        midScreenPosX = Screen.width / 2f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float movement = 0f;

        //if (Input.touchCount > 0)//玩家正在觸控裝置
        //{
        //    Touch firstTouch = Input.GetTouch(0); //取得玩家第一個觸控點
        //    Debug.Log("觸碰裝置中"+"，觸碰位置: "+ firstTouch.position);    

        //    //檢查觸控點的位置在螢幕的左方還是右方，並計算出移動值
        //    movement = (firstTouch.position.x < midScreenPosX ? -1f : 1f) * movementSpeed * Time.deltaTime;

        //    Vector3 newPos = new Vector3(Mathf.Clamp(transform.position.x + movement, minPosX, maxPosX),transform.position.y, transform.position.z);
        //    transform.position = newPos;
        //}
        //if (Input.touches[0].phase == TouchPhase.Moved)//玩家正在滑動裝置
        //{
        //    Touch firstTouch = Input.GetTouch(0); //取得玩家第一個觸控點
        //    if (Input.touches[0].phase == TouchPhase.Ended)
        //    {
        //        Touch endedTouch = Input.GetTouch(0); //取得玩家最後一個觸控點

        //        movement = (endedTouch.position.x < firstTouch.position.x ? -1f : 1f) * movementSpeed * Time.deltaTime;//檢查觸控點的位置在螢幕的左方還是右方，並計算出移動值

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
                Debug.Log("右滑");
            }
            else if (vecDeltaArea.x < -50)
            {
                movement = -movementSpeed * Time.deltaTime;
                Vector3 newPos = new Vector3(Mathf.Clamp(transform.position.x + movement, minPosX, maxPosX), transform.position.y, transform.position.z);
                transform.position = newPos;
                Debug.Log("左滑");
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

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
    private float midScreenPosX;


    void Start()
    {
        //初始螢幕中間X軸
        midScreenPosX = Screen.width / 2f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float movement = 0f;

        if (Input.touchCount > 0)//玩家正在觸控裝置
        {
            Touch firstTouch = Input.GetTouch(0); //取得玩家第一個觸控點
            Debug.Log("觸碰裝置中"+"，觸碰位置: "+ firstTouch.position);    

            //檢查觸控點的位置在螢幕的左方還是右方，並計算出移動值
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

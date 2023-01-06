using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePrefabDrop : MonoBehaviour
{
    public MyAccount myAccount;

    [Tooltip("節拍掉落速度")]
    public float obstacleDropSpeed;
    [Tooltip("節拍Perfect獲得分數")]
    public int perfectPoint;
    [Tooltip("節拍Good獲得分數")]
    public int goodPoint;
    [Tooltip("節拍Miss扣分")]
    public int missPoint;
    [Tooltip("是否Miss了該物件")]
    public bool obstacleMiss;

    //public SensorL sensorL;
    //public SensorR sensorR;
    BoxCollider missArea;
    BoxCollider goodArea;
    BoxCollider perfectArea;

    void Start()
    {
        if (!myAccount)
        {
            myAccount = Resources.Load<MyAccount>("CharacterScriptObjsInfo/MyAccount");
        }

        missArea = GetComponentsInChildren<BoxCollider>()[2];
        goodArea = GetComponentsInChildren<BoxCollider>()[1];
        perfectArea = GetComponentsInChildren<BoxCollider>()[0];

        obstacleMiss = false;
        //sensorL = GameObject.FindGameObjectWithTag("SensorL").GetComponent<SensorL>();
        //sensorR = GameObject.FindGameObjectWithTag("SensorR").GetComponent<SensorR>();

        //var euler = transform.eulerAngles;
        //euler.z = Random.Range(0.0f, 360.0f);
        //transform.eulerAngles = euler;
    }
    void Update()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // 如果射線與平面碰撞，列印碰撞物體資訊  
            //Debug.Log("碰撞物件: " + hit.collider.name);
            // 在場景檢視中繪製射線  
            Debug.DrawLine(ray.origin, hit.point, Color.red);
            if (hit.collider.CompareTag("PerfectArea")|| hit.collider.CompareTag("GoodArea")|| hit.collider.CompareTag("MissArea"))
            {
                missArea.enabled = false;
                goodArea.enabled = false;
                perfectArea.enabled = false;
            }
            else
            {
                missArea.enabled = true;
                goodArea.enabled = true;
                perfectArea.enabled = true;
            }
        }

        transform.Translate(new Vector3(0f, (0f - obstacleDropSpeed) * Time.deltaTime, 0f), Space.World);
        if (transform.position.y <= -15 && !obstacleMiss)
        {
            //Destroy(gameObject);
            myAccount.CurMissCount += 1;
            myAccount.CurRythmPoint += missPoint;
            gameObject.SetActive(false);
            obstacleMiss = true;
        }
        else
        {
            obstacleMiss = false;
        }

    }
    public void SetActiveFalseObj()
    {
        gameObject.SetActive(false);
    }

}

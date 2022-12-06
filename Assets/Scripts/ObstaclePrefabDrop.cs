using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePrefabDrop : MonoBehaviour
{
    [Tooltip("節拍掉落速度")]
    public float obstacleDropSpeed;
    [Tooltip("節拍Perfect獲得分數")]
    public int perfectPoint;
    [Tooltip("節拍Good獲得分數")]
    public int goodPoint;
    [Tooltip("節拍Miss扣分")]
    public int missPoint;
    DropMeshRenderCtrl dropMeshRenderCtrl;

    //Renderer rend;

    //public SensorL sensorL;
    //public SensorR sensorR;
    BoxCollider missArea;
    BoxCollider goodArea;
    BoxCollider perfectArea;

    void Start()
    {
        //rend = GetComponent<Renderer>();
        dropMeshRenderCtrl = GetComponent<DropMeshRenderCtrl>();
        missArea = GetComponentsInChildren<BoxCollider>()[2];
        goodArea = GetComponentsInChildren<BoxCollider>()[1];
        perfectArea = GetComponentsInChildren<BoxCollider>()[0];


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
        if (transform.position.y <= -15)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
        //if (dropMeshRenderCtrl.dropMeshRenderOpen)
        //{
        //    Color customColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        //    rend.material.SetColor("_Color", customColor);
        //}
        //else
        //{
        //    Color customColor = new Color(0f, 0f, 0f, 0f);
        //    rend.material.SetColor("_Color", customColor);
        //}
    }
    public void SetActiveFalseObj()
    {
        gameObject.SetActive(false);
    }
    
}

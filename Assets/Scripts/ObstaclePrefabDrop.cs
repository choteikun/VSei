using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePrefabDrop : MonoBehaviour
{
    public float obstacleDropSpeed;

    //public SensorL sensorL;
    //public SensorR sensorR;

    void Start()
    {
        //sensorL = GameObject.FindGameObjectWithTag("SensorL").GetComponent<SensorL>();
        //sensorR = GameObject.FindGameObjectWithTag("SensorR").GetComponent<SensorR>();

        //var euler = transform.eulerAngles;
        //euler.z = Random.Range(0.0f, 360.0f);
        //transform.eulerAngles = euler;
    }
    void Update()
    {
        transform.Translate(new Vector3(0f, (0f - obstacleDropSpeed) * Time.deltaTime, 0f), Space.World);
        if (transform.position.y <= -10)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}

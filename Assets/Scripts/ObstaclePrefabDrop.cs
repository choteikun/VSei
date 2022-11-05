using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePrefabDrop : MonoBehaviour
{
    public float obstacleDropSpeed;
    //public float iconSpeenSpeed;

    void Start()
    {
        var euler = transform.eulerAngles;
        euler.z = Random.Range(0.0f, 360.0f);
        transform.eulerAngles = euler;
    }
    void Update()
    {
        transform.Translate(new Vector3(0f, (0f - obstacleDropSpeed) * Time.deltaTime, 0f), Space.World);
        if (transform.position.y <= -10)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Destroy(gameObject);
        }
    }
}

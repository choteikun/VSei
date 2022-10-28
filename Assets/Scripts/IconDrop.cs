using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconDrop : MonoBehaviour
{
    public float iconMoveSpeed;
    //public float iconSpeenSpeed;

    void Start()
    {
        var euler = transform.eulerAngles;
        euler.z = Random.Range(0.0f, 360.0f);
        transform.eulerAngles = euler;
    }
    void Update()
    {
        transform.Translate(new Vector3(0f, (0f - iconMoveSpeed) * Time.deltaTime, 0f), Space.World);
        if (transform.position.y <= -Screen.height/10)
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour
{
    public float BGScrollSpeed;

    Vector3 StartPos;
    void Start()
    {
        StartPos = transform.position;
    }

    
    void Update()
    {
        transform.Translate(Vector3.down * BGScrollSpeed * Time.deltaTime);
        if (transform.position.y < -19.5f)
        {
            transform.position = StartPos;
        }
    }
}

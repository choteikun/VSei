using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour
{
    public float BGScrollSpeed;

    Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
    }

    
    void Update()
    {
        transform.Translate(Vector3.down * BGScrollSpeed * Time.deltaTime);
        if (transform.GetComponent<RectTransform>().anchoredPosition.y < -85.2f)
        {
            transform.position = startPos;
        }
    }
}

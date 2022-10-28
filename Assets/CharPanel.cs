using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharPanel : MonoBehaviour
{
    void Start()
    {
        transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(850, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

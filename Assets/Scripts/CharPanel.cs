using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharPanel : MonoBehaviour
{
    private CanvasScaler canvasScaler;


    void Start()
    {
        canvasScaler = transform.GetComponentInParent<CanvasScaler>();

        transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(10000, 0);//���̥k�ݮ��A�۰ʹ��
        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(1800, 250);


    }


    
}

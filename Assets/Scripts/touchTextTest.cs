using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class touchTextTest : MonoBehaviour
{
    public TMP_Text tmp_text;

    public SensorLL sensorLL;
    public SensorL sensorL;
    public SensorR sensorR;
    public SensorRR sensorRR;

    void Start()
    {
        sensorLL = GameObject.FindWithTag("SensorLL").GetComponent<SensorLL>();
        sensorL = GameObject.FindWithTag("SensorL").GetComponent<SensorL>();
        sensorR = GameObject.FindWithTag("SensorR").GetComponent<SensorR>();
        sensorRR = GameObject.FindWithTag("SensorRR").GetComponent<SensorRR>();
    }
    void Update()
    {
        tmp_text.text = "TouchCount : " + Input.touchCount.ToString();
    }
}

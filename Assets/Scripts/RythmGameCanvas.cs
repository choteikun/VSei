using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;
using TMPro;

public class RythmGameCanvas : MonoBehaviour
{
    public GameObject perfectEffectPrefab;
    public GameObject goodEffectPrefab;
    public TMP_Text rythmPointText;
    public MyAccount myAccount;

    public CharInfo[] charInfoList;
    [System.Serializable]
    public struct CharInfo
    {
        public MyAccount.CurCharacterUse curCharacterUse;
        public CharactersInfo charsInfo;
    }

    public Dictionary<MyAccount.CurCharacterUse, CharactersInfo> charInfoDictionary = new();//item<�W�r,ItemsInfo>


    public int rythmPoint;
    public int feverTime;
    Canvas canvas;
    
    Vector3 screenPos_sensorLL;
    Vector3 screenPos_sensorL;
    Vector3 screenPos_sensorR;
    Vector3 screenPos_sensorRR;

    RectTransform sensorButtonLL;
    RectTransform sensorButtonL;
    RectTransform sensorButtonR;
    RectTransform sensorButtonRR;

    private Camera cam;
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rythmPointText = GameObject.Find("RythmPointText").GetComponent<TMP_Text>();
        canvas = GetComponent<Canvas>();

        screenPos_sensorLL = cam.WorldToScreenPoint(GameObject.Find("Sensor(LL)").transform.position);
        screenPos_sensorL = cam.WorldToScreenPoint(GameObject.Find("Sensor(L)").transform.position);
        screenPos_sensorR = cam.WorldToScreenPoint(GameObject.Find("Sensor(R)").transform.position);
        screenPos_sensorRR = cam.WorldToScreenPoint(GameObject.Find("Sensor(RR)").transform.position);

        sensorButtonLL = GameObject.Find("SensorButton(LL)").GetComponent<RectTransform>();
        sensorButtonL = GameObject.Find("SensorButton(L)").GetComponent<RectTransform>();
        sensorButtonR = GameObject.Find("SensorButton(R)").GetComponent<RectTransform>();
        sensorButtonRR = GameObject.Find("SensorButton(RR)").GetComponent<RectTransform>();

        float h = Screen.height;
        float w = Screen.width;
        float s = canvas.scaleFactor;

        sensorButtonLL.anchoredPosition = new Vector3((screenPos_sensorLL.x - w / 2) / s, (screenPos_sensorLL.y - h / 2) / s, screenPos_sensorLL.z);
        sensorButtonL.anchoredPosition = new Vector3((screenPos_sensorL.x - w / 2) / s, (screenPos_sensorL.y - h / 2) / s, screenPos_sensorL.z);
        sensorButtonR.anchoredPosition = new Vector3((screenPos_sensorR.x - w / 2) / s, (screenPos_sensorR.y - h / 2) / s, screenPos_sensorR.z);
        sensorButtonRR.anchoredPosition = new Vector3((screenPos_sensorRR.x - w / 2) / s, (screenPos_sensorRR.y - h / 2) / s, screenPos_sensorRR.z);

        InitItemDictionary();


    }
    private void InitItemDictionary()
    {
        charInfoDictionary = new Dictionary<MyAccount.CurCharacterUse, CharactersInfo>();
        for (int i = 0; i < charInfoList.Length; i++)
        {
            //�`�N�G�YcharactersInfoList�X�{�ۦP��key�ഫ��u�|�ɤJ�Ĥ@���X�{���ƾڡA
            //����key�ȵ���bug�åB�S���O�@�A�Фp�ߨϥ�!

            if (!charInfoDictionary.ContainsKey(charInfoList[i].curCharacterUse))//���s�b�o��key����
            {
                charInfoDictionary.Add(charInfoList[i].curCharacterUse, charInfoList[i].charsInfo);
            }
        }
    }
    void Update()
    {
        rythmPointText.text = "Point : " + rythmPoint.ToString();
    }
    public void PerfectEffect()
    {
        PoolManager.Release(perfectEffectPrefab);//�ͦ�PerfectEffect
    }
    public void GoodEffect()
    {
        PoolManager.Release(goodEffectPrefab);//�ͦ�GoodEffect
    }
    
}

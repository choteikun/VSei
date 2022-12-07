using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;
using TMPro;

public class RythmGameCanvas : MonoBehaviour
{
    public SensorLL sensorLL;
    public SensorL sensorL;
    public SensorR sensorR;
    public SensorRR sensorRR;

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


    public int rythmPoint;//����
    public int curSpecialCount;//�����쪺�S��`�禸��
    public int feverNeedPoint;//�ϥγQ�ʧީһݪ����Ʊ���
    public float PerfectPointBounsMulti { get; private set; }//���ƥ[�������v
    public float CurCharMissShield;//��e������Miss������



    Canvas canvas;
    
    Vector3 screenPos_sensorLL;
    Vector3 screenPos_sensorL;
    Vector3 screenPos_sensorR;
    Vector3 screenPos_sensorRR;

    RectTransform sensorButtonLL;
    RectTransform sensorButtonL;
    RectTransform sensorButtonR;
    RectTransform sensorButtonRR;

    float curCharFeverTime;//��e����FeverTime
    int curCharHp;//��e�����q
    float charBounsMulti; //������ƥ[�������v
    int charMissShield;//����K��Miss������
    int charHealHp;//����^��

    private Camera cam;
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rythmPointText = GameObject.Find("RythmPointText").GetComponent<TMP_Text>();
        canvas = GetComponent<Canvas>();

        sensorLL = GameObject.FindGameObjectWithTag("SensorLL").GetComponent<SensorLL>();
        sensorL = GameObject.FindGameObjectWithTag("SensorL").GetComponent<SensorL>();
        sensorR = GameObject.FindGameObjectWithTag("SensorR").GetComponent<SensorR>();
        sensorRR = GameObject.FindGameObjectWithTag("SensorRR").GetComponent<SensorRR>();

        screenPos_sensorLL = cam.WorldToScreenPoint(sensorLL.transform.position);
        screenPos_sensorL = cam.WorldToScreenPoint(sensorL.transform.position);
        screenPos_sensorR = cam.WorldToScreenPoint(sensorR.transform.position);
        screenPos_sensorRR = cam.WorldToScreenPoint(sensorRR.transform.position);

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
        ReadCharacterSkillInfo();

        curCharFeverTime = 0;//���J��e����feverTime
        curCharHp = charInfoDictionary[myAccount.curCharacterUse].charHp;//���J��e�����q
        CurCharMissShield = charMissShield;//���J��e����Miss�ޭ�
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

        if (curSpecialCount >= feverNeedPoint)//�S��`�祴�����ƹF��fever�һݦ��ƥH�W��
        {
            curCharFeverTime = charInfoDictionary[myAccount.curCharacterUse].charFeverTime;
            curCharHp += charHealHp;//�^��
            curSpecialCount = 0;
        }
        if (curCharFeverTime > 0)
        {
            curCharFeverTime -= Time.deltaTime;
            PerfectPointBounsMulti = charBounsMulti;//����Q�ʤ��ƥ[��
            
            if (curCharFeverTime < 0)
            {
                curCharFeverTime = 0;
                PerfectPointBounsMulti = 1.0f;//���ƥ[�����v��_��1.0
            }
        }
        if(curCharHp >= charInfoDictionary[myAccount.curCharacterUse].charHp)//�^�夣�W�L���⥻����q
        {
            curCharHp = charInfoDictionary[myAccount.curCharacterUse].charHp;
        }
    }
    public void PerfectEffect()
    {
        PoolManager.Release(perfectEffectPrefab);//�ͦ�PerfectEffect
    }
    public void GoodEffect()
    {
        PoolManager.Release(goodEffectPrefab);//�ͦ�GoodEffect
    }
    public void ReadCharacterSkillInfo()
    {
        if (charInfoDictionary[myAccount.curCharacterUse].charName == "�Ẹ���ۡE������")//���ƥ[�����ޯ�
        {
            switch (charInfoDictionary[myAccount.curCharacterUse].charLevel)
            {
                case 1:
                    charBounsMulti = 1.2f;
                    break;
                case 2:
                    charBounsMulti = 1.4f;
                    break;
                case 3:
                    charBounsMulti = 1.6f;
                    break;
                case 4:
                    charBounsMulti = 1.8f;
                    break;
                case 5:
                    charBounsMulti = 2.0f;
                    break;
                default:
                    break;
            }
        }
        else { charBounsMulti = 1.0f; }

        if (charInfoDictionary[myAccount.curCharacterUse].charName == "�L�W")//�K��Miss������
        {
            switch (charInfoDictionary[myAccount.curCharacterUse].charLevel)
            {
                case 1:
                    charMissShield = 10;
                    break;
                case 2:
                    charMissShield = 12;
                    break;
                case 3:
                    charMissShield = 14;
                    break;
                case 4:
                    charMissShield = 16;
                    break;
                case 5:
                    charMissShield = 18;
                    break;
                default:
                    break;
            }
        }
        else { charMissShield = 0; }

        if (charInfoDictionary[myAccount.curCharacterUse].charName == "��������E�ڭ�")//�^�_��q���Q�ʧޯ�
        {
            switch (charInfoDictionary[myAccount.curCharacterUse].charLevel)
            {
                case 1:
                    charHealHp = 120;
                    break;
                case 2:
                    charHealHp = 140;
                    break;
                case 3:
                    charHealHp = 160;
                    break;
                case 4:
                    charHealHp = 180;
                    break;
                case 5:
                    charHealHp = 200;
                    break;
                default:
                    break;
            }
        }
        else { charHealHp = 0; }
    }
}

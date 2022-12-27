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
    public GameObject missEffectPrefab;
    public TMP_Text rythmPointText;
    public MyAccount myAccount;

    public CharInfo[] charInfoList;
    [System.Serializable]
    public struct CharInfo
    {
        public MyAccount.CurCharacterUse curCharacterUse;
        public CharactersInfo charsInfo;
    }

    public Dictionary<MyAccount.CurCharacterUse, CharactersInfo> charInfoDictionary = new();//item<名字,ItemsInfo>


    public int rythmPoint;//分數
    public int curSpecialCount;//打擊到的特殊節拍次數
    public int feverNeedPoint;//使用被動技所需的次數條件
    public float PerfectPointBounsMulti { get; private set; }//分數加成的倍率
    public float ItemPointBounsMulti;
    public int curCharHp { get; private set; }//當前角色血量
    public float CurCharMissShield;//當前角色抵抗Miss的次數



    Canvas canvas;
    
    Vector3 screenPos_sensorLL;
    Vector3 screenPos_sensorL;
    Vector3 screenPos_sensorR;
    Vector3 screenPos_sensorRR;

    RectTransform sensorButtonLL;
    RectTransform sensorButtonL;
    RectTransform sensorButtonR;
    RectTransform sensorButtonRR;

    float curCharFeverTime;//當前角色FeverTime  
    float charBounsMulti; //角色分數加成的倍率
    int charMissShield;//角色免疫Miss的次數
    int charHealHp;//角色回血

    private Camera cam;
    void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rythmPointText = GameObject.Find("RythmPointText").GetComponent<TMP_Text>();
        canvas = GetComponent<Canvas>();

        sensorLL = GameObject.Find("Sensor(LL)").GetComponent<SensorLL>();
        sensorL = GameObject.Find("Sensor(L)").GetComponent<SensorL>();
        sensorR = GameObject.Find("Sensor(R)").GetComponent<SensorR>();
        sensorRR = GameObject.Find("Sensor(RR)").GetComponent<SensorRR>();

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

        InitCharInfoDictionary();
        ReadCharacterSkillInfo();

        curCharFeverTime = 0;//載入當前角色feverTime
        curCharHp = charInfoDictionary[myAccount.curCharacterUse].charHp;//載入當前角色血量
        CurCharMissShield = charMissShield;//載入當前角色Miss盾值
        PerfectPointBounsMulti = 1.0f;//perfect倍率初始值

        if (myAccount.HpAddItemUsing)//血量加成道具使用
        {
            curCharHp += 300;
        }

        if (myAccount.PointBounsItemUsing)//分數加成道具使用
        {
            ItemPointBounsMulti = 1.2f;
        }
        else
        {
            ItemPointBounsMulti = 1.0f;
        }
    }
    private void InitCharInfoDictionary()
    {
        charInfoDictionary = new Dictionary<MyAccount.CurCharacterUse, CharactersInfo>();
        for (int i = 0; i < charInfoList.Length; i++)
        {
            //注意：若charactersInfoList出現相同的key轉換後只會導入第一次出現的數據，
            //重複key值視為bug並且沒有保護，請小心使用!

            if (!charInfoDictionary.ContainsKey(charInfoList[i].curCharacterUse))//不存在這個key的話
            {
                charInfoDictionary.Add(charInfoList[i].curCharacterUse, charInfoList[i].charsInfo);
            }
        }
    }
    void Update()
    {
        if (rythmPoint <= 0)
        {
            rythmPoint = 0;
        }
        rythmPointText.text = "Point : " + rythmPoint.ToString();
        myAccount.CurRythmPoint = rythmPoint;

        if (curSpecialCount >= feverNeedPoint)//特殊節拍打擊次數達到fever所需次數以上時
        {
            curCharFeverTime = charInfoDictionary[myAccount.curCharacterUse].charFeverTime;
            curCharHp += charHealHp;//回血
            curSpecialCount = 0;
        }
        if (curCharFeverTime > 0)
        {
            curCharFeverTime -= Time.deltaTime;
            PerfectPointBounsMulti = charBounsMulti * ItemPointBounsMulti;//角色被動分數加乘
            
            if (curCharFeverTime <= 0)
            {
                curCharFeverTime = 0;
                PerfectPointBounsMulti = 1.0f * ItemPointBounsMulti;//分數加乘倍率恢復為1.0
            }
        }
        if(curCharHp >= charInfoDictionary[myAccount.curCharacterUse].charHp)//回血不超過角色本身血量
        {
            curCharHp = charInfoDictionary[myAccount.curCharacterUse].charHp;
        }
    }
    public void PerfectEffect()
    {
        PoolManager.Release(perfectEffectPrefab);//生成PerfectEffect
    }
    public void GoodEffect()
    {
        PoolManager.Release(goodEffectPrefab);//生成GoodEffect
    }
    public void MissEffect()
    {
        PoolManager.Release(missEffectPrefab);//生成MissEffect
    }
    public void ReadCharacterSkillInfo()
    {
        if (charInfoDictionary[myAccount.curCharacterUse].charName == "菲爾貝倫‧阿莉絲")//分數加成的技能
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

        if (charInfoDictionary[myAccount.curCharacterUse].charName == "無名")//免疫Miss的次數
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

        if (charInfoDictionary[myAccount.curCharacterUse].charName == "瑪莉貝塔‧蘿倫")//回復血量的被動技能
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

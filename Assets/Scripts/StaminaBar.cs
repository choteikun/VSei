using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StaminaBar : MonoBehaviour
{
    [Tooltip("每幾分鐘恢復1點體力值")]
    public float restoreStaminaMinute = 5;
    public MyAccount myAccount;
    public Slider staminaSlider;
    public TMP_Text staminaText;
    public Button staminaRecoveryButton;

    public float timer;
    int staminaMax = 10;//體力最大值
    float staminaSecRequired;//回滿體的所需秒數
    float staminaMinuteRequired;//回滿體的所需分鐘
    float secToMinuteCount;//回滿體計算用


    // Start is called before the first frame update
    void Start()
    {
        staminaText = GameObject.Find("StaminaRequiredText (TMP)").GetComponent<TMP_Text>();
        staminaRecoveryButton = GameObject.Find("StaminaRecovery(Button)").GetComponent<Button>();
        staminaSecRequired = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (myAccount.stamina <= staminaMax && myAccount.stamina > 0)//現有體力值小於最大體力值且大於0
        {
            staminaMinuteRequired = restoreStaminaMinute * (staminaMax - myAccount.stamina) - secToMinuteCount;
            staminaSecRequired -= Time.deltaTime;
            if (staminaSecRequired <= 0)//每60秒跳一次計時
            {
                secToMinuteCount += 1;
                if (secToMinuteCount >= restoreStaminaMinute)//回滿體計算用
                {
                    secToMinuteCount = 0;
                }
                staminaSecRequired = 60;
            }
        }
        else//體力值超過最大體力值或為0
        {
            staminaMinuteRequired = 0;
            staminaSecRequired = 0;
        }

        staminaText.text = (int)staminaMinuteRequired + " : " + (int)staminaSecRequired;
        //if (staminaMinuteRequired <= 0)
        //{
        //    staminaMinuteRequired = 0;
        //    StaminaText.text = myAccount.stamina + "/" + staminaMax;
        //}
        //else
        //{
        //    StaminaText.text = myAccount.stamina + "/" + staminaMax;/*+ "剩餘" + (int)staminaMinuteRequired + " : " + (int)staminaSecRequired + "回滿體力";*/
        //}
        
        staminaSlider.value = (float)myAccount.stamina / staminaMax;

        timer += Time.deltaTime;
        if (timer >= 60 * restoreStaminaMinute && myAccount.stamina < staminaMax)//體力值不超過10點的情況下每5分鐘恢復1點體力
        {
            myAccount.stamina += 1;
            timer = 0;
        }

        
        

        if (myAccount.stamina < 0)
        {
            myAccount.stamina = 0;//體力值不會小於0
        }


        if((myAccount.stamina > staminaMax) || (myAccount.MyToken < 150))//如果體力值超出或代幣不足，將補充按鈕disable
        {
            staminaRecoveryButton.interactable = false;
        }
        else
        {
            staminaRecoveryButton.interactable = true;
        }

    }
    public void StaminaRecovery()//補充體力
    {
        if((myAccount.stamina < staminaMax) && (myAccount.MyToken >= 150))
        {
            myAccount.MyToken -= 150;
            myAccount.stamina += 1;
        }
    }
    
}

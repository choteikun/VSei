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
    public TMP_Text StaminaText;

    public float timer;
    int staminaMax = 10;//體力最大值
    float staminaSecRequired;//回滿體的所需秒數
    float staminaMinuteRequired;//回滿體的所需分鐘


    // Start is called before the first frame update
    void Start()
    {
        StaminaText = GameObject.Find("StaminaRequiredText (TMP)").GetComponent<TMP_Text>();
        staminaSecRequired = 60;
        staminaMinuteRequired = restoreStaminaMinute * (staminaMax - myAccount.stamina);
    }

    // Update is called once per frame
    void Update()
    {
        staminaMinuteRequired = restoreStaminaMinute * (staminaMax - myAccount.stamina);
        StaminaText.text = (int)staminaMinuteRequired + " : " + (int)staminaSecRequired;
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
        staminaSecRequired -= Time.deltaTime;
        if (timer >= 60 * restoreStaminaMinute && myAccount.stamina < staminaMax)//體力值不超過10點的情況下每5分鐘恢復1點體力
        {
            staminaSecRequired = 60;
            myAccount.stamina += 1;
            timer = 0;
        }
        if (myAccount.stamina <= 0)
        {
            myAccount.stamina = 0;//體力值不會小於0
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

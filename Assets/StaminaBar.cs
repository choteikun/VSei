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
    

    // Start is called before the first frame update
    void Start()
    {
        StaminaText = transform.GetChild(transform.childCount - 1).GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        StaminaText.text = myAccount.stamina.ToString();
        staminaSlider.value = myAccount.stamina / 100;

        timer += Time.deltaTime;
        if (timer >= 60 * restoreStaminaMinute)
        {
            myAccount.stamina += 1;
            timer = 0;
        }
        if (myAccount.stamina >= staminaMax)//如果超過體力最大值
        {
            myAccount.stamina = staminaMax;
        }
        if (myAccount.stamina <= 0)
        {
            myAccount.stamina = 0;//體力值不會小於0
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    public MyAccount myAccount;
    public Button[] button;

    void Start()
    {
        if (myAccount.Shachiku)
        {
            button[0].GetComponent<Button>().interactable = false;
        }
        if (myAccount.FragmentLover)
        {
            button[1].GetComponent<Button>().interactable = false;
        }
        if (myAccount.Salvage)
        {
            button[2].GetComponent<Button>().interactable = false;
        }
    }
    public void BuyFragmentLover()
    {
        if (myAccount.MyToken > 500)
        {
            myAccount.MyToken -= 500;
            myAccount.FragmentLover = true;
            button[0].GetComponent<Button>().interactable = false;
        }
    }
    public void BuyShachiku()
    {
        if (myAccount.MyToken > 500)
        {
            myAccount.MyToken -= 500;
            myAccount.Shachiku = true;
            button[1].GetComponent<Button>().interactable = false;
        }
    }
    public void BuySalvage()
    {
        if (myAccount.MyToken > 500)
        {
            myAccount.MyToken -= 500;
            myAccount.Salvage = true;
            button[2].GetComponent<Button>().interactable = false;
        }  
    }
}

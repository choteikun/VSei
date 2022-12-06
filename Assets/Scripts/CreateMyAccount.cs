using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateMyAccount : MonoBehaviour
{
    public TMP_InputField MyAccountInput;
    public MyAccount myAccount;

    public void CreateAccountButton()
    {
        myAccount.myAccountName = MyAccountInput.text;
        myAccount.firstPlay = false;
        myAccount.tutorialClose = false;
    }
}

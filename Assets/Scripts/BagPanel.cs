using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagPanel : MonoBehaviour
{
    public MyAccount myAccount;
    public List<int> itemList = new();

    public int kindOfItems;

    void Start()
    {
        for(int i=0;i< kindOfItems; i++)
        {

        }
    }
}

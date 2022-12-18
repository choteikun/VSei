using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BagPanel : MonoBehaviour
{
    public MyAccount myAccount;
    public Button ItemUseCheckButton;


    [Serializable]
    public class ItemData
    {

        public string itemName;
        public int itemAmount;
    }

    public Item[] itemList;
    [System.Serializable]
    public struct Item
    {
        public ItemsInfo itemInfo;
        public int itemAmount;
    }

    public Dictionary<ItemsInfo, int> itemDictionary = new();//itemDictionary<Name,item數量>

    

    void Start()
    {
        ItemUseCheckButton = GameObject.Find("ItemUseCheckButton").GetComponent<Button>();

        InitItemDictionary();

        BagRefresh();

        foreach (KeyValuePair<ItemsInfo, int> item in itemDictionary)
        {
            Debug.Log("Key: " + item.Key);
            Debug.Log("Value: " + item.Value);
        }
        for(int i=0;i< 4; i++)
        {
            Debug.Log(transform.GetChild(i).GetChild(0).gameObject.name);
        }


    }
    private void InitItemDictionary()
    {
        itemDictionary = new Dictionary<ItemsInfo, int>();
        for (int i = 0; i < itemList.Length; i++)
        {
            //注意：若InitItemDictionary出現相同的key轉換後只會導入第一次出現的數據，
            //重複key值視為bug並且沒有保護，請小心使用!

            if (!itemDictionary.ContainsKey(itemList[i].itemInfo))//不存在這個key的話
            {
                itemDictionary.Add(itemList[i].itemInfo, itemList[i].itemAmount);
            }
            switch(itemDictionary.Keys.ElementAt(i).itemName)//依道具名稱(key)放入對應道具的數量(value)
            {
                case "能量飲料":
                    itemList[i].itemAmount = myAccount.EnergyDrink;
                    itemDictionary[itemList[i].itemInfo] = myAccount.EnergyDrink;
                    break;
                case "角色碎片":
                    itemList[i].itemAmount = myAccount.CharacterFragment;
                    itemDictionary[itemList[i].itemInfo] = myAccount.CharacterFragment;
                    break;
                case "血量加成道具":
                    itemList[i].itemAmount = myAccount.HpAddItem;
                    itemDictionary[itemList[i].itemInfo] = myAccount.HpAddItem;
                    break;
                case "分數加成道具":
                    itemList[i].itemAmount = myAccount.PointBounsItem;
                    itemDictionary[itemList[i].itemInfo] = myAccount.PointBounsItem;
                    break;
                case "垃圾":
                    itemList[i].itemAmount = myAccount.Trash;
                    itemDictionary[itemList[i].itemInfo] = myAccount.Trash;
                    break;
                default:
                    break;
            }
        }
        //List<int> bagList = new(itemDictionary.Values);
        //bagList.Sort((x, y) => -x.CompareTo(y));//按道具數量降序
    }
    void Update()
    {
        
    }
    public void BagRefresh()
    {
        for (int i = 0; i < itemList.Length; i++)
        {
            switch (itemDictionary.Keys.ElementAt(i).itemName)//刷新道具value值
            {
                case "能量飲料":
                    itemDictionary[itemDictionary.Keys.ElementAt(i)] = myAccount.EnergyDrink;
                    break;
                case "角色碎片":
                    itemDictionary[itemDictionary.Keys.ElementAt(i)] = myAccount.CharacterFragment;
                    break;
                case "血量加成道具":
                    itemDictionary[itemDictionary.Keys.ElementAt(i)] = myAccount.HpAddItem;
                    break;
                case "分數加成道具":
                    itemDictionary[itemDictionary.Keys.ElementAt(i)] = myAccount.PointBounsItem;
                    break;
                case "垃圾":
                    itemDictionary[itemDictionary.Keys.ElementAt(i)] = myAccount.Trash;
                    break;
                default:
                    break;
            }
        }

        //這裡定義了一個變量，這個變量是linq語句返回的類型的
        //對linq語句進行解讀,從字典中獲取項，選中項，根據項中的Value排序
        var temp = from pair in itemDictionary orderby pair.Value descending select pair;

        //將上面的變量轉換回字典
        itemDictionary =
        temp.ToDictionary(pair => pair.Key, pair => pair.Value);

        for (int i = 0; i < itemList.Length; i++)
        {
            switch (itemDictionary.Keys.ElementAt(i).itemName)//依道具名稱(key)給予該道具按鈕功能(button)
            {
                case "能量飲料":
                    transform.GetChild(i).GetChild(0).GetComponent<TMP_Text>().text = myAccount.EnergyDrink.ToString();
                    transform.GetChild(i).GetComponent<Button>().onClick.AddListener(EnergyDrinkBtn);                    
                    break;
                case "角色碎片":
                    transform.GetChild(i).GetChild(0).GetComponent<TMP_Text>().text = myAccount.CharacterFragment.ToString();
                    transform.GetChild(i).GetComponent<Button>().onClick.AddListener(CharacterFragmentBtn);
                    break;
                case "血量加成道具":
                    transform.GetChild(i).GetChild(0).GetComponent<TMP_Text>().text = myAccount.HpAddItem.ToString();
                    transform.GetChild(i).GetComponent<Button>().onClick.AddListener(HpAddItemBtn);
                    break;
                case "分數加成道具":
                    transform.GetChild(i).GetChild(0).GetComponent<TMP_Text>().text = myAccount.PointBounsItem.ToString();
                    transform.GetChild(i).GetComponent<Button>().onClick.AddListener(PointBounsItemBtn);
                    break;
                case "垃圾":
                    transform.GetChild(i).GetChild(0).GetComponent<TMP_Text>().text = myAccount.Trash.ToString();
                    transform.GetChild(i).GetComponent<Button>().onClick.AddListener(TrashBtn);
                    break;
                default:
                    break;
            }
            transform.GetChild(i).GetComponent<Image>().sprite = itemDictionary.Keys.ElementAt(i).itemIcon;//遍歷子物件並按dictionary的圖片順序排進去
        }

        foreach (KeyValuePair<ItemsInfo, int> item in itemDictionary)
        {
            Debug.Log("Key: " + item.Key);
            Debug.Log("Value: " + item.Value);
        }
        //List<KeyValuePair<ItemsInfo, int>> bagList = new(itemDictionary);
        //bagList.Sort(delegate (KeyValuePair<ItemsInfo, int> s1, KeyValuePair<ItemsInfo, int> s2)
        //{
        //    return s2.Value.CompareTo(s1.Value);//按道具數量降序
        //});

        //itemList.Add(myAccount.EnergyDrink);
        //itemList.Add(myAccount.CharacterFragment);
        //itemList.Add(myAccount.PointBounsItem);
        //itemList.Add(myAccount.HpAddItem);
        //itemList.Add(myAccount.Trash);
        //itemList.Sort((x, y) => -x.CompareTo(y));
    }
    void EnergyDrinkBtn()
    {
        ItemUseCheckButton.onClick.RemoveAllListeners();
        ItemUseCheckButton.onClick.AddListener(UseEnergyDrink);
    }
    void UseEnergyDrink()
    {
        if (myAccount.EnergyDrink > 0)
        {
            myAccount.EnergyDrink--;
            //增加體力
        }
        else
        {

            //體力罐已耗盡
        }
        BagRefresh();
    }
    void CharacterFragmentBtn()
    {
        ItemUseCheckButton.onClick.RemoveAllListeners();
        //沒有功能
    }
    void HpAddItemBtn()
    {
        ItemUseCheckButton.onClick.RemoveAllListeners();
        ItemUseCheckButton.onClick.AddListener(UseHpAddItem);
    }
    void UseHpAddItem()
    { 
        if (!myAccount.HpAddItemUsing)
        {
            if(myAccount.HpAddItem > 0)
            {
                myAccount.HpAddItem--;
                myAccount.HpAddItemUsing = true;
                //血量增加
            }
            else
            {
                //血量增加道具已耗盡
            }
        }
        else
        {
            //道具效果還在，不消耗該道具
        }
        BagRefresh();
    }

    void PointBounsItemBtn()
    {
        ItemUseCheckButton.onClick.RemoveAllListeners();
        ItemUseCheckButton.onClick.AddListener(UsePointBounsItem);
    }
    void UsePointBounsItem()
    {
        if (!myAccount.PointBounsItemUsing)
        {
            if (myAccount.PointBounsItem > 0)
            {
                myAccount.PointBounsItem--;
                myAccount.PointBounsItemUsing = true;
                //分數加成
            }
            else
            {
                //分數加成道具已耗盡
            }
        }
        else
        {
            //道具效果還在
        }
        BagRefresh();
    }
    void TrashBtn()
    {
        ItemUseCheckButton.onClick.RemoveAllListeners();
        //沒有功能
    }
}

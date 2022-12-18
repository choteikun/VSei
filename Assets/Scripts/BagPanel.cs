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

    public Dictionary<ItemsInfo, int> itemDictionary = new();//itemDictionary<Name,item�ƶq>

    

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
            //�`�N�G�YInitItemDictionary�X�{�ۦP��key�ഫ��u�|�ɤJ�Ĥ@���X�{���ƾڡA
            //����key�ȵ���bug�åB�S���O�@�A�Фp�ߨϥ�!

            if (!itemDictionary.ContainsKey(itemList[i].itemInfo))//���s�b�o��key����
            {
                itemDictionary.Add(itemList[i].itemInfo, itemList[i].itemAmount);
            }
            switch(itemDictionary.Keys.ElementAt(i).itemName)//�̹D��W��(key)��J�����D�㪺�ƶq(value)
            {
                case "��q����":
                    itemList[i].itemAmount = myAccount.EnergyDrink;
                    itemDictionary[itemList[i].itemInfo] = myAccount.EnergyDrink;
                    break;
                case "����H��":
                    itemList[i].itemAmount = myAccount.CharacterFragment;
                    itemDictionary[itemList[i].itemInfo] = myAccount.CharacterFragment;
                    break;
                case "��q�[���D��":
                    itemList[i].itemAmount = myAccount.HpAddItem;
                    itemDictionary[itemList[i].itemInfo] = myAccount.HpAddItem;
                    break;
                case "���ƥ[���D��":
                    itemList[i].itemAmount = myAccount.PointBounsItem;
                    itemDictionary[itemList[i].itemInfo] = myAccount.PointBounsItem;
                    break;
                case "�U��":
                    itemList[i].itemAmount = myAccount.Trash;
                    itemDictionary[itemList[i].itemInfo] = myAccount.Trash;
                    break;
                default:
                    break;
            }
        }
        //List<int> bagList = new(itemDictionary.Values);
        //bagList.Sort((x, y) => -x.CompareTo(y));//���D��ƶq����
    }
    void Update()
    {
        
    }
    public void BagRefresh()
    {
        for (int i = 0; i < itemList.Length; i++)
        {
            switch (itemDictionary.Keys.ElementAt(i).itemName)//��s�D��value��
            {
                case "��q����":
                    itemDictionary[itemDictionary.Keys.ElementAt(i)] = myAccount.EnergyDrink;
                    break;
                case "����H��":
                    itemDictionary[itemDictionary.Keys.ElementAt(i)] = myAccount.CharacterFragment;
                    break;
                case "��q�[���D��":
                    itemDictionary[itemDictionary.Keys.ElementAt(i)] = myAccount.HpAddItem;
                    break;
                case "���ƥ[���D��":
                    itemDictionary[itemDictionary.Keys.ElementAt(i)] = myAccount.PointBounsItem;
                    break;
                case "�U��":
                    itemDictionary[itemDictionary.Keys.ElementAt(i)] = myAccount.Trash;
                    break;
                default:
                    break;
            }
        }

        //�o�̩w�q�F�@���ܶq�A�o���ܶq�Olinq�y�y��^��������
        //��linq�y�y�i���Ū,�q�r�夤������A�襤���A�ھڶ�����Value�Ƨ�
        var temp = from pair in itemDictionary orderby pair.Value descending select pair;

        //�N�W�����ܶq�ഫ�^�r��
        itemDictionary =
        temp.ToDictionary(pair => pair.Key, pair => pair.Value);

        for (int i = 0; i < itemList.Length; i++)
        {
            switch (itemDictionary.Keys.ElementAt(i).itemName)//�̹D��W��(key)�����ӹD����s�\��(button)
            {
                case "��q����":
                    transform.GetChild(i).GetChild(0).GetComponent<TMP_Text>().text = myAccount.EnergyDrink.ToString();
                    transform.GetChild(i).GetComponent<Button>().onClick.AddListener(EnergyDrinkBtn);                    
                    break;
                case "����H��":
                    transform.GetChild(i).GetChild(0).GetComponent<TMP_Text>().text = myAccount.CharacterFragment.ToString();
                    transform.GetChild(i).GetComponent<Button>().onClick.AddListener(CharacterFragmentBtn);
                    break;
                case "��q�[���D��":
                    transform.GetChild(i).GetChild(0).GetComponent<TMP_Text>().text = myAccount.HpAddItem.ToString();
                    transform.GetChild(i).GetComponent<Button>().onClick.AddListener(HpAddItemBtn);
                    break;
                case "���ƥ[���D��":
                    transform.GetChild(i).GetChild(0).GetComponent<TMP_Text>().text = myAccount.PointBounsItem.ToString();
                    transform.GetChild(i).GetComponent<Button>().onClick.AddListener(PointBounsItemBtn);
                    break;
                case "�U��":
                    transform.GetChild(i).GetChild(0).GetComponent<TMP_Text>().text = myAccount.Trash.ToString();
                    transform.GetChild(i).GetComponent<Button>().onClick.AddListener(TrashBtn);
                    break;
                default:
                    break;
            }
            transform.GetChild(i).GetComponent<Image>().sprite = itemDictionary.Keys.ElementAt(i).itemIcon;//�M���l����ë�dictionary���Ϥ����Ǳƶi�h
        }

        foreach (KeyValuePair<ItemsInfo, int> item in itemDictionary)
        {
            Debug.Log("Key: " + item.Key);
            Debug.Log("Value: " + item.Value);
        }
        //List<KeyValuePair<ItemsInfo, int>> bagList = new(itemDictionary);
        //bagList.Sort(delegate (KeyValuePair<ItemsInfo, int> s1, KeyValuePair<ItemsInfo, int> s2)
        //{
        //    return s2.Value.CompareTo(s1.Value);//���D��ƶq����
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
            //�W�[��O
        }
        else
        {

            //��O���w�Ӻ�
        }
        BagRefresh();
    }
    void CharacterFragmentBtn()
    {
        ItemUseCheckButton.onClick.RemoveAllListeners();
        //�S���\��
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
                //��q�W�[
            }
            else
            {
                //��q�W�[�D��w�Ӻ�
            }
        }
        else
        {
            //�D��ĪG�٦b�A�����ӸӹD��
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
                //���ƥ[��
            }
            else
            {
                //���ƥ[���D��w�Ӻ�
            }
        }
        else
        {
            //�D��ĪG�٦b
        }
        BagRefresh();
    }
    void TrashBtn()
    {
        ItemUseCheckButton.onClick.RemoveAllListeners();
        //�S���\��
    }
}

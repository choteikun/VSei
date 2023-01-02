using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq.Expressions;
using TMPro;


public class GashaponPanel : MonoBehaviour
{
    //public CanvasGroup gashaCanvasGroup;
    //public CanvasGroup[] canvasChildGroups;//抽卡介面子物件的CanvasGroup
    
    public MyAccount myAccount;
    public TMP_Text playerTokenText;
    [Tooltip("單抽價格")]
    public int OneGashaTokens = 300;
    [Tooltip("五抽價格")]
    public int TenGashaTokens = 1400;
    public List<CharactersInfo> charactersInfo = new();

    public List<Sprite> gashaResultSprite = new();
    public List<Sprite> allSprite = new();
    //int gashaResultIndex;

    float totalCharProbability;
    Animation gashaPanelAnim;

    public Item[] itemList;
    [System.Serializable]
    public struct Item
    {
        public string ItemID;
        public ItemsInfo itemsInfo;
    }

    public Dictionary<string, ItemsInfo> itemDictionary = new();//item<名字,ItemsInfo>


    void Start()
    {
        InitItemDictionary();
        playerTokenText = GameObject.Find("MyToken(Gasha_TMP)").GetComponent<TMP_Text>();
        gashaPanelAnim = GetComponent<Animation>();
        //gashaCanvasGroup = GetComponent<GashaponPanel>().gameObject.GetComponent<CanvasGroup>();

        //if (canvasChildGroups.Length >= 4)
        //{
        //    canvasChildGroups[3] = transform.GetChild(transform.childCount - 4).gameObject.GetComponent<CanvasGroup>();
        //    canvasChildGroups[2] = transform.GetChild(transform.childCount - 3).gameObject.GetComponent<CanvasGroup>();
        //    canvasChildGroups[1] = transform.GetChild(transform.childCount - 2).gameObject.GetComponent<CanvasGroup>();
        //    canvasChildGroups[0] = transform.GetChild(transform.childCount - 1).gameObject.GetComponent<CanvasGroup>();
        //}
        //else
        //{
        //    throw new ArgumentNullException("請輸入正確的索引值(點擊此訊息查看上面的數組長度並在inspector中輸入比他大的數值)，讓UI功能正常運行");
        //}

        for (int i = 0; i < charactersInfo.Count; i++) 
        {
            //Debug.Log(charactersInfo[i].charName);
            totalCharProbability += charactersInfo[i].gashaProbability;//角色總機率
        }
    }
    private void InitItemDictionary()
    {
        itemDictionary = new Dictionary<string, ItemsInfo>();
        for (int i = 0; i < itemList.Length; i++)
        {
            //注意：若charProfilesList出現相同的key轉換後只會導入第一次出現的數據，
            //重複key值視為bug並且沒有保護，請小心使用!

            if (!itemDictionary.ContainsKey(itemList[i].ItemID))//不存在這個key的話
            {
                itemDictionary.Add(itemList[i].ItemID, itemList[i].itemsInfo);
            }
        }
    }

    void Update()
    {
        playerTokenText.text = myAccount.MyToken.ToString();
    }
    public bool Probability(float percent)//比較大小確率計算
    {
        float probabilityRate = UnityEngine.Random.value * 100.0f;
        if (percent == 100.0f && probabilityRate == percent)
        {
            return true;
        }
        else if (probabilityRate < percent)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Gasha()
    { 
        if (Probability(totalCharProbability))//如果抽中角色
        {
            string gashaCharacter;
            gashaCharacter = charactersInfo[UnityEngine.Random.Range(0, charactersInfo.Count)].name;//隨機選一隻角色抽出
            Debug.Log(gashaCharacter);
            //if (gashaResultSprite[0]!=null) { gashaResultSprite.Remove(gashaResultSprite[0]); }
            if (gashaCharacter == GetMemberName(() => myAccount.AikaAmimi) && !myAccount.AikaAmimi) //如果抽出角色是艾卡‧阿米米且沒有該角色
            {
                myAccount.AikaAmimi = true;
                gashaResultSprite.Add(allSprite[1]);
            }
            else if (gashaCharacter == GetMemberName(() => myAccount.FelbelemAlice) && !myAccount.FelbelemAlice)//如果抽出角色是菲爾貝倫‧阿莉絲且沒有該角色
            {
                myAccount.FelbelemAlice = true;
                gashaResultSprite.Add(allSprite[0]);
            }
            else if (gashaCharacter == GetMemberName(() => myAccount.MalibetaRorem) && !myAccount.MalibetaRorem)//如果抽出角色是瑪莉貝塔‧蘿倫且沒有該角色
            {
                myAccount.MalibetaRorem = true;
                gashaResultSprite.Add(allSprite[2]);
            }
            else if (gashaCharacter == GetMemberName(() => myAccount.Nameless) && !myAccount.Nameless)//如果抽出角色是無名且沒有該角色
            {
                myAccount.Nameless = true;
                gashaResultSprite.Add(allSprite[3]);
            }
            else if (gashaCharacter == GetMemberName(() => myAccount.ShiorhaiYai) && !myAccount.ShiorhaiYai)//如果抽出角色是白灰.亞衣且沒有該角色
            {
                myAccount.ShiorhaiYai = true;
                gashaResultSprite.Add(allSprite[4]);
            }
            else if ((gashaCharacter == GetMemberName(() => myAccount.AikaAmimi)) || (gashaCharacter == GetMemberName(() => myAccount.FelbelemAlice)) || (gashaCharacter == GetMemberName(() => myAccount.MalibetaRorem)) || (gashaCharacter == GetMemberName(() => myAccount.Nameless)) || (gashaCharacter == GetMemberName(() => myAccount.ShiorhaiYai)))
            {
                //抽到重覆角色時
                Debug.Log("再接再勵");
                myAccount.CharacterFragment += 5;
                gashaResultSprite.Add(allSprite[5]);
            }
            else
            {
                throw new ArgumentNullException("有未添加的角色被抽出，請在上方判斷式裡添加");
            }
        }
        else if (Probability(itemDictionary["角色碎片"].gashaProbability * 100 / (itemDictionary["角色碎片"].gashaProbability + itemDictionary["能量飲料"].gashaProbability + itemDictionary["血量加成"].gashaProbability + itemDictionary["分數加成"].gashaProbability + itemDictionary["垃圾"].gashaProbability))) //角色碎片機率
        {
            myAccount.CharacterFragment += 5;
            gashaResultSprite.Add(allSprite[5]);
        }
        else if (Probability(itemDictionary["能量飲料"].gashaProbability * 100 / (itemDictionary["能量飲料"].gashaProbability + itemDictionary["血量加成"].gashaProbability + itemDictionary["分數加成"].gashaProbability + itemDictionary["垃圾"].gashaProbability)))//能量飲料機率
        {
            myAccount.EnergyDrink += 1;
            gashaResultSprite.Add(allSprite[6]);
        }
        else if (Probability(itemDictionary["血量加成"].gashaProbability * 100 / (itemDictionary["血量加成"].gashaProbability + itemDictionary["分數加成"].gashaProbability + itemDictionary["垃圾"].gashaProbability)))//分數加成道具機率
        {
            myAccount.PointBounsItem += 1;
            gashaResultSprite.Add(allSprite[7]);
        }
        else if (Probability(itemDictionary["分數加成"].gashaProbability * 100 / (itemDictionary["分數加成"].gashaProbability + itemDictionary["垃圾"].gashaProbability)))//血量加成道具機率
        {
            myAccount.HpAddItem += 1;
            gashaResultSprite.Add(allSprite[8]);
        }
        else if (Probability(itemDictionary["垃圾"].gashaProbability * 100 / (itemDictionary["垃圾"].gashaProbability)))//垃圾機率
        {
            myAccount.Trash += 1;
            gashaResultSprite.Add(allSprite[9]);
        }
        else
        {
            Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");//驗證用(機率加總100%所以他不應該出現)
        }
    }
    public void OneGasha()
    {
        if (myAccount.MyToken >= OneGashaTokens)
        {
            myAccount.MyToken -= OneGashaTokens;
            Gasha();
            GameObject.Find("OneGachaCharIcon").transform.GetChild(0).GetComponent<Image>().sprite = gashaResultSprite[0];
            if (gashaResultSprite.Count == 2)
            {
                gashaResultSprite.Remove(gashaResultSprite[0]);
            }
        }
    }
    public void TenGasha()
    {
        if (myAccount.MyToken >= TenGashaTokens) 
        {
            myAccount.MyToken -= TenGashaTokens;
            for (int i = 0; i < 5; i++)
            {
                Gasha();
                GameObject.Find("TenGachaCharIcon").transform.GetChild(i).GetComponent<Image>().sprite = gashaResultSprite[i];
                if (gashaResultSprite.Count > 5)
                {
                    gashaResultSprite.Remove(gashaResultSprite[i]);
                }
            }
        }
    }
    public void OneGashaPanelFadeIn()//過場
    {
        gashaPanelAnim.Play("OneGachaResult");
    }
    public void TenGashaPanelFadeIn()//過場
    {
        gashaPanelAnim.Play("TenGachaResult");
    }
    public void Test()
    {
        Debug.Log("test");
    }
    public string GetMemberName<T>(Expression<Func<T>> memberExpression)//獲取變量名字(不是值)
    {
        MemberExpression expressionBody = (MemberExpression)memberExpression.Body;
        return expressionBody.Member.Name;
    }


}

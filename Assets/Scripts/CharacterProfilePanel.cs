using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class CharacterProfilePanel : MonoBehaviour
{
    public MyAccount myAccount;
    public CharactersInfo[] charactersInfos;
    public BagPanel bagPanel;

    public CharProfile[] charProfilesList;
    
    TMP_Text levelText;
    TMP_Text fragmentsText;

    int curMaxFragments;//當前面板顯示最大角色碎片持有數(右)
    int curFragments;//當前面板顯示角色碎片持有數(左)
    int curCharacterListID;

    [System.Serializable]
    public struct CharProfile
    {
        public string characterID;
        public Image characterImage;
    }

    public Dictionary<string, Image> charProfilesDictionary = new();//角色<名字,image>


    void Start()
    {
        InitCharProfilesDictionary();
        bagPanel = GameObject.Find("BagPanel").GetComponent<BagPanel>();
        levelText = GameObject.Find("Lv(TMP)").GetComponent<TMP_Text>();
        fragmentsText = GameObject.Find("Fragments(TMP)").GetComponent<TMP_Text>();
        curCharacterListID = 0;
    }


    private void InitCharProfilesDictionary()
    {
        charProfilesDictionary = new Dictionary<string, Image>();
        for (int i = 0; i < charProfilesList.Length; i++)
        {
            //注意：若charProfilesList出現相同的key轉換後只會導入第一次出現的數據，
            //重複key值視為bug並且沒有保護，請小心使用!

            if (!charProfilesDictionary.ContainsKey(charProfilesList[i].characterID))//不存在這個key的話
            {
                charProfilesDictionary.Add(charProfilesList[i].characterID, charProfilesList[i].characterImage);

            }
        }
    }
    void Update()
    {
        switch (charactersInfos[curCharacterListID].charLevel)
        {
            case 1:
                curMaxFragments = 25;
                if (myAccount.CharacterFragment > 25)
                {
                    curFragments = 25;
                }
                else { curFragments = myAccount.CharacterFragment; }

                break;
            case 2:
                curMaxFragments = 50;
                if (myAccount.CharacterFragment > 50)
                {
                    curFragments = 50;
                }
                else { curFragments = myAccount.CharacterFragment; }

                break;
            case 3:
                curMaxFragments = 125;
                if (myAccount.CharacterFragment > 125)
                {
                    curFragments = 125;
                }
                else { curFragments = myAccount.CharacterFragment; }

                break;
            case 4:
                curMaxFragments = 250;
                if (myAccount.CharacterFragment > 250)
                {
                    curFragments = 250;
                }
                else { curFragments = myAccount.CharacterFragment; }

                break;
            default:
                break;
        }
        fragmentsText.text = curFragments + "/" + curMaxFragments;
        levelText.text = "LV" + charactersInfos[curCharacterListID].charLevel.ToString();
    }

    public void AikaAmimiProfile()
    {
        curCharacterListID = 1;
        AllProfileClose();
        CharProfilesImageOpen("艾卡‧阿米米");
    }
    public void FelbelemAliceProfile()
    {
        curCharacterListID = 0;
        AllProfileClose();
        CharProfilesImageOpen("菲爾貝倫‧阿莉絲");      
    }
    public void MalibetaRoremProfile()
    {
        curCharacterListID = 2;
        AllProfileClose();
        CharProfilesImageOpen("瑪莉貝塔‧蘿倫");
    }
    public void NamelessProfile()
    {
        curCharacterListID = 3;
        AllProfileClose();
        CharProfilesImageOpen("無名");
    }
    public void ShiorhaiYaiProfile()
    {
        curCharacterListID = 4;
        AllProfileClose();
        CharProfilesImageOpen("白灰.亞衣");
    }
    public void AllProfileClose()
    {
        for(int i = 0; i < charProfilesList.Length; i++)
        {
            charProfilesList[i].characterImage.GetComponent<CanvasGroup>().alpha = 0;
            charProfilesList[i].characterImage.GetComponent<CanvasGroup>().interactable = false;
            charProfilesList[i].characterImage.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }
    public void CharProfilesImageOpen(string charName)
    {
        charProfilesDictionary[charName].GetComponent<CanvasGroup>().alpha = 1;
        charProfilesDictionary[charName].GetComponent<CanvasGroup>().interactable = true;
        charProfilesDictionary[charName].GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
    public void LvUpButton()
    {
        if (curFragments == curMaxFragments)
        {
            switch (charactersInfos[curCharacterListID].charLevel)
            {
                case 1:
                    myAccount.CharacterFragment -= 25;
                    charactersInfos[curCharacterListID].charLevel = 2;
                    break;
                case 2:
                    myAccount.CharacterFragment -= 50;
                    charactersInfos[curCharacterListID].charLevel = 3;
                    break;
                case 3:
                    myAccount.CharacterFragment -= 125;
                    charactersInfos[curCharacterListID].charLevel = 4;
                    break;
                case 4:
                    myAccount.CharacterFragment -= 250;
                    charactersInfos[curCharacterListID].charLevel = 5;
                    break;
                default:
                    break;
            }
        }
        bagPanel.BagRefresh();
    }
}

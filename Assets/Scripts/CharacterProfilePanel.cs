using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CharacterProfilePanel : MonoBehaviour
{
    public CharProfile[] charProfilesList;

    [System.Serializable]
    public struct CharProfile
    {
        public string characterID;
        public Image characterImage;
    }

    public Dictionary<string, Image> charProfilesDictionary = new();//角色<名字,image>


    private void Start()
    {
        InitCharProfilesDictionary();
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

    public void AikaAmimiProfile()
    {
        AllProfileClose();
        CharProfilesImageOpen("艾卡‧阿米米");
    }
    public void FelbelemAliceProfile()
    {
        AllProfileClose();
        CharProfilesImageOpen("菲爾貝倫‧阿莉絲");
    }
    public void MalibetaRoremProfile()
    {
        AllProfileClose();
        CharProfilesImageOpen("瑪莉貝塔‧蘿倫");
    }
    public void NamelessProfile()
    {
        AllProfileClose();
        CharProfilesImageOpen("無名");
    }
    public void ShiorhaiYaiProfile()
    {
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
    
}

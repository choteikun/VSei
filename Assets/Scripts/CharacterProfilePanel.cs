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

    int curMaxFragments;//��e���O��̤ܳj����H��������(�k)
    int curFragments;//��e���O��ܨ���H��������(��)
    int curCharacterListID;

    [System.Serializable]
    public struct CharProfile
    {
        public string characterID;
        public Image characterImage;
    }

    public Dictionary<string, Image> charProfilesDictionary = new();//����<�W�r,image>


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
            //�`�N�G�YcharProfilesList�X�{�ۦP��key�ഫ��u�|�ɤJ�Ĥ@���X�{���ƾڡA
            //����key�ȵ���bug�åB�S���O�@�A�Фp�ߨϥ�!

            if (!charProfilesDictionary.ContainsKey(charProfilesList[i].characterID))//���s�b�o��key����
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
        CharProfilesImageOpen("��d�E���̦�");
    }
    public void FelbelemAliceProfile()
    {
        curCharacterListID = 0;
        AllProfileClose();
        CharProfilesImageOpen("�Ẹ���ۡE������");      
    }
    public void MalibetaRoremProfile()
    {
        curCharacterListID = 2;
        AllProfileClose();
        CharProfilesImageOpen("��������E�ڭ�");
    }
    public void NamelessProfile()
    {
        curCharacterListID = 3;
        AllProfileClose();
        CharProfilesImageOpen("�L�W");
    }
    public void ShiorhaiYaiProfile()
    {
        curCharacterListID = 4;
        AllProfileClose();
        CharProfilesImageOpen("�զ�.�Ȧ�");
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

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

    public Dictionary<string, Image> charProfilesDictionary = new();//����<�W�r,image>


    private void Start()
    {
        InitCharProfilesDictionary();
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

    public void AikaAmimiProfile()
    {
        AllProfileClose();
        CharProfilesImageOpen("��d�E���̦�");
    }
    public void FelbelemAliceProfile()
    {
        AllProfileClose();
        CharProfilesImageOpen("�Ẹ���ۡE������");
    }
    public void MalibetaRoremProfile()
    {
        AllProfileClose();
        CharProfilesImageOpen("��������E�ڭ�");
    }
    public void NamelessProfile()
    {
        AllProfileClose();
        CharProfilesImageOpen("�L�W");
    }
    public void ShiorhaiYaiProfile()
    {
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
    
}

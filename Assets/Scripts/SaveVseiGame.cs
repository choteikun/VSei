using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveVseiGame : MonoBehaviour
{
    public static SaveVseiGame instance;
    public bool isPlayingWithDefaultData;

    public MyAccount myAccount;
    public SongsInfo songsInfo;
    public CharInfo[] charInfoList;
    public ItemInfo[] itemInfoList;

    [System.Serializable]
    public struct CharInfo
    {
        public string charName;
        public CharactersInfo charsInfo;
    }
    public Dictionary<string, CharactersInfo> charInfoDictionary = new();//Character<�W�r,charsInfo>

    
    [System.Serializable]
    public struct ItemInfo
    {
        public string itemName;
        public ItemsInfo itemsInfo;
    }
    public Dictionary<string, ItemsInfo> itemInfoDictionary = new();//item<�W�r,ItemsInfo>

    string saveMyAccountToJson;

    string saveFelbelemAliceInfoToJson;
    string saveAikaAmimiInfoToJson;
    string saveMalibetaRoremInfoToJson;
    string saveNamelessInfoToJson;
    string saveShiorhaiYaiInfoToJson;

    string saveCharacterFragmentInfoToJson;
    string saveEnergyDrinkInfoToJson;
    string saveHpAddItemInfoToJson;
    string savePointBounsItemInfoToJson;
    string saveTrashInfoToJson;

    string saveSongsInfoToJson;

    string savePathPC;
    string savePathAndroid;
    string savePath;

    private void InitCharInfoDictionary()
    {
        charInfoDictionary = new Dictionary<string, CharactersInfo>();
        for (int i = 0; i < charInfoList.Length; i++)
        {
            //�`�N�G�YcharactersInfoList�X�{�ۦP��key�ഫ��u�|�ɤJ�Ĥ@���X�{���ƾڡA
            //����key�ȵ���bug�åB�S���O�@�A�Фp�ߨϥ�!

            if (!charInfoDictionary.ContainsKey(charInfoList[i].charName))//���s�b�o��key����
            {
                charInfoDictionary.Add(charInfoList[i].charName, charInfoList[i].charsInfo);
            }
        }
    }
    private void InitItemInfoDictionary()
    {
        itemInfoDictionary = new Dictionary<string, ItemsInfo>();
        for (int i = 0; i < charInfoList.Length; i++)
        {
            //�`�N�G�YitemsInfoList�X�{�ۦP��key�ഫ��u�|�ɤJ�Ĥ@���X�{���ƾڡA
            //����key�ȵ���bug�åB�S���O�@�A�Фp�ߨϥ�!

            if (!itemInfoDictionary.ContainsKey(itemInfoList[i].itemName))//���s�b�o��key����
            {
                itemInfoDictionary.Add(itemInfoList[i].itemName, itemInfoList[i].itemsInfo);
            }
        }
    }
   

    void Awake()
    {
        InitCharInfoDictionary();
        InitItemInfoDictionary();
    }

    void Start()
    {
        savePathPC = Application.dataPath + "/StreamingAssets";
        //savePathAndroid = "jar:file://" + Application.persistentDataPath + "!/assets/";
        savePathAndroid = Application.persistentDataPath;
        if (Application.platform == RuntimePlatform.Android)
        {
            savePath = savePathAndroid;
        }
        else
        {
            savePath = savePathPC;
        }
    }

    public void SaveGame()
    {
        saveMyAccountToJson = JsonUtility.ToJson(myAccount);

        saveFelbelemAliceInfoToJson = JsonUtility.ToJson(charInfoDictionary["�Ẹ���ۡE������"]);
        saveAikaAmimiInfoToJson = JsonUtility.ToJson(charInfoDictionary["��d�E���̦�"]);
        saveMalibetaRoremInfoToJson = JsonUtility.ToJson(charInfoDictionary["��������E�ڭ�"]);
        saveNamelessInfoToJson = JsonUtility.ToJson(charInfoDictionary["�L�W"]);
        saveShiorhaiYaiInfoToJson = JsonUtility.ToJson(charInfoDictionary["�զ�.�Ȧ�"]);

        saveCharacterFragmentInfoToJson = JsonUtility.ToJson(itemInfoDictionary["����H��"]);
        saveEnergyDrinkInfoToJson = JsonUtility.ToJson(itemInfoDictionary["��q����"]);
        saveHpAddItemInfoToJson = JsonUtility.ToJson(itemInfoDictionary["��q�[���D��"]);
        savePointBounsItemInfoToJson = JsonUtility.ToJson(itemInfoDictionary["���ƥ[���D��"]);
        saveTrashInfoToJson = JsonUtility.ToJson(itemInfoDictionary["�U��"]);

        saveSongsInfoToJson = JsonUtility.ToJson(songsInfo);


        //if (!Directory.Exists(savePathPC)) { Directory.CreateDirectory(savePathPC); }//PC���|�S��StreamingAssets�h�Ыؤ@��

        File.WriteAllText(savePath + "/saveMyAccount.Json", saveMyAccountToJson);
        //-------------------------------------------------------------------------------------------------�H�W�g�J���a�ӤH�b���T
        File.WriteAllText(savePath + "/saveFelbelemAliceInfo.Json", saveFelbelemAliceInfoToJson);
        File.WriteAllText(savePath + "/saveAikaAmimiInfo.Json", saveAikaAmimiInfoToJson);
        File.WriteAllText(savePath + "/saveMalibetaRoremInfo.Json", saveMalibetaRoremInfoToJson);
        File.WriteAllText(savePath + "/saveNamelessInfo.Json", saveNamelessInfoToJson);
        File.WriteAllText(savePath + "/saveShiorhaiYaiInfo.Json", saveShiorhaiYaiInfoToJson);
        //-------------------------------------------------------------------------------------------------�H�W�g�J����json��
        File.WriteAllText(savePath + "/saveCharacterFragmentInfo.Json", saveCharacterFragmentInfoToJson);
        File.WriteAllText(savePath + "/saveEnergyDrinkInfo.Json", saveEnergyDrinkInfoToJson);
        File.WriteAllText(savePath + "/saveHpAddItemInfo.Json", saveHpAddItemInfoToJson);
        File.WriteAllText(savePath + "/savePointBounsItemInfo.Json", savePointBounsItemInfoToJson);
        File.WriteAllText(savePath + "/saveTrashInfo.Json", saveTrashInfoToJson);
        //-------------------------------------------------------------------------------------------------�H�W�g�JItemjson��
        File.WriteAllText(savePath + "/saveSongsInfo.Json", saveSongsInfoToJson);
        //-------------------------------------------------------------------------------------------------�H�W�g�J�q����Tjson��

        Debug.Log("DataSave");
    }
   public void LoadCheck()
    {
        if(!LoadGame())
        {
            Debug.Log("Get Default Data...");
            isPlayingWithDefaultData = true;
        }
        else
        {
            Debug.Log("Saved File Get!!");
        }
    }
    public bool LoadGame()
    {
        if (File.Exists(savePath + "/saveMyAccount.Json"))
        {
            saveMyAccountToJson = File.ReadAllText(savePath + "/saveMyAccount.Json");
            MyAccount dataSO;
            Debug.Log("Save MyAccount_Data Already");
            dataSO = Resources.Load<MyAccount>("CharacterScriptObjsInfo/MyAccount");
            JsonUtility.FromJsonOverwrite(saveMyAccountToJson, dataSO);
            myAccount = dataSO;
            Debug.Log("FirstPlayer: " + myAccount.firstPlay);
        }
        if (File.Exists(savePath + "/saveFelbelemAliceInfo.Json"))
        {
            saveFelbelemAliceInfoToJson = File.ReadAllText(savePath + "/saveFelbelemAliceInfo.Json");
            CharactersInfo dataSO;
            Debug.Log("Save FelbelemAlice_Data Already");
            dataSO = Resources.Load<CharactersInfo>("CharacterScriptObjsInfo/FelbelemAlice");
            JsonUtility.FromJsonOverwrite(saveFelbelemAliceInfoToJson, dataSO);
            charInfoDictionary["�Ẹ���ۡE������"] = dataSO;
        }
        if (File.Exists(savePath + "/saveAikaAmimiInfo.Json"))
        {
            saveAikaAmimiInfoToJson = File.ReadAllText(savePath + "/saveAikaAmimiInfo.Json");
            CharactersInfo dataSO;
            Debug.Log("Save AikaAmimi_Data Already");
            dataSO = Resources.Load<CharactersInfo>("CharacterScriptObjsInfo/AikaAmimi");
            JsonUtility.FromJsonOverwrite(saveAikaAmimiInfoToJson, dataSO);
            charInfoDictionary["��d�E���̦�"] = dataSO;
        }
        if (File.Exists(savePath + "/saveMalibetaRoremInfo.Json"))
        {
            saveMalibetaRoremInfoToJson = File.ReadAllText(savePath + "/saveMalibetaRoremInfo.Json");
            CharactersInfo dataSO;
            Debug.Log("Save MalibetaRorem_Data Already");
            dataSO = Resources.Load<CharactersInfo>("CharacterScriptObjsInfo/MalibetaRorem");
            JsonUtility.FromJsonOverwrite(saveFelbelemAliceInfoToJson, dataSO);
            charInfoDictionary["��������E�ڭ�"] = dataSO;
        }
        if (File.Exists(savePath + "/saveNamelessInfo.Json"))
        {
            saveNamelessInfoToJson = File.ReadAllText(savePath + "/saveNamelessInfo.Json");
            CharactersInfo dataSO;
            Debug.Log("Save Nameless_Data Already");
            dataSO = Resources.Load<CharactersInfo>("CharacterScriptObjsInfo/Nameless");
            JsonUtility.FromJsonOverwrite(saveNamelessInfoToJson, dataSO);
            charInfoDictionary["�L�W"] = dataSO;
        }
        if (File.Exists(savePath + "/saveShiorhaiYaiInfo.Json"))
        {
            saveShiorhaiYaiInfoToJson = File.ReadAllText(savePath + "/saveShiorhaiYaiInfo.Json");
            CharactersInfo dataSO;
            Debug.Log("Save ShiorhaiYai_Data Already");
            dataSO = Resources.Load<CharactersInfo>("CharacterScriptObjsInfo/ShiorhaiYai");
            JsonUtility.FromJsonOverwrite(saveShiorhaiYaiInfoToJson, dataSO);
            charInfoDictionary["�զ�.�Ȧ�"] = dataSO;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        if (File.Exists(savePath + "/saveCharacterFragmentInfo.Json"))
        {
            saveCharacterFragmentInfoToJson = File.ReadAllText(savePath + "/saveCharacterFragmentInfo.Json");
            ItemsInfo dataSO;
            Debug.Log("Save CharacterFragment_Data Already");
            dataSO = Resources.Load<ItemsInfo>("ItemScriptObjsInfo/CharacterFragment");
            JsonUtility.FromJsonOverwrite(saveCharacterFragmentInfoToJson, dataSO);
            itemInfoDictionary["����H��"] = dataSO;
        }
        if (File.Exists(savePath + "/saveEnergyDrinkInfo.Json"))
        {
            saveEnergyDrinkInfoToJson = File.ReadAllText(savePath + "/saveEnergyDrinkInfo.Json");
            ItemsInfo dataSO;
            Debug.Log("Save EnergyDrink_Data Already");
            dataSO = Resources.Load<ItemsInfo>("ItemScriptObjsInfo/EnergyDrink");
            JsonUtility.FromJsonOverwrite(saveEnergyDrinkInfoToJson, dataSO);
            itemInfoDictionary["��q����"] = dataSO;
        }
        if (File.Exists(savePath + "/saveHpAddItemInfo.Json"))
        {
            saveHpAddItemInfoToJson = File.ReadAllText(savePath + "/saveHpAddItemInfo.Json");
            ItemsInfo dataSO;
            Debug.Log("Save HpAddItem_Data Already");
            dataSO = Resources.Load<ItemsInfo>("ItemScriptObjsInfo/HpAddItem");
            JsonUtility.FromJsonOverwrite(saveHpAddItemInfoToJson, dataSO);
            itemInfoDictionary["��q�W�[�D��"] = dataSO;
        }
        if (File.Exists(savePath + "/savePointBounsItemInfo.Json"))
        {
            savePointBounsItemInfoToJson = File.ReadAllText(savePath + "/savePointBounsItemInfo.Json");
            ItemsInfo dataSO;
            Debug.Log("Save PointBounsItem_Data Already");
            dataSO = Resources.Load<ItemsInfo>("ItemScriptObjsInfo/PointBounsItem");
            JsonUtility.FromJsonOverwrite(savePointBounsItemInfoToJson, dataSO);
            itemInfoDictionary["���ƥ[���D��"] = dataSO;
        }
        if (File.Exists(savePath + "/saveTrashInfo.Json"))
        {
            saveCharacterFragmentInfoToJson = File.ReadAllText(savePath + "/saveTrashInfo.Json");
            ItemsInfo dataSO;
            Debug.Log("Save Trash_Data Already");
            dataSO = Resources.Load<ItemsInfo>("ItemScriptObjsInfo/Trash");
            JsonUtility.FromJsonOverwrite(saveTrashInfoToJson, dataSO);
            itemInfoDictionary["�U��"] = dataSO;
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------
        if (File.Exists(savePath + "/saveSongsInfo.Json"))
        {
            saveSongsInfoToJson = File.ReadAllText(savePath + "/saveSongsInfo.Json");
            SongsInfo dataSO;
            Debug.Log("Save SongsInfo_Data Already");
            dataSO = Resources.Load<SongsInfo>("SongScriptObjsInfo/SongsInfo");
            JsonUtility.FromJsonOverwrite(saveSongsInfoToJson, dataSO);
            songsInfo = dataSO;

            return true;
        }
        //if (!Directory.Exists(savePathPC)) { return false; }
        //===================================================================================================================================================
        else
        {
            return false;
        }
    }
    public void ResetAllInfo()
    {
        myAccount.firstPlay = true;
        myAccount.tutorialClose = false;

        myAccount.AikaAmimi = false;
        myAccount.MalibetaRorem = false;
        myAccount.Nameless = false;
        myAccount.ShiorhaiYai = false;

        myAccount.curCharacterUse = MyAccount.CurCharacterUse.FelbelemAlice;

        myAccount.EnergyDrink = 0;
        myAccount.CharacterFragment = 0;
        myAccount.PointBounsItem = 0;
        myAccount.HpAddItem = 0;
        myAccount.Trash = 0;

        charInfoDictionary["�Ẹ���ۡE������"].charLevel = 1;
        charInfoDictionary["��d�E���̦�"].charLevel = 1;
        charInfoDictionary["��������E�ڭ�"].charLevel = 1;
        charInfoDictionary["�L�W"].charLevel = 1;
        charInfoDictionary["�զ�.�Ȧ�"].charLevel = 1;
    }
}

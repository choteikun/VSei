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
    public Dictionary<string, CharactersInfo> charInfoDictionary = new();//Character<名字,charsInfo>

    
    [System.Serializable]
    public struct ItemInfo
    {
        public string itemName;
        public ItemsInfo itemsInfo;
    }
    public Dictionary<string, ItemsInfo> itemInfoDictionary = new();//item<名字,ItemsInfo>

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
            //注意：若charactersInfoList出現相同的key轉換後只會導入第一次出現的數據，
            //重複key值視為bug並且沒有保護，請小心使用!

            if (!charInfoDictionary.ContainsKey(charInfoList[i].charName))//不存在這個key的話
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
            //注意：若itemsInfoList出現相同的key轉換後只會導入第一次出現的數據，
            //重複key值視為bug並且沒有保護，請小心使用!

            if (!itemInfoDictionary.ContainsKey(itemInfoList[i].itemName))//不存在這個key的話
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

        saveFelbelemAliceInfoToJson = JsonUtility.ToJson(charInfoDictionary["菲爾貝倫‧阿莉絲"]);
        saveAikaAmimiInfoToJson = JsonUtility.ToJson(charInfoDictionary["艾卡‧阿米米"]);
        saveMalibetaRoremInfoToJson = JsonUtility.ToJson(charInfoDictionary["瑪莉貝塔‧蘿倫"]);
        saveNamelessInfoToJson = JsonUtility.ToJson(charInfoDictionary["無名"]);
        saveShiorhaiYaiInfoToJson = JsonUtility.ToJson(charInfoDictionary["白灰.亞衣"]);

        saveCharacterFragmentInfoToJson = JsonUtility.ToJson(itemInfoDictionary["角色碎片"]);
        saveEnergyDrinkInfoToJson = JsonUtility.ToJson(itemInfoDictionary["能量飲料"]);
        saveHpAddItemInfoToJson = JsonUtility.ToJson(itemInfoDictionary["血量加成道具"]);
        savePointBounsItemInfoToJson = JsonUtility.ToJson(itemInfoDictionary["分數加成道具"]);
        saveTrashInfoToJson = JsonUtility.ToJson(itemInfoDictionary["垃圾"]);

        saveSongsInfoToJson = JsonUtility.ToJson(songsInfo);


        //if (!Directory.Exists(savePathPC)) { Directory.CreateDirectory(savePathPC); }//PC路徑沒有StreamingAssets則創建一個

        File.WriteAllText(savePath + "/saveMyAccount.Json", saveMyAccountToJson);
        //-------------------------------------------------------------------------------------------------以上寫入玩家個人帳戶資訊
        File.WriteAllText(savePath + "/saveFelbelemAliceInfo.Json", saveFelbelemAliceInfoToJson);
        File.WriteAllText(savePath + "/saveAikaAmimiInfo.Json", saveAikaAmimiInfoToJson);
        File.WriteAllText(savePath + "/saveMalibetaRoremInfo.Json", saveMalibetaRoremInfoToJson);
        File.WriteAllText(savePath + "/saveNamelessInfo.Json", saveNamelessInfoToJson);
        File.WriteAllText(savePath + "/saveShiorhaiYaiInfo.Json", saveShiorhaiYaiInfoToJson);
        //-------------------------------------------------------------------------------------------------以上寫入角色json檔
        File.WriteAllText(savePath + "/saveCharacterFragmentInfo.Json", saveCharacterFragmentInfoToJson);
        File.WriteAllText(savePath + "/saveEnergyDrinkInfo.Json", saveEnergyDrinkInfoToJson);
        File.WriteAllText(savePath + "/saveHpAddItemInfo.Json", saveHpAddItemInfoToJson);
        File.WriteAllText(savePath + "/savePointBounsItemInfo.Json", savePointBounsItemInfoToJson);
        File.WriteAllText(savePath + "/saveTrashInfo.Json", saveTrashInfoToJson);
        //-------------------------------------------------------------------------------------------------以上寫入Itemjson檔
        File.WriteAllText(savePath + "/saveSongsInfo.Json", saveSongsInfoToJson);
        //-------------------------------------------------------------------------------------------------以上寫入歌曲資訊json檔

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
            charInfoDictionary["菲爾貝倫‧阿莉絲"] = dataSO;
        }
        if (File.Exists(savePath + "/saveAikaAmimiInfo.Json"))
        {
            saveAikaAmimiInfoToJson = File.ReadAllText(savePath + "/saveAikaAmimiInfo.Json");
            CharactersInfo dataSO;
            Debug.Log("Save AikaAmimi_Data Already");
            dataSO = Resources.Load<CharactersInfo>("CharacterScriptObjsInfo/AikaAmimi");
            JsonUtility.FromJsonOverwrite(saveAikaAmimiInfoToJson, dataSO);
            charInfoDictionary["艾卡‧阿米米"] = dataSO;
        }
        if (File.Exists(savePath + "/saveMalibetaRoremInfo.Json"))
        {
            saveMalibetaRoremInfoToJson = File.ReadAllText(savePath + "/saveMalibetaRoremInfo.Json");
            CharactersInfo dataSO;
            Debug.Log("Save MalibetaRorem_Data Already");
            dataSO = Resources.Load<CharactersInfo>("CharacterScriptObjsInfo/MalibetaRorem");
            JsonUtility.FromJsonOverwrite(saveFelbelemAliceInfoToJson, dataSO);
            charInfoDictionary["瑪莉貝塔‧蘿倫"] = dataSO;
        }
        if (File.Exists(savePath + "/saveNamelessInfo.Json"))
        {
            saveNamelessInfoToJson = File.ReadAllText(savePath + "/saveNamelessInfo.Json");
            CharactersInfo dataSO;
            Debug.Log("Save Nameless_Data Already");
            dataSO = Resources.Load<CharactersInfo>("CharacterScriptObjsInfo/Nameless");
            JsonUtility.FromJsonOverwrite(saveNamelessInfoToJson, dataSO);
            charInfoDictionary["無名"] = dataSO;
        }
        if (File.Exists(savePath + "/saveShiorhaiYaiInfo.Json"))
        {
            saveShiorhaiYaiInfoToJson = File.ReadAllText(savePath + "/saveShiorhaiYaiInfo.Json");
            CharactersInfo dataSO;
            Debug.Log("Save ShiorhaiYai_Data Already");
            dataSO = Resources.Load<CharactersInfo>("CharacterScriptObjsInfo/ShiorhaiYai");
            JsonUtility.FromJsonOverwrite(saveShiorhaiYaiInfoToJson, dataSO);
            charInfoDictionary["白灰.亞衣"] = dataSO;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        if (File.Exists(savePath + "/saveCharacterFragmentInfo.Json"))
        {
            saveCharacterFragmentInfoToJson = File.ReadAllText(savePath + "/saveCharacterFragmentInfo.Json");
            ItemsInfo dataSO;
            Debug.Log("Save CharacterFragment_Data Already");
            dataSO = Resources.Load<ItemsInfo>("ItemScriptObjsInfo/CharacterFragment");
            JsonUtility.FromJsonOverwrite(saveCharacterFragmentInfoToJson, dataSO);
            itemInfoDictionary["角色碎片"] = dataSO;
        }
        if (File.Exists(savePath + "/saveEnergyDrinkInfo.Json"))
        {
            saveEnergyDrinkInfoToJson = File.ReadAllText(savePath + "/saveEnergyDrinkInfo.Json");
            ItemsInfo dataSO;
            Debug.Log("Save EnergyDrink_Data Already");
            dataSO = Resources.Load<ItemsInfo>("ItemScriptObjsInfo/EnergyDrink");
            JsonUtility.FromJsonOverwrite(saveEnergyDrinkInfoToJson, dataSO);
            itemInfoDictionary["能量飲料"] = dataSO;
        }
        if (File.Exists(savePath + "/saveHpAddItemInfo.Json"))
        {
            saveHpAddItemInfoToJson = File.ReadAllText(savePath + "/saveHpAddItemInfo.Json");
            ItemsInfo dataSO;
            Debug.Log("Save HpAddItem_Data Already");
            dataSO = Resources.Load<ItemsInfo>("ItemScriptObjsInfo/HpAddItem");
            JsonUtility.FromJsonOverwrite(saveHpAddItemInfoToJson, dataSO);
            itemInfoDictionary["血量增加道具"] = dataSO;
        }
        if (File.Exists(savePath + "/savePointBounsItemInfo.Json"))
        {
            savePointBounsItemInfoToJson = File.ReadAllText(savePath + "/savePointBounsItemInfo.Json");
            ItemsInfo dataSO;
            Debug.Log("Save PointBounsItem_Data Already");
            dataSO = Resources.Load<ItemsInfo>("ItemScriptObjsInfo/PointBounsItem");
            JsonUtility.FromJsonOverwrite(savePointBounsItemInfoToJson, dataSO);
            itemInfoDictionary["分數加成道具"] = dataSO;
        }
        if (File.Exists(savePath + "/saveTrashInfo.Json"))
        {
            saveCharacterFragmentInfoToJson = File.ReadAllText(savePath + "/saveTrashInfo.Json");
            ItemsInfo dataSO;
            Debug.Log("Save Trash_Data Already");
            dataSO = Resources.Load<ItemsInfo>("ItemScriptObjsInfo/Trash");
            JsonUtility.FromJsonOverwrite(saveTrashInfoToJson, dataSO);
            itemInfoDictionary["垃圾"] = dataSO;
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

        myAccount.Shachiku = false;
        myAccount.FragmentLover = false;
        myAccount.Salvage = false;

        myAccount.curCharacterUse = MyAccount.CurCharacterUse.FelbelemAlice;

        myAccount.EnergyDrink = 0;
        myAccount.CharacterFragment = 0;
        myAccount.PointBounsItem = 0;
        myAccount.HpAddItem = 0;
        myAccount.Trash = 0;

        charInfoDictionary["菲爾貝倫‧阿莉絲"].charLevel = 1;
        charInfoDictionary["艾卡‧阿米米"].charLevel = 1;
        charInfoDictionary["瑪莉貝塔‧蘿倫"].charLevel = 1;
        charInfoDictionary["無名"].charLevel = 1;
        charInfoDictionary["白灰.亞衣"].charLevel = 1;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom;
using SonicBloom.Koreo;
using UnityEngine.UI;
using System.IO;

public class IconDropSpawner : MonoBehaviour
{
    public MyAccount myAccount;
    public GameObject dropIconSpawner;
    [Tooltip("title畫面掉落下來的iconList")]
    public List<Image> dropIconList = new();
    [Tooltip("title畫面掉落下來的iconImages")]
    public Image[] dropIconImages;

    [EventID]
    public string eventID;
    public Koreography koreography;
    public List<KoreographyEvent> rhythmEvents;

    string saveMyAccountToJson;

    void Awake()
    {
        //dropIconList.Add(dropIconImages[0]);
        //dropIconList.Add(dropIconImages[1]);
        //if (myAccount.FragmentLover)
        //{
        //    dropIconList.Add(dropIconImages[2]);
        //}
        //if (myAccount.Shachiku)
        //{
        //    dropIconList.Add(dropIconImages[3]);
        //}
        //if (myAccount.Salvage)
        //{
        //    dropIconList.Add(dropIconImages[4]);
        //}
    }
    void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/saveMyAccount.Json"))
        {
            saveMyAccountToJson = File.ReadAllText(Application.persistentDataPath + "/saveMyAccount.Json");
            MyAccount dataSO;
            Debug.Log("Save MyAccount_Data Already");
            dataSO = Resources.Load<MyAccount>("CharacterScriptObjsInfo/MyAccount");
            JsonUtility.FromJsonOverwrite(saveMyAccountToJson, dataSO);
            myAccount = dataSO;
            Debug.Log("FirstPlayer: " + myAccount.firstPlay);
        }

        dropIconList.Add(dropIconImages[0]);
        dropIconList.Add(dropIconImages[1]);
        if (myAccount.FragmentLover)
        {
            dropIconList.Add(dropIconImages[2]);
        }
        if (myAccount.Shachiku)
        {
            dropIconList.Add(dropIconImages[3]);
        }
        if (myAccount.Salvage)
        {
            dropIconList.Add(dropIconImages[4]);
        }

        Debug.Log(dropIconList.Count);
        Debug.Log(myAccount.FragmentLover);

        //獲取koreography文件
        koreography = Koreographer.Instance.GetKoreographyAtIndex(0);

        rhythmEvents = koreography.GetTrackByID(eventID).GetAllEvents();

        Koreographer.Instance.RegisterForEvents(eventID, Maker);
    }

    void Maker(KoreographyEvent koreographyEvent)
    {   
        //Instantiate(DropIconImages[Random.Range(0, 2)], transform.position + new Vector3(Random.Range(-360.0f, 360.0f), 1000, 0), Quaternion.identity);
        Instantiate(dropIconList[Random.Range(0, dropIconList.Count)], transform.position + new Vector3(Random.Range(-320.0f, 320.0f), 0, 0), Quaternion.identity).transform.SetParent(dropIconSpawner.transform, false);
    }
}

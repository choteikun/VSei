using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom;
using SonicBloom.Koreo;
using UnityEngine.UI;

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



    void Start()
    {
        //switch將獲得的myaccount裡的設備bool去判斷加入image
        dropIconList.Add(dropIconImages[0]);
        dropIconList.Add(dropIconImages[1]);

        Debug.Log(dropIconList.Count);
        
        //獲取koreography文件
        koreography = Koreographer.Instance.GetKoreographyAtIndex(0);

        rhythmEvents = koreography.GetTrackByID(eventID).GetAllEvents();

        Koreographer.Instance.RegisterForEvents(eventID, Maker);
    }

    void Update()
    {
        
    }
    void Maker(KoreographyEvent koreographyEvent)
    {   
        //Instantiate(DropIconImages[Random.Range(0, 2)], transform.position + new Vector3(Random.Range(-360.0f, 360.0f), 1000, 0), Quaternion.identity);
        Instantiate(dropIconImages[Random.Range(0, dropIconList.Count)], transform.position + new Vector3(Random.Range(-320.0f, 320.0f), 0, 0), Quaternion.identity).transform.SetParent(dropIconSpawner.transform, false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom;
using SonicBloom.Koreo;
using UnityEngine.UI;

public class ObstacleSpawn : MonoBehaviour
{
    [Tooltip("譜面生成的物件")]
    public GameObject obstaclePrefab;


    [EventID]
    public string eventID;
    public Koreography koreography;
    public List<KoreographyEvent> rhythmEvents;

    void Start()
    {
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
        Instantiate(obstaclePrefab, transform.position + new Vector3(Random.Range(-360.0f, 360.0f), 1000, 0), Quaternion.identity);
    }
}

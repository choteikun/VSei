using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom;
using SonicBloom.Koreo;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RythmBeatSpawner : MonoBehaviour
{
    [Tooltip("�Э��ͦ�������")]
    public GameObject obstaclePrefab;
    public GameObject sensorL;
    public GameObject sensorR;

    public int curTime;//��e�ɶ�
    public int spawnTime;//������e�ͦ��`�窺�ɶ�
    public int beatsLimitTimeDifference;//���@�ӭȥΩ󭭨��l�����ɶ��t
    public int curCheckIdx;//Event Element

    public AudioSource audioCom;

    [EventID]
    public string eventID;
    public Koreography koreography;
    public List<KoreographyEvent> rhythmEvents;

    bool changeTrack;
    bool IsResume;

    void Start()
    {
        //Initialize events.
        koreography = Koreographer.Instance.GetKoreographyAtIndex(0);

        // Grab all the events out of the Koreography.
        rhythmEvents = koreography.GetTrackByID(eventID).GetAllEvents();

        Koreographer.Instance.RegisterForEvents(eventID, Maker);
        

        curCheckIdx = 0;
        spawnTime = 0;
        changeTrack = true;
        IsResume = false;
    }
    void Update()
    {
        if (curCheckIdx < rhythmEvents.Count)
        {
            curTime = Koreographer.GetSampleTime();
            //Debug.Log("Element " + curCheckIdx);//�ĴX��
            //Debug.Log("curTime" + curTime);
            //Debug.Log("StartSample" + rhythmEvents[curCheckIdx].StartSample);
            if (curTime > rhythmEvents[curCheckIdx].StartSample)
            {
                curCheckIdx++;
            }
        }
    }
    void Maker(KoreographyEvent koreographyEvent)
    {
        if (curTime - spawnTime > beatsLimitTimeDifference)//�̤֭n�W�L�h��beatsTimeDifference�~�ͦ��`��
        {
            if (changeTrack)
            {
                obstaclePrefab.name = "Element��" + curCheckIdx;
                Instantiate(obstaclePrefab, transform.position + new Vector3(sensorL.transform.position.x, 0, 0), Quaternion.identity);
                spawnTime = curTime;

                changeTrack = !changeTrack;
            }
            else
            {
                obstaclePrefab.name = "Element�k" + curCheckIdx;
                Instantiate(obstaclePrefab, transform.position + new Vector3(sensorR.transform.position.x, 0, 0), Quaternion.identity);
                spawnTime = curTime;

                changeTrack = !changeTrack;
            }
        }
    }
    public void Restart()
    {
        // Reset the audio.
        //audioCom.Stop();
        //audioCom.time = 0f;
        //audioCom.Play();

        SceneManager.LoadScene(2);

        //koreography.ResetTimings();

    }
    public void Resume()
    {
        IsResume = !IsResume;
        if (IsResume)
        {
            audioCom.Pause();
            Time.timeScale = 0;
        }
        else
        {
            audioCom.Play();
            Time.timeScale = 1;
        }
    }
}

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

    public GameObject sensorLL;
    public GameObject sensorL;
    public GameObject sensorR;
    public GameObject sensorRR;

    public int curTime;//��e�ɶ�
    public int spawnTime;//������e�ͦ��`�窺�ɶ�
    public int beatsLimitTimeDifference;//���@�ӭȥΩ󭭨��l�����ɶ��t
    public int curCheckIdx;//Event Element

    public AudioSource audioCom;

    [EventID]
    public string eventID;
    public Koreography koreography;
    public List<KoreographyEvent> rhythmEvents;

    //bool changeTrack;
    private ExcelLineManage LineManage;

    public enum SensorSet
    {
        SensorLL_SetPosX,
        SensorL_SetPosX,
        SensorR_SetPosX,
        SensorRR_SetPosX
    }
    public SensorSet sensorSet;

    void Start()
    {

        //Initialize events.
        koreography = Koreographer.Instance.GetKoreographyAtIndex(0);

        // Grab all the events out of the Koreography.
        rhythmEvents = koreography.GetTrackByID(eventID).GetAllEvents();

        Koreographer.Instance.RegisterForEvents(eventID, Maker);

        LineManage = Resources.Load<ExcelLineManage>("Release/rythmBeatSetTest");

        curCheckIdx = 0;
        spawnTime = 0;
        //changeTrack = true;
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
                //Debug.Log("Element" + curCheckIdx);
            }
        }
        
    }
    void Maker(KoreographyEvent koreographyEvent)
    {
        if (curTime - spawnTime > beatsLimitTimeDifference)//�̤֭n�W�L�h��beatsTimeDifference�~�ͦ��`��
        {
            if (LineManage.dataArray[curCheckIdx-1].BeatPosLL == "1")//����excel�W������m
            {
                sensorSet = SensorSet.SensorLL_SetPosX;
            }
            else if (LineManage.dataArray[curCheckIdx-1].BeatPosL == "1")
            {
                sensorSet = SensorSet.SensorL_SetPosX;
            }
            else if (LineManage.dataArray[curCheckIdx-1].BeatPosR == "1")
            {
                sensorSet = SensorSet.SensorR_SetPosX;
            }
            else if (LineManage.dataArray[curCheckIdx-1].BeatPosRR == "1")
            {
                sensorSet = SensorSet.SensorRR_SetPosX;
            }
            else { sensorSet = (SensorSet)Random.Range(0, System.Enum.GetValues(typeof(SensorSet)).Length); }

            Debug.Log("Element" + curCheckIdx);

            //obstaclePrefab.name = "Element" + curCheckIdx;

            switch (sensorSet)
            {
                case SensorSet.SensorLL_SetPosX:

                    //Instantiate(obstaclePrefab, transform.position + new Vector3(sensorLL.transform.position.x, 0, 0), Quaternion.identity);
                    PoolManager.Release(obstaclePrefab, transform.position + new Vector3(sensorLL.transform.position.x, 0, 0), Quaternion.identity);//�ͦ��`��
                    spawnTime = curTime;
                    break;
                case SensorSet.SensorL_SetPosX:

                    //Instantiate(obstaclePrefab, transform.position + new Vector3(sensorL.transform.position.x, 0, 0), Quaternion.identity);
                    PoolManager.Release(obstaclePrefab, transform.position + new Vector3(sensorL.transform.position.x, 0, 0), Quaternion.identity);
                    spawnTime = curTime;
                    break;
                case SensorSet.SensorR_SetPosX:
 
                    //Instantiate(obstaclePrefab, transform.position + new Vector3(sensorR.transform.position.x, 0, 0), Quaternion.identity);
                    PoolManager.Release(obstaclePrefab, transform.position + new Vector3(sensorR.transform.position.x, 0, 0), Quaternion.identity);
                    spawnTime = curTime;
                    break;
                case SensorSet.SensorRR_SetPosX:

                    //Instantiate(obstaclePrefab, transform.position + new Vector3(sensorRR.transform.position.x, 0, 0), Quaternion.identity);
                    PoolManager.Release(obstaclePrefab, transform.position + new Vector3(sensorRR.transform.position.x, 0, 0), Quaternion.identity);
                    spawnTime = curTime;
                    break;
                default:
                    break;
            }
            //if (changeTrack)
            //{
            //    obstaclePrefab.name = "Element��" + curCheckIdx;
            //    Instantiate(obstaclePrefab, transform.position + new Vector3(sensorL.transform.position.x, 0, 0), Quaternion.identity);
            //    spawnTime = curTime;

            //    changeTrack = !changeTrack;
            //}
            //else
            //{
            //    obstaclePrefab.name = "Element�k" + curCheckIdx;
            //    Instantiate(obstaclePrefab, transform.position + new Vector3(sensorR.transform.position.x, 0, 0), Quaternion.identity);
            //    spawnTime = curTime;

            //    changeTrack = !changeTrack;
            //}
        }
    }

    public void Restart()
    {
        // Reset the audio.
        //audioCom.Stop();
        //audioCom.time = 0f;
        //audioCom.Play();
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }
    public void PauseButton()
    {
        audioCom.Pause();
        Time.timeScale = 0;
    }
    public void ResumeButton()
    {
        audioCom.Play();
        Time.timeScale = 1;
    }
    public void BackHomeScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}

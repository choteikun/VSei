using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom;
using SonicBloom.Koreo;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SonicBloom.Koreo.Players;

public class RythmBeatSpawner : MonoBehaviour
{
    [Tooltip("Normal�Э��ͦ�������")]
    public GameObject normalBeatPrefab;
    [Tooltip("Hard�Э��ͦ�������")]
    public GameObject specialBeatPrefab;

    [Tooltip("�̾ںq���ܪ��q�����XData")]
    public SongsInfo songsInfo;

    public GameObject sensorLL;
    public GameObject sensorL;
    public GameObject sensorR;
    public GameObject sensorRR;

    public MyAccount myAccount;

    public int curTime;//��e�ɶ�
    public int spawnTime;//������e�ͦ��`�窺�ɶ�
    public int beatsLimitTimeDifference;//���@�ӭȥΩ󭭨��l�����ɶ��t
    public int curCheckIdx;//Normal Event Element

    public AudioSource audioCom;


    [EventID]
    public string normalEventID;
    [EventID]
    public string hardEventID;
    //public Koreography koreography;
    public List<KoreographyEvent> rhythmEvents;
    //���musicplayer script
    public SimpleMusicPlayer musicPlayer;
    //koreography list
    public List<Koreography> loadedKoreo = new();

    //bool changeTrack;
    private ExcelLineManageNormal LineManageNormal;
    private ExcelLineManageHard LineManageHard;

    public enum SensorSet
    {
        SensorLL_SetPosX,
        SensorL_SetPosX,
        SensorR_SetPosX,
        SensorRR_SetPosX
    }

    public SensorSet sensorSet;

    void Awake()
    {
        switch (songsInfo.songNumIs)
        {
            case 1:
                normalEventID = "StreetCityPop_SnareDrum";
                hardEventID = "StreetCityPop_HardNote";
                LineManageNormal = Resources.Load<ExcelLineManageNormal>("Release/rythmBeatNormalSet_StreetCityPop");
                LineManageHard = Resources.Load<ExcelLineManageHard>("Release/rythmBeatHardSet_StreetCityPop");
                break;
            case 2:
                normalEventID = "Take_Off_Into_the_Sky_SnareDrum";
                hardEventID = "Take_Off_Into_the_Sky_HardNote";
                LineManageNormal = Resources.Load<ExcelLineManageNormal>("Release/rythmBeatNormalSet_Take_Off_Into_the_Sky");
                LineManageHard = Resources.Load<ExcelLineManageHard>("Release/rythmBeatHardSet_Take_Off_Into_the_Sky");
                break;
            default:
                break;
        }
        
        
    }
    void Start()
    {
        Koreographer.Instance.GetAllLoadedKoreography(loadedKoreo);
        Koreographer.Instance.RegisterForEvents(normalEventID, NormalMaker);
        Koreographer.Instance.RegisterForEvents(hardEventID, HardMaker);

        musicPlayer.LoadSong(loadedKoreo[songsInfo.songNumIs - 1]);

        if (songsInfo.difficultySelection == "Normal")
        {
            rhythmEvents = loadedKoreo[songsInfo.songNumIs - 1].GetTrackByID(normalEventID).GetAllEvents();
        }
        if (songsInfo.difficultySelection == "Hard")
        {
            rhythmEvents = loadedKoreo[songsInfo.songNumIs - 1].GetTrackByID(hardEventID).GetAllEvents();
        }
        
        ////Initialize events.
        //koreography = Koreographer.Instance.GetKoreographyAtIndex(0);

        //// Grab all the events out of the Koreography.
        //rhythmEvents = koreography.GetTrackByID(eventID).GetAllEvents();

        //Koreographer.Instance.RegisterForEvents(eventID, Maker);

        //musicPlayer.LoadSong(koreography);

        //LineManageNormal = Resources.Load<ExcelLineManage>("Release/rythmBeatSetTest");
        //LineManageHard = Resources.Load<ExcelLineManage>("Release/XXX");

        curCheckIdx = 0;
        spawnTime = 0;
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
        if (!audioCom.isPlaying)//�p�G�q������
        {
            SceneManager.LoadScene("FinalScene");//�q���������
            myAccount.HpAddItemUsing = false;//��q�W�[�D��ĪG����
            myAccount.PointBounsItemUsing = false;//���ƥ[���D��ĪG����
        }
    }
    void NormalMaker(KoreographyEvent koreographyEvent)
    {
        if (songsInfo.difficultySelection == "Normal")
        {
            RythmNormalBeatPosSet();
        }
    }
    void HardMaker(KoreographyEvent koreographyEvent)
    {
        if(songsInfo.difficultySelection == "Hard")
        {
            RythmHardBeatPosSet();
        }
    }

    public void RythmNormalBeatPosSet()//���q���׸`���mset
    {
        if (curTime - spawnTime > beatsLimitTimeDifference)//�̤֭n�W�L�h��beatsTimeDifference�~�ͦ��`��
        {
            if (LineManageNormal.dataArray[curCheckIdx].BeatPosLL == "1")//����excel�W������m
            {
                //sensorSet = SensorSet.SensorLL_SetPosX;
                PoolManager.Release(normalBeatPrefab, transform.position + new Vector3(sensorLL.transform.position.x, 0, 0), Quaternion.identity);//�ͦ��`��
                spawnTime = curTime;
            }
            if (LineManageNormal.dataArray[curCheckIdx].BeatPosL == "1")
            {
                //sensorSet = SensorSet.SensorL_SetPosX;
                PoolManager.Release(normalBeatPrefab, transform.position + new Vector3(sensorL.transform.position.x, 0, 0), Quaternion.identity);
                spawnTime = curTime;
            }
            if (LineManageNormal.dataArray[curCheckIdx].BeatPosR == "1")
            {
                //sensorSet = SensorSet.SensorR_SetPosX;
                PoolManager.Release(normalBeatPrefab, transform.position + new Vector3(sensorR.transform.position.x, 0, 0), Quaternion.identity);
                spawnTime = curTime;
            }
            if (LineManageNormal.dataArray[curCheckIdx].BeatPosRR == "1")
            {
                //sensorSet = SensorSet.SensorRR_SetPosX;
                PoolManager.Release(normalBeatPrefab, transform.position + new Vector3(sensorRR.transform.position.x, 0, 0), Quaternion.identity);
                spawnTime = curTime;
            }
            //-----------------------------------------------------------------------------------------------------------------------------------------------------------
            if (LineManageNormal.dataArray[curCheckIdx].BeatPosLL == "2")//����excel�W������m
            {
                //sensorSet = SensorSet.SensorLL_SetPosX;
                PoolManager.Release(specialBeatPrefab, transform.position + new Vector3(sensorLL.transform.position.x, 0, 0), Quaternion.identity);//�ͦ��`��
                spawnTime = curTime;
            }
            if (LineManageNormal.dataArray[curCheckIdx].BeatPosL == "2")
            {
                //sensorSet = SensorSet.SensorL_SetPosX;
                PoolManager.Release(specialBeatPrefab, transform.position + new Vector3(sensorL.transform.position.x, 0, 0), Quaternion.identity);
                spawnTime = curTime;
            }
            if (LineManageNormal.dataArray[curCheckIdx].BeatPosR == "2")
            {
                //sensorSet = SensorSet.SensorR_SetPosX;
                PoolManager.Release(specialBeatPrefab, transform.position + new Vector3(sensorR.transform.position.x, 0, 0), Quaternion.identity);
                spawnTime = curTime;
            }
            if (LineManageNormal.dataArray[curCheckIdx].BeatPosRR == "2")
            {
                //sensorSet = SensorSet.SensorRR_SetPosX;
                PoolManager.Release(specialBeatPrefab, transform.position + new Vector3(sensorRR.transform.position.x, 0, 0), Quaternion.identity);
                spawnTime = curTime;
            }
            if ((LineManageNormal.dataArray[curCheckIdx].BeatPosLL != "1" && LineManageNormal.dataArray[curCheckIdx].BeatPosL != "1" && LineManageNormal.dataArray[curCheckIdx].BeatPosR != "1" && LineManageNormal.dataArray[curCheckIdx].BeatPosRR != "1")
                && (LineManageNormal.dataArray[curCheckIdx].BeatPosLL != "0" && LineManageNormal.dataArray[curCheckIdx].BeatPosL != "0" && LineManageNormal.dataArray[curCheckIdx].BeatPosR != "0" && LineManageNormal.dataArray[curCheckIdx].BeatPosRR != "0")
                && (LineManageNormal.dataArray[curCheckIdx].BeatPosLL != "2" && LineManageNormal.dataArray[curCheckIdx].BeatPosL != "2" && LineManageNormal.dataArray[curCheckIdx].BeatPosR != "2" && LineManageNormal.dataArray[curCheckIdx].BeatPosRR != "2"))
                //�Yexcel�̨S������Ʀr�O1&0�h�H���w�Ʀ�m(�@��ƥu�|��1�Ӹ`��ͦ�)
            {
                //Debug.Log("no rythmBeats");
                switch (sensorSet)//���t��m
                {
                    case SensorSet.SensorLL_SetPosX:

                        //Instantiate(obstaclePrefab, transform.position + new Vector3(sensorLL.transform.position.x, 0, 0), Quaternion.identity);
                        PoolManager.Release(normalBeatPrefab, transform.position + new Vector3(sensorLL.transform.position.x, 0, 0), Quaternion.identity);//�ͦ��`��
                        spawnTime = curTime;
                        break;
                    case SensorSet.SensorL_SetPosX:

                        //Instantiate(obstaclePrefab, transform.position + new Vector3(sensorL.transform.position.x, 0, 0), Quaternion.identity);
                        PoolManager.Release(normalBeatPrefab, transform.position + new Vector3(sensorL.transform.position.x, 0, 0), Quaternion.identity);
                        spawnTime = curTime;
                        break;
                    case SensorSet.SensorR_SetPosX:

                        //Instantiate(obstaclePrefab, transform.position + new Vector3(sensorR.transform.position.x, 0, 0), Quaternion.identity);
                        PoolManager.Release(normalBeatPrefab, transform.position + new Vector3(sensorR.transform.position.x, 0, 0), Quaternion.identity);
                        spawnTime = curTime;
                        break;
                    case SensorSet.SensorRR_SetPosX:

                        //Instantiate(obstaclePrefab, transform.position + new Vector3(sensorRR.transform.position.x, 0, 0), Quaternion.identity);
                        PoolManager.Release(normalBeatPrefab, transform.position + new Vector3(sensorRR.transform.position.x, 0, 0), Quaternion.identity);
                        spawnTime = curTime;
                        break;
                    default:
                        break;
                }
                sensorSet = (SensorSet)Random.Range(0, System.Enum.GetValues(typeof(SensorSet)).Length);
            }
            //Debug.Log("Element" + curCheckIdx);

            //obstaclePrefab.name = "Element" + curCheckIdx;
        }
    }
    public void RythmHardBeatPosSet()//�x�����׸`���mset
    {
        if (curTime - spawnTime > beatsLimitTimeDifference)//�̤֭n�W�L�h��beatsTimeDifference�~�ͦ��`��
        {
            if (LineManageHard.dataArray[curCheckIdx].BeatPosLL == "1")//����excel�W������m1�O�@�뱼����2�O�S������0�O�S��
            {
                PoolManager.Release(normalBeatPrefab, transform.position + new Vector3(sensorLL.transform.position.x, 0, 0), Quaternion.identity);//�ͦ��`��
                spawnTime = curTime;
            }
            if (LineManageHard.dataArray[curCheckIdx].BeatPosL == "1")
            {
                PoolManager.Release(normalBeatPrefab, transform.position + new Vector3(sensorL.transform.position.x, 0, 0), Quaternion.identity);
                spawnTime = curTime;
            }
            if (LineManageHard.dataArray[curCheckIdx].BeatPosR == "1")
            {
                PoolManager.Release(normalBeatPrefab, transform.position + new Vector3(sensorR.transform.position.x, 0, 0), Quaternion.identity);
                spawnTime = curTime;
            }
            if (LineManageHard.dataArray[curCheckIdx].BeatPosRR == "1")
            {
                PoolManager.Release(normalBeatPrefab, transform.position + new Vector3(sensorRR.transform.position.x, 0, 0), Quaternion.identity);
                spawnTime = curTime;
            }
            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            if (LineManageHard.dataArray[curCheckIdx].BeatPosLL == "2")
            {
                PoolManager.Release(specialBeatPrefab, transform.position + new Vector3(sensorLL.transform.position.x, 0, 0), Quaternion.identity);//�ͦ��`��
                spawnTime = curTime;
            }
            if (LineManageHard.dataArray[curCheckIdx].BeatPosL == "2")
            {
                PoolManager.Release(specialBeatPrefab, transform.position + new Vector3(sensorL.transform.position.x, 0, 0), Quaternion.identity);
                spawnTime = curTime;
            }
            if (LineManageHard.dataArray[curCheckIdx].BeatPosR == "2")
            {
                PoolManager.Release(specialBeatPrefab, transform.position + new Vector3(sensorR.transform.position.x, 0, 0), Quaternion.identity);
                spawnTime = curTime;
            }
            if (LineManageHard.dataArray[curCheckIdx].BeatPosRR == "2")
            {
                PoolManager.Release(specialBeatPrefab, transform.position + new Vector3(sensorRR.transform.position.x, 0, 0), Quaternion.identity);
                spawnTime = curTime;
            }
            if ((LineManageHard.dataArray[curCheckIdx].BeatPosLL != "1" && LineManageHard.dataArray[curCheckIdx].BeatPosL != "1" && LineManageHard.dataArray[curCheckIdx].BeatPosR != "1" && LineManageHard.dataArray[curCheckIdx].BeatPosRR != "1")
                && (LineManageHard.dataArray[curCheckIdx].BeatPosLL != "0" && LineManageHard.dataArray[curCheckIdx].BeatPosL != "0" && LineManageHard.dataArray[curCheckIdx].BeatPosR != "0" && LineManageHard.dataArray[curCheckIdx].BeatPosRR != "0")
                && (LineManageHard.dataArray[curCheckIdx].BeatPosLL != "2" && LineManageHard.dataArray[curCheckIdx].BeatPosL != "2" && LineManageHard.dataArray[curCheckIdx].BeatPosR != "2" && LineManageHard.dataArray[curCheckIdx].BeatPosRR != "2")) //�Yexcel�̨S������Ʀr�O1�h�H���w�Ʀ�m(�@��ƥu�|��1�Ӹ`��ͦ�)
            {
                //Debug.Log("no rythmBeats");
                switch (sensorSet)//���t��m
                {
                    case SensorSet.SensorLL_SetPosX:
                        PoolManager.Release(normalBeatPrefab, transform.position + new Vector3(sensorLL.transform.position.x, 0, 0), Quaternion.identity);//�ͦ��`��
                        spawnTime = curTime;
                        break;
                    case SensorSet.SensorL_SetPosX:
                        PoolManager.Release(normalBeatPrefab, transform.position + new Vector3(sensorL.transform.position.x, 0, 0), Quaternion.identity);
                        spawnTime = curTime;
                        break;
                    case SensorSet.SensorR_SetPosX:
                        PoolManager.Release(normalBeatPrefab, transform.position + new Vector3(sensorR.transform.position.x, 0, 0), Quaternion.identity);
                        spawnTime = curTime;
                        break;
                    case SensorSet.SensorRR_SetPosX:
                        PoolManager.Release(normalBeatPrefab, transform.position + new Vector3(sensorRR.transform.position.x, 0, 0), Quaternion.identity);
                        spawnTime = curTime;
                        break;
                    default:
                        break;
                }
                sensorSet = (SensorSet)Random.Range(0, System.Enum.GetValues(typeof(SensorSet)).Length);
            }
            //Debug.Log("Element" + curCheckIdx);
            //obstaclePrefab.name = "Element" + curCheckIdx;
        }
    }
    public void Restart()
    {
        // Reset the audio.
        //audioCom.Stop();
        //audioCom.time = 0f;
        //audioCom.Play();
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
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
        SceneManager.LoadScene("HomeScene");
    }

}

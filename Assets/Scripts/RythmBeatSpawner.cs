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
    [Tooltip("Normal譜面生成的物件")]
    public GameObject normalBeatPrefab;
    [Tooltip("Hard譜面生成的物件")]
    public GameObject specialBeatPrefab;

    [Tooltip("依據歌單選擇的歌曲號碼Data")]
    public SongsInfo songsInfo;

    public GameObject sensorLL;
    public GameObject sensorL;
    public GameObject sensorR;
    public GameObject sensorRR;

    public MyAccount myAccount;

    public int curTime;//當前時間
    public int spawnTime;//紀錄當前生成節拍的時間
    public int beatsLimitTimeDifference;//取一個值用於限制拍子間的時間差
    public int curCheckIdx;//Normal Event Element

    public AudioSource audioCom;


    [EventID]
    public string normalEventID;
    [EventID]
    public string hardEventID;
    //public Koreography koreography;
    public List<KoreographyEvent> rhythmEvents;
    //獲取musicplayer script
    public SimpleMusicPlayer musicPlayer;
    //koreography list
    public List<Koreography> loadedKoreo = new();

    //bool changeTrack;
    private ExcelLineManageNormal LineManageNormal;
    private ExcelLineManageHard LineManageHard;
    bool gameIsPause;

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
        gameIsPause = false;
    }
    void Update()
    {
        if (curCheckIdx < rhythmEvents.Count)
        {
            curTime = Koreographer.GetSampleTime();
            //Debug.Log("Element " + curCheckIdx);//第幾拍
            //Debug.Log("curTime" + curTime);
            //Debug.Log("StartSample" + rhythmEvents[curCheckIdx].StartSample);
            if (curTime > rhythmEvents[curCheckIdx].StartSample)
            {
                curCheckIdx++;
                //Debug.Log("Element" + curCheckIdx);
            }
        }
        if (!audioCom.isPlaying & !gameIsPause)//如果歌曲播放完
        { 
            switch (myAccount.curCharacterUse)
            {
                case MyAccount.CurCharacterUse.FelbelemAlice:
                    SceneManager.LoadScene("AliceFinish");
                    break;
                case MyAccount.CurCharacterUse.AikaAmimi:
                    SceneManager.LoadScene("AmimiFinish");
                    break;
                case MyAccount.CurCharacterUse.MalibetaRorem:
                    SceneManager.LoadScene("LorenFinish");
                    break;
                case MyAccount.CurCharacterUse.Nameless:
                    SceneManager.LoadScene("NonameFinish");
                    break;
                case MyAccount.CurCharacterUse.ShiorhaiYai:
                    SceneManager.LoadScene("YaiFinish");
                    break;
                default:
                    break;
            }
            myAccount.HpAddItemUsing = false;//血量增加道具效果關閉
            myAccount.PointBounsItemUsing = false;//分數加成道具效果關閉
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

    public void RythmNormalBeatPosSet()//普通難度節拍位置set
    {
        if (curTime - spawnTime > beatsLimitTimeDifference)//最少要超過多少beatsTimeDifference才生成節拍
        {
            if (LineManageNormal.dataArray[curCheckIdx].BeatPosLL == "1")//按照excel上的表格位置
            {
                //sensorSet = SensorSet.SensorLL_SetPosX;
                PoolManager.Release(normalBeatPrefab, transform.position + new Vector3(sensorLL.transform.position.x, 0, 0), Quaternion.identity);//生成節拍
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
            if (LineManageNormal.dataArray[curCheckIdx].BeatPosLL == "2")//按照excel上的表格位置
            {
                //sensorSet = SensorSet.SensorLL_SetPosX;
                PoolManager.Release(specialBeatPrefab, transform.position + new Vector3(sensorLL.transform.position.x, 0, 0), Quaternion.identity);//生成節拍
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
                //若excel裡沒有任何數字是1&0則隨機安排位置(一橫排只會有1個節拍生成)
            {
                //Debug.Log("no rythmBeats");
                switch (sensorSet)//分配位置
                {
                    case SensorSet.SensorLL_SetPosX:

                        //Instantiate(obstaclePrefab, transform.position + new Vector3(sensorLL.transform.position.x, 0, 0), Quaternion.identity);
                        PoolManager.Release(normalBeatPrefab, transform.position + new Vector3(sensorLL.transform.position.x, 0, 0), Quaternion.identity);//生成節拍
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
    public void RythmHardBeatPosSet()//困難難度節拍位置set
    {
        if (curTime - spawnTime > beatsLimitTimeDifference)//最少要超過多少beatsTimeDifference才生成節拍
        {
            if (LineManageHard.dataArray[curCheckIdx].BeatPosLL == "1")//按照excel上的表格位置1是一般掉落物2是特殊掉落物0是沒有
            {
                PoolManager.Release(normalBeatPrefab, transform.position + new Vector3(sensorLL.transform.position.x, 0, 0), Quaternion.identity);//生成節拍
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
                PoolManager.Release(specialBeatPrefab, transform.position + new Vector3(sensorLL.transform.position.x, 0, 0), Quaternion.identity);//生成節拍
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
                && (LineManageHard.dataArray[curCheckIdx].BeatPosLL != "2" && LineManageHard.dataArray[curCheckIdx].BeatPosL != "2" && LineManageHard.dataArray[curCheckIdx].BeatPosR != "2" && LineManageHard.dataArray[curCheckIdx].BeatPosRR != "2")) //若excel裡沒有任何數字是1則隨機安排位置(一橫排只會有1個節拍生成)
            {
                //Debug.Log("no rythmBeats");
                switch (sensorSet)//分配位置
                {
                    case SensorSet.SensorLL_SetPosX:
                        PoolManager.Release(normalBeatPrefab, transform.position + new Vector3(sensorLL.transform.position.x, 0, 0), Quaternion.identity);//生成節拍
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
        //gameIsPause = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }
    public void PauseButton()
    {
        gameIsPause = true;
        audioCom.Pause();
        Time.timeScale = 0;
    }
    public void ResumeButton()
    {
        gameIsPause = false;
        audioCom.Play();
        Time.timeScale = 1;
    }
    public void BackHomeScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("HomeScene");
    }

}

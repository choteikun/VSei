using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SongSelect : MonoBehaviour
{
    public SongsInfo songsInfo;
    public void SongSelect_1()
    {
        songsInfo.songNumIs = 1;
    }
    public void SongSelect_2()
    {
        songsInfo.songNumIs = 2;
    }
    public void SongDifficultySelection_Normal()
    {
        songsInfo.difficultySelection = "Normal";
        EnterGameScene();
    }
    public void SongDifficultySelection_Hard()
    {
        songsInfo.difficultySelection = "Hard";
        EnterGameScene();
    }
    void EnterGameScene()
    {
        SceneManager.LoadScene("GameScene");
        Destroy(GameObject.FindGameObjectWithTag("DialogueManager"));
    }
}

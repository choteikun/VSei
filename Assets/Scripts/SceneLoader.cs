using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    private CanvasScaler canvasScaler;
    public MyAccount myAccount;


    void Start()
    {
        Debug.Log("Screen.width : " + Screen.width);
        Debug.Log("Screen.height : " + Screen.height);

        canvasScaler = GetComponent<CanvasScaler>();

        Application.targetFrameRate = 120;
        QualitySettings.vSyncCount = 0;

        if (Screen.width > canvasScaler.referenceResolution.x) 
        {
            canvasScaler.matchWidthOrHeight = 1.0f;
        }
        else
        {
            canvasScaler.matchWidthOrHeight = 0f;
        }
    }
    public void StartScene()
    {
        //AudioSourceController.PlaySE("Sounds_SE", "");
        StartCoroutine(DelayedStartScene());
    }
    public void HomeScene()
    {
        //AudioSourceController.PlaySE("Sounds_SE", "");
        StartCoroutine(DelayedHomeScene());
    }
    public void GameScene()
    {
        //AudioSourceController.PlaySE("Sounds_SE", "");
        StartCoroutine(DelayedGameScene());
    }
    public void StoreScene()
    {
        //AudioSourceController.PlaySE("Sounds_SE", "");
        StartCoroutine(DelayedStoreScene());
    }
    public void SongSelectScene()
    {
        //AudioSourceController.PlaySE("Sounds_SE", "");
        StartCoroutine(DelayedSongSelectScene());
    }
    public void CreateAccountScene()
    {
        if (myAccount.firstPlay == true)
        {
            //AudioSourceController.PlaySE("Sounds_SE", "");
            StartCoroutine(DelayedCreateAccountScene());
            myAccount.firstPlay = false;
        }
        else
        {
            //AudioSourceController.PlaySE("Sounds_SE", "");
            StartCoroutine(DelayedHomeScene());
        }
    }
    
    //public void FinalScene()
    //{
    //    //AudioSourceController.PlaySE("Sounds_SE", "");
    //    StartCoroutine(DelayedFinalScene());
    //}
    public void ExitGame()
    {
        Application.Quit();
    }
    IEnumerator DelayedStartScene()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("StartScene");
    }
    IEnumerator DelayedHomeScene()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("HomeScene");
    }
    IEnumerator DelayedGameScene()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("GameScene");
    }
    IEnumerator DelayedStoreScene()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("StoreScene");
    }
    IEnumerator DelayedSongSelectScene()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("SongSelectScene");
    }
    IEnumerator DelayedCreateAccountScene()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("CreateAccount");
    }
    //IEnumerator DelayedFinalScene()
    //{
    //    yield return new WaitForSeconds(0.3f);
    //    SceneManager.LoadScene(6);
    //}
}

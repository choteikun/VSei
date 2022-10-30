using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    private CanvasScaler canvasScaler;
    void Start()
    {
        Debug.Log("Screen.width : " + Screen.width);
        Debug.Log("Screen.height : " + Screen.height);

        canvasScaler = GetComponent<CanvasScaler>();

        Application.targetFrameRate = 60;
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
    public void FinalScene()
    {
        //AudioSourceController.PlaySE("Sounds_SE", "");
        StartCoroutine(DelayedFinalScene());
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    IEnumerator DelayedStartScene()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(0);
    }
    IEnumerator DelayedHomeScene()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(1);
    }
    IEnumerator DelayedGameScene()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(2);
    }
    IEnumerator DelayedStoreScene()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(3);
    }    
    IEnumerator DelayedFinalScene()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(4);
    }
}

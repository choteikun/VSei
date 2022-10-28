using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
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

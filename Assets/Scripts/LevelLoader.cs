using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : /*Singleton<LevelLoader>*/ MonoBehaviour
{
    public Animator transition;
    public float transitionTime;

    public const string gameSceneName = "Game";
    public const string aboutSceneName = "About";
    public const string menuSceneName = "Menu";
    public const string tutorialSceneName = "Tutorial";

     public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));
    }

    IEnumerator LoadLevel(string sceneName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);
    }
}

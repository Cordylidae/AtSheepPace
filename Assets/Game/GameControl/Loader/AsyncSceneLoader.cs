using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class AsyncSceneLoader : MonoBehaviour
{
    public AsyncOperation scene;

    public void LoadAsync(string nameOfScene)
    {
        StartCoroutine(LoadScene(nameOfScene));
    }

    IEnumerator LoadScene(string sceneName)
    {
        yield return null;

        scene = SceneManager.LoadSceneAsync(sceneName);

        scene.allowSceneActivation = false;
        
        Debug.Log("Pro :" + scene.progress);
        
        while (!scene.isDone)
        {
            if (scene.progress >= 0.9f)
            {
                scene.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}



public struct GameSceneName
{
    public const string MainMenu = "Scene_MainMenu";
    public const string LevelsMap = "Scene_LevelMap";
    public const string CurrentLevel = "Scene_CoreGame";
}

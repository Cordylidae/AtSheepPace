using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using Zenject;

public class AsyncSceneLoader : MonoBehaviour
{
    public AsyncOperation scene;
    bool isChanged = true;

    public void LoadAsync(string nameOfScene)
    {
        LoadScene(nameOfScene);
    }

    async void LoadScene(string sceneName)
    {
        if (!isChanged) return;
        else isChanged = false;

        await Task.Yield();

        scene = SceneManager.LoadSceneAsync(sceneName);

        scene.allowSceneActivation = false;
        
        Debug.Log("Pro :" + scene.progress);
        
        while (!scene.isDone)
        {
            if (scene.progress >= 0.9f)
            {
                scene.allowSceneActivation = true;
            }

            await Task.Yield();
        }

        isChanged = true;
    }
}

public struct GameSceneName
{
    public const string MainMenu = "Scene_MainMenu";
    public const string LevelsMap = "Scene_LevelMap";
    public const string CoreGame = "Scene_CoreGame";
}

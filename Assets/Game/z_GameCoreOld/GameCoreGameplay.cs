using GameInstance;
using UnityEngine;
using Zenject;

// # TODO Splite UI and Contrpled things

public class GameCoreGameplay : MonoBehaviour
{
    // Control
    [SerializeField] private SimpleLevelGeneration levelGeneration;

    [SerializeField] private AsyncSceneLoader loader;

    //Need remove from there
    [Inject]
    MapInstance mapInstance;

    [Inject]
    LevelResultInctance levelResultInctance;

    [Inject]
    PreloadLevelInstance preloadLevelInstance;

    public void Awake()
    {
        Debug.Log(preloadLevelInstance.LevelType);
        if (preloadLevelInstance.LevelType == LevelType.Simple) levelGeneration.myAwake();
        else Win();

        levelGeneration.Win += Win_SimpleLevel;
        levelGeneration.Lose += Lose_SimpleLevel;
    }

    public void Start()
    {
        if(preloadLevelInstance.LevelType == LevelType.Simple) levelGeneration.myStart();
    }

    void Win()
    {
        mapInstance.CompleteLevel();

        levelResultInctance.status = LevelResultInctance.Status.Win;
        loader.LoadAsync("Scene_CoreGamePost");
    }

    void Win_SimpleLevel()
    { 
        mapInstance.CompleteLevel();

        levelGeneration.ClearRounds();

        levelResultInctance.status = LevelResultInctance.Status.Win;
        loader.LoadAsync("Scene_CoreGamePost");
    }

    void Lose_SimpleLevel()
    {
        levelGeneration.ClearRounds();

        levelResultInctance.status = LevelResultInctance.Status.Lose;

        loader.LoadAsync("Scene_CoreGamePost");
    }
}

using GameInstance;
using UnityEngine;
using Zenject;

// # TODO Splite UI and Contrpled things

public class GameCoreGameplay : MonoBehaviour
{
    // Control
    [SerializeField] private LevelGeneration levelGeneration;

    [SerializeField] private AsyncSceneLoader loader;

    [Inject]
    MapInstance mapInstance;

    [Inject]
    LevelResultInctance levelResultInctance;

    public void Awake()
    {
        if (mapInstance.levels[mapInstance.currentUniqIndex].type == LevelType.Simple) levelGeneration.myAwake();
        else Win();

        levelGeneration.Win += Win_SimpleLevel;
        levelGeneration.Lose += Lose_SimpleLevel;
    }

    public void Start()
    {
        if (mapInstance.levels[mapInstance.currentUniqIndex].type == LevelType.Simple) levelGeneration.myStart();
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

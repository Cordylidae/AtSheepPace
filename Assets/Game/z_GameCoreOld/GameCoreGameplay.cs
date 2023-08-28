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
        levelGeneration.Win += Win;
        levelGeneration.Lose += Lose;

        if (mapInstance.levels[mapInstance.currentUniqIndex].type == LevelType.Simple) levelGeneration.myAwake();
        else Win();
    }

    public void Start()
    {
        if (mapInstance.levels[mapInstance.currentUniqIndex].type == LevelType.Simple) levelGeneration.myStart();
    }

    void Win()
    { 
        mapInstance.CompleteLevel();

        levelGeneration.ClearRounds();

        levelResultInctance.status = LevelResultInctance.Status.Win;
        loader.LoadAsync("Scene_CoreGamePost");
    }

    void Lose()
    {
        levelGeneration.ClearRounds();

        levelResultInctance.status = LevelResultInctance.Status.Lose;

        loader.LoadAsync("Scene_CoreGamePost");
    }
}

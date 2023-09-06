using GameInstance;
using UnityEngine;
using Zenject;

// # TODO Splite UI and Contrpled things

public class GameCoreGameplay : MonoBehaviour
{
    [Header("Control")]
    [SerializeField] private SimpleLevelGeneration levelGeneration;
    [SerializeField] private TutorialLevelGeneration tutorialGeneration;

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
        if (preloadLevelInstance.LevelType == LevelType.Simple)
        {
            levelGeneration.myAwake();

            levelGeneration.Win += Win;
            levelGeneration.Lose += Lose;
        }
        else if (preloadLevelInstance.LevelType == LevelType.Tutorial) 
        {
            tutorialGeneration.myAwake();

            tutorialGeneration.Win += Win;
            tutorialGeneration.Lose += Lose;
        }
        else Win();
    }

    public void Start()
    {
        if(preloadLevelInstance.LevelType == LevelType.Simple) levelGeneration.myStart();
        else if (preloadLevelInstance.LevelType == LevelType.Tutorial) tutorialGeneration.myStart();
    }

    public void Win()
    {
        mapInstance.CompleteLevel();

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

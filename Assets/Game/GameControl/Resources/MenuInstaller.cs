using GameInstance;
using System.Collections.Generic;
using Zenject;
using LevelSettings;

public class MenuInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindPlayerInstance();
    }

    private void BindPlayerInstance()
    {
        PlayerInstance playerInstance = AfterLoadPlayer();
        Container.Bind<PlayerInstance>().FromInstance(playerInstance).AsSingle().NonLazy();

        MapInstance levelMapInstance = AfterLoadLevelMap();
        Container.Bind<MapInstance>().FromInstance(levelMapInstance).AsSingle().NonLazy();

        LevelResultInctance levelResultInctance = new LevelResultInctance();
        Container.Bind<LevelResultInctance>().FromInstance(levelResultInctance).AsSingle().NonLazy();

        PreloadLevelInstance preLoadLevelInstance = new PreloadLevelInstance();
        Container.Bind<PreloadLevelInstance>().FromInstance(preLoadLevelInstance).AsSingle().NonLazy();
    }

    private MapInstance AfterLoadLevelMap()
    {
        List<LevelInstance> levels = new List<LevelInstance>
        {
            new TutorialLevel(0, LevelState.New), //Sheep
            new TutorialLevel(1), //Fearbar
            new SimpleLevel(2),
            new TutorialLevel(3), // Wolf
            new SimpleLevel(4),
            new SimpleLevel(5),
            new UnlimitedLevel(6),
        };

        return new MapInstance(levels);
    }

    private PlayerInstance AfterLoadPlayer()
    {
        List<StatisticObject> allStatistics = new List<StatisticObject>
        {
            new StatisticObject(AnimalType.Sheep, 12, 10, 2),
            new StatisticObject(AnimalType.Wolf, 8, 5, 3),
            new StatisticObject(AnimalType.Deer, 10, 5, 5)
        };

        Dictionary<int, float> allLevelResults = new Dictionary<int, float>
        {
            { 1, 1234.0f },
            { 2, 2475.0f },
            { 3, 1753.0f }
        };

        return new PlayerInstance(2, 2, 0, allStatistics, allLevelResults);
    }
}

namespace GameInstance
{
    public class PreloadLevelInstance
    {
        public LevelType LevelType;
        public LevelSettingsModel settings_simple;
        public TutorialSettingsModel settings_tutorial;

        public PreloadLevelInstance()
        {
            LevelType = LevelType.Simple;
            settings_simple = new LevelSettingsModel();
            settings_tutorial = new TutorialSettingsModel();
        }
    }
 };
using GameInstance;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

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

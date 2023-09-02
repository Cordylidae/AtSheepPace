using GameInstance;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private LevelButtonPrototype prototype;

    [Space, Header("Level Positions")]
    [SerializeField] private GameObject AllLevelPosition;
    private List<LevelView> levelViews;

    [Inject]
    MapInstance mapInstance;

    private void Awake()
    {
        levelViews = AllLevelPosition.GetComponentsInChildren<LevelView>().ToList();

        LoadLevelSetting();
        SettupLevelView();
    }

    private void LoadLevelSetting()
    {
        for (int i = 0; i < mapInstance.levels.Count && i < levelViews.Count; i++)
        {
            LevelView levelView = levelViews[i];

            levelView.uniqIndex.Index = mapInstance.levels[i].uniqIndex;
            levelView.levelState.State = mapInstance.levels[i].state;
            levelView.levelType.myLevelType = mapInstance.levels[i].type;
        }
    }

    // Need Replace not delete object
    private void SettupLevelView()
    {
        foreach (LevelView levelView in levelViews)
        {
            if (levelView.levelState.State == LevelState.Lock)
            {
                GameObject defGenarat = Instantiate(prototype.lockView, levelView.transform);

                GameObject addGenarat = new GameObject("AdditionView");
                addGenarat.transform.SetParent(levelView.transform);
            }
            if (levelView.levelState.State == LevelState.New)
            {
                GameObject defGenarat = Instantiate(prototype.takeBaseView(levelView.levelType.myLevelType), levelView.transform);
                GameObject addGenarat = Instantiate(prototype.newView, levelView.transform);

            }
            if (levelView.levelState.State == LevelState.Open)
            {
                GameObject defGenarat = Instantiate(prototype.takeBaseView(levelView.levelType.myLevelType), levelView.transform);
                GameObject addGenarat = Instantiate(prototype.takeResultView(levelView.levelType.myLevelType), levelView.transform);
            }
        }
    }

    public IEnumerable<LevelView> GetLevels()
    {
        return levelViews;
    }
}

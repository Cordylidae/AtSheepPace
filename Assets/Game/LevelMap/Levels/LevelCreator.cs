using GameInstance;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class LevelCreator : MonoBehaviour
{
    [Space, Header("Level Positions")]
    [SerializeField] private GameObject AllLevelPosition;
    private List<LevelView> levelViews;

    [Inject]
    MapInstance mapInstance;

    [SerializeField] LevelButtonPrototype prototype;

    private void Awake()
    {
        levelViews = AllLevelPosition.GetComponentsInChildren<LevelView>().ToList();

        LoadLevelSetting();
    }

    private void LoadLevelSetting()
    {
        if (mapInstance.levels.Count < levelViews.Count) Debug.Log("Not all MapInctace fulled");
        else if (mapInstance.levels.Count > levelViews.Count) Debug.Log("MapInstance More than data");

        for (int i = 0, simpleCount = 0; i < mapInstance.levels.Count && i < levelViews.Count; i++)
        {
            LevelView levelView = levelViews[i];

            SetPrototype(levelView);

            levelView.uniqIndex.Index = mapInstance.levels[i].uniqIndex;
            levelView.levelState.State = mapInstance.levels[i].state;
            levelView.levelType.myLevelType = mapInstance.levels[i].type;

            levelView.ChangeView();

            if (levelView.levelType.myLevelType == LevelType.Simple)
            {
                Debug.Log("There");

                simpleCount++;
                levelView.GetComponentInChildren<IAnimalUniqIndex>(true).Index = simpleCount;

                SimpleLevel simpleLevel = mapInstance.levels[i] as SimpleLevel;
                if (simpleLevel != null) { simpleLevel.index = simpleCount; }
                else Debug.Log("Wrong cast");

                Debug.Log("NoThere");
            }
        }
    }

    public void SetPrototype(LevelView levelView)
    {
        if (levelView.levelButtonPrototype == null)
        {
            GameObject gameObject = Instantiate(prototype.gameObject, levelView.transform);
            levelView.levelButtonPrototype = gameObject.transform.GetComponent<LevelButtonPrototype>();
            gameObject.name = $"PrototypeView({levelView.name})";
        }
    }

    public IEnumerable<LevelView> GetLevels()
    {
        return levelViews;
    }
}

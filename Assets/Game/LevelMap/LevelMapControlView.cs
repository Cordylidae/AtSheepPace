using GameInstance;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelMapControlView : MonoBehaviour
{
    [SerializeField] private PlayerInputLevelMap playerInputLevelMap;
    [SerializeField] private List<PanelView> panels;
    [SerializeField] private AsyncSceneLoader asyncLoader;
    [Header("\"Levels GameObject\"")]
    [SerializeField] private List<GameObject> levelsObject;

    [Inject]
    MapInstance mapInstance;

    private string currentLevelSceneName;
    void Awake()
    {
        playerInputLevelMap.baseTap += TappedOnLevel;

        foreach (PanelView panel in panels)
        {
            panel.CloseTapped += ClosePanel;
            panel.StartLevelTapped += StartCurrentLevel;
        }

        LoadLevelSetting();
    }

    private void LoadLevelSetting()
    {
        for (int i = 0; i < mapInstance.levels.Count; i++)
        {
            LevelView levelView = levelsObject[i].GetComponent<LevelView>();

            levelView.uniqIndex.Index = mapInstance.levels[i].uniqIndex;
            levelView.levelState.State = mapInstance.levels[i].state;
            levelView.levelType.myLevelType = mapInstance.levels[i].type;
        }
    }

    void TappedOnLevel(BaseTapHandel tapHandel)
    {
        playerInputLevelMap.inFocus = false;

        LevelView levelView = tapHandel.GetComponentInParent<LevelView>();

        if(levelView.levelState.State == LevelState.Lock)
        {
            panels[0].gameObject.SetActive(true);
            return;
        }

        mapInstance.currentUniqIndex = levelView.uniqIndex.Index;

        switch (levelView.levelType.myLevelType)
        {
            case LevelType.Simple:
                {
                    panels[1].gameObject.SetActive(true);
                    PanelView_Simple view_Simple = panels[1] as PanelView_Simple;

                    if (view_Simple != null)
                    {
                        if (levelView.levelType.myLevelType == LevelType.Simple) Debug.Log("Mysituation");// view_Simple.ShowLevelNumber(levelView.l)
                    }
                    else Debug.Log("Wrong cast");

                    Debug.Log("&&");
                    currentLevelSceneName = GameSceneName.CoreGame;

                }
                return;
            case LevelType.Unlimited:
                {
                    panels[2].gameObject.SetActive(true);
                    currentLevelSceneName = GameSceneName.UnlimitedGame;
                }
                return;
            case LevelType.Tutorial:
                {
                    panels[3].gameObject.SetActive(true);
                    currentLevelSceneName = GameSceneName.TutorialGame;
                }
                return;
        }

        throw new NotImplementedException();
    }

    void ClosePanel()
    {
        foreach (PanelView panel in panels)
        {
            panel.gameObject.SetActive(false);
        }

        playerInputLevelMap.inFocus = true;
    }

    void StartCurrentLevel()
    {
        Debug.Log("Start Level");

        asyncLoader.LoadAsync(currentLevelSceneName);
    }


    public void ResetSubscribtion()
    {
        playerInputLevelMap.baseTap -= TappedOnLevel;

        foreach (PanelView panel in panels)
        {
            panel.CloseTapped -= ClosePanel;
            panel.StartLevelTapped -= StartCurrentLevel;
        }
    }
    private void OnDestroy()
    {
        ResetSubscribtion();
    }
}

public class LevelMapControl
{
    List<LevelInstance> levelsList = new List<LevelInstance>();
}

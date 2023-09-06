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

    [SerializeField] private LevelCreator levelCreator;

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
                    SimpleLevel simpleLevel = mapInstance.levels[mapInstance.currentUniqIndex] as SimpleLevel;

                    if (view_Simple != null && simpleLevel != null)
                    {
                        if (levelView.levelType.myLevelType == LevelType.Simple) view_Simple.ShowLevelNumber(simpleLevel.index);
                    }
                    else Debug.Log("Wrong cast");

                    currentLevelSceneName = GameSceneName.CoreGame;

                }
                return;
            case LevelType.Unlimited:
                {
                    panels[2].gameObject.SetActive(true);
                    currentLevelSceneName = GameSceneName.CoreGame;
                }
                return;
            case LevelType.Tutorial:
                {
                    panels[3].gameObject.SetActive(true);
                    currentLevelSceneName = GameSceneName.CoreGame;
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

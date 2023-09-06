using PlasticGui.WorkspaceWindow.QueryViews.Changesets;
using System;
using UnityEngine;

public class LevelButtonPrototype : MonoBehaviour
{
    [SerializeField] public GameObject lockView;
    [SerializeField] public GameObject newView;

    [Header("Simple")]
    [SerializeField] public GameObject simpleResultView;
    [SerializeField] public GameObject simpleBaseView;

    [Header("Unlimit")]
    [SerializeField] public GameObject unlimitResultView;
    [SerializeField] public GameObject unlimitBaseView;

    [Header("Tutorial")]
    [SerializeField] public GameObject tutorialResultView;
    [SerializeField] public GameObject tutorialBaseView;

    public void setActiveBase(LevelType levelType)
    {
        switch (levelType)
        {
            case LevelType.Simple: simpleBaseView.SetActive(true); break;
            case LevelType.Unlimited: unlimitBaseView.SetActive(true); break;
            case LevelType.Tutorial: tutorialBaseView.SetActive(true); break;
        }
    }

    public void setActiveResult(LevelType levelType)
    {
        switch (levelType)
        {
            case LevelType.Simple: simpleResultView.SetActive(true); break;
            case LevelType.Unlimited: unlimitResultView.SetActive(true); break;
            case LevelType.Tutorial: tutorialResultView.SetActive(true); break;
        }
    }

    public void DisableAll()
    {
        lockView.SetActive(false);
        newView.SetActive(false);
        
        simpleResultView.SetActive(false);
        simpleBaseView.SetActive(false);
        
        unlimitResultView.SetActive(false);
        unlimitBaseView.SetActive(false);

        tutorialResultView.SetActive(false);
        tutorialBaseView.SetActive(false);
    }
}

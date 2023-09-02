using System;
using System.Collections;
using System.Collections.Generic;
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

    public GameObject takeBaseView(LevelType levelType)
    {
        switch (levelType)
        {
            case LevelType.Simple: return simpleBaseView;
            case LevelType.Unlimited: return unlimitBaseView;
            case LevelType.Tutorial: return tutorialBaseView;
        }
        throw new NotImplementedException();
    }

    public GameObject takeResultView(LevelType levelType)
    {
        switch (levelType)
        {
            case LevelType.Simple: return simpleResultView;
            case LevelType.Unlimited: return unlimitResultView;
            case LevelType.Tutorial: return tutorialResultView;
        }
        throw new NotImplementedException();
    }
}

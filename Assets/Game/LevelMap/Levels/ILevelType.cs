using System;
using UnityEngine;

public class ILevelType : MonoBehaviour
{
    [SerializeField] private LevelType levelType;

    [SerializeField] private GameObject Simple;
    [SerializeField] private GameObject Unlimited;
    [SerializeField] private GameObject Tutorial;

    public LevelType myLevelType
    {
        set
        {
            levelType = value;
            SetState();
        }
        get { return levelType; }
    }

    public void SetState()
    {
        switch (levelType)
        {
            case LevelType.Simple:
                {
                    Simple.SetActive(true);
                    Unlimited.SetActive(false);
                    Tutorial.SetActive(false);
                }
                return;
            case LevelType.Unlimited:
                {
                    Simple.SetActive(false);
                    Unlimited.SetActive(true);
                    Tutorial.SetActive(false);
                }
                return;
            case LevelType.Tutorial:
                {
                    Simple.SetActive(false);
                    Unlimited.SetActive(false);
                    Tutorial.SetActive(true);
                }
                return;
        }

        throw new NotImplementedException();
    }

    private void OnValidate()
    {
        SetState();
    }
}

public enum LevelType
{
   Simple, Unlimited, Tutorial
};
using System;
using UnityEngine;

public class ILevelType : MonoBehaviour
{
    [SerializeField] private LevelType levelType;

    public Action ChangedState;
    public LevelType myLevelType
    {
        set
        {
            levelType = value;
            ChangedState?.Invoke();
        }
        get { return levelType; }
    }

    private void OnValidate()
    {
        ChangedState?.Invoke();
    }
}

public enum LevelType
{
   Simple, Unlimited, Tutorial
};
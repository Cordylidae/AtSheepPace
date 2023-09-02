using System;
using UnityEngine;

public class ILevelType : MonoBehaviour
{
    [SerializeField] private LevelType levelType;

    public LevelType myLevelType
    {
        set
        {
            levelType = value;
        }
        get { return levelType; }
    }
}

public enum LevelType
{
   Simple, Unlimited, Tutorial
};
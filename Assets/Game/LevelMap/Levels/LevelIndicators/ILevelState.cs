using System;
using System.Collections.Generic;
using UnityEngine;

public class ILevelState : MonoBehaviour
{
    [SerializeField] private LevelState state;
    public LevelState State
    {
        set
        {
            state = value;
        }
        get { return state; }
    }
}

public enum LevelState { Lock, New, Open};

using System;
using UnityEngine;

public class ILevelState : MonoBehaviour
{
    [SerializeField] private LevelState state;
    public Action ChangedState;
    public LevelState State
    {
        set
        {
            state = value;
            ChangedState?.Invoke();
        }
        get { return state; }
    }

    private void OnValidate()
    {
        ChangedState?.Invoke();
    }
}

public enum LevelState { Lock, New, Open};

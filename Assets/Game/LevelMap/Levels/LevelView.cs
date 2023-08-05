using System;
using UnityEngine;

public class LevelView : MonoBehaviour
{
    public ILevelState levelState;
    public ILevelType levelType;
  
    void Awake()
    {
       ChangeView();
    }
    
    void ChangeView()
    {
        ChangeState();
        ChangeType();
    }

    void ChangeState()
    {
        levelState.SetState();
    }

    void ChangeType()
    {
        levelType.SetState();
    }
}

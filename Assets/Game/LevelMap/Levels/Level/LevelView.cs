using System;
using UnityEngine;

[ExecuteAlways]
public class LevelView : MonoBehaviour
{
    public ILevelState levelState;
    public ILevelType levelType;
    public IUniqIndex uniqIndex;

    public LevelButtonPrototype levelButtonPrototype;

    void Awake()
    {
        levelState.ChangedState += ChangeView;
        levelType.ChangedState += ChangeView;
    }

    public void ChangeView()
    {
        if (levelButtonPrototype == null) { Debug.Log("Empty prottotype view " + this.name); return; }
    
        SetByState();
    }

    private void SetByState()
    {
        levelButtonPrototype.DisableAll();

        if (levelState.State == LevelState.Lock)
        {
            levelButtonPrototype.lockView.SetActive(true);
        }
        if (levelState.State == LevelState.New)
        {
            levelButtonPrototype.setActiveBase(levelType.myLevelType);
            levelButtonPrototype.newView.SetActive(true);
        }
        if (levelState.State == LevelState.Open)
        {
            levelButtonPrototype.setActiveBase(levelType.myLevelType);
            levelButtonPrototype.setActiveResult(levelType.myLevelType);
        }
    }

    private void OnDestroy()
    {
        levelState.ChangedState -= ChangeView;
        levelType.ChangedState -= ChangeView;
    }
}

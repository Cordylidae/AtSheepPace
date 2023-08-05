using System;
using System.Collections.Generic;
using UnityEngine;

public class ILevelState : MonoBehaviour
{
    [SerializeField] private LevelState state;

    [SerializeField] private GameObject lockBlock;
    [SerializeField] private GameObject newBlock;
    [SerializeField] private GameObject openBlock;

    [SerializeField] private List<GameObject> openSubBlock;

    public LevelState State
    {
        set
        {
            state = value;
            SetState();
        }
        get { return state; }
    }

    public void SetState()
    {
        switch (state)
        {
            case LevelState.Lock:
                {
                    newBlock.SetActive(false);
                    lockBlock.SetActive(true);
                    openBlock.SetActive(false);

                    foreach (GameObject go in openSubBlock)
                    {
                        go.SetActive(false);
                    }
                }
                return;
            case LevelState.New:
                {
                    newBlock.SetActive(true);
                    lockBlock.SetActive(false);
                    openBlock.SetActive(true);

                    foreach (GameObject go in openSubBlock)
                    {
                        go.SetActive(false);
                    }
                }
                return;
            case LevelState.Open:
                {
                    newBlock.SetActive(false);
                    lockBlock.SetActive(false);
                    openBlock.SetActive(true);

                    foreach (GameObject go in openSubBlock)
                    {
                        go.SetActive(true);
                    }
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

public enum LevelState { Lock, New, Open};

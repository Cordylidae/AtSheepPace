using GameInstance;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelMapControlView : MonoBehaviour
{
    [SerializeField] private PlayerInputLevelMap playerInputLevelMap;
    [SerializeField] private List<GameObject> panels;
    
    void Awake()
    {
        playerInputLevelMap.baseTap += TappedOnLevel;
    }

    void TappedOnLevel(BaseTapHandel tapHandel)
    {
        LevelView levelTapped = tapHandel.GetComponentInParent<LevelView>();

        switch (levelTapped.levelType.myLevelType)
        {
            case LevelType.Simple:
            { 

            }return;
            case LevelType.Unlimited:
            {

            }return;
            case LevelType.Tutorial:
            {

            }return;
        }

        throw new NotImplementedException();
    }


    public void ResetSubscribtion()
    {
        playerInputLevelMap.baseTap -= TappedOnLevel;
    }
    private void OnDestroy()
    {
        ResetSubscribtion();
    }
}

public class LevelMapControl
{
    List<LevelInstance> levelsList = new List<LevelInstance>();


}

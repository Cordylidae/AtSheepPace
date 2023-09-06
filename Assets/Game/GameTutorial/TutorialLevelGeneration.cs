using GameInstance;
using LevelSettings;
using System;
using TMPro;
using UnityEngine;
using Zenject;

public class TutorialLevelGeneration : MonoBehaviour
{
    [SerializeField] private GameObject TutorialGameCanvas;
    [SerializeField] private TextMeshProUGUI nameObject;

    [Inject] PreloadLevelInstance preloadLevelInstance;

    public Action Win;
    public Action Lose;

    public void myAwake()
    {
        TutorialGameCanvas.SetActive(true);
    }

    public void myStart()
    {
        TutorialSettingsModel model = preloadLevelInstance.settings_tutorial;

        if (model != null)
        {
            if (model.stateEffect) nameObject.text = settingName(model.effect);
            else nameObject.text = settingName(model.animal);
        }
        else Debug.Log("Null settings");
    }

    private string settingName(LevelEffects levelEffect)
    {
        switch (levelEffect)
        {
            case LevelEffects.FEAR_BAR:
                return "Fear Bar";
        }

        throw new NotImplementedException();
    }

    private string settingName(LevelAnimal levelAnimal)
    {
        switch (levelAnimal)
        {
            case LevelAnimal.Sheep:
                return "Sheep";
            case LevelAnimal.Wolf:
                return "Wolf";
        }

        throw new NotImplementedException();
    }
}

using GameInstance;
using LevelSettings;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TutorialLevelGeneration : MonoBehaviour
{
    [SerializeField] private GameObject TutorialGameCanvas;
    [SerializeField] private TextMeshProUGUI nameObject;
    [SerializeField] private Image iconImage;

    [SerializeField] private SubElements tutorialIcon;


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

        settingDescription(model.tutorialObject);
    }

    private void settingDescription(TutorialObject tutorialObject)
    {
        iconImage.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        switch (tutorialObject)
        {
            case TutorialObject.Sheep:
                {
                    nameObject.text = "Sheep";
                    iconImage.sprite = tutorialIcon.sprites[0];
                    iconImage.SetNativeSize();
                    iconImage.transform.localScale = new Vector3(2.5f, 2.5f, 1.0f);
                }
                break;
            case TutorialObject.FEAR_BAR:
                {
                    nameObject.text = "Fear Bar";
                    iconImage.sprite = tutorialIcon.sprites[1];
                    iconImage.SetNativeSize();
                    iconImage.transform.localScale = new Vector3(0.65f, 1.0f, 1.0f);
                }
                break;
            case TutorialObject.Wolf:
                {
                    nameObject.text = "Wolf";
                    iconImage.sprite = tutorialIcon.sprites[2];
                    iconImage.SetNativeSize();
                    iconImage.transform.localScale = new Vector3(2.5f, 2.5f, 1.0f);
                }
                break;
            default:
                {
                    nameObject.text = "Without settings";
                    iconImage.sprite = null;
                }
                break;
        }
    }
}

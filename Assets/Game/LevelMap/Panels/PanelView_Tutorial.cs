using LevelSettings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelView_Tutorial : PanelView
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image iconImage;

    [SerializeField] private SubElements tutorialIcon;
    
    public void SetTutorialDescription(TutorialObject tutorialObject)
    {
        iconImage.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        switch (tutorialObject)
        {
            case TutorialObject.Sheep:
                {
                    nameText.text = "Sheep";
                    iconImage.sprite = tutorialIcon.sprites[0];
                    iconImage.SetNativeSize();
                    iconImage.transform.localScale = new Vector3(2.5f, 2.5f, 1.0f);
                }
                break;
            case TutorialObject.FEAR_BAR:
                {
                    nameText.text = "Fear Bar";
                    iconImage.sprite = tutorialIcon.sprites[1];
                    iconImage.SetNativeSize();
                    iconImage.transform.localScale = new Vector3(0.65f, 1.0f, 1.0f);
                }
                break;
            case TutorialObject.Wolf:
                {
                    nameText.text = "Wolf";
                    iconImage.sprite = tutorialIcon.sprites[2];
                    iconImage.SetNativeSize();
                    iconImage.transform.localScale = new Vector3(2.5f, 2.5f, 1.0f);
                }
                break;
            default:
                {
                    nameText.text = "Without settings";
                    iconImage.sprite = null;
                }
                break;
        }
    }

}

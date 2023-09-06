using TMPro;
using UnityEngine;

public class PanelView_Simple : PanelView
{
    [SerializeField] TextMeshProUGUI headerText;
    public void ShowLevelNumber(int index)
    {
        headerText.text = "Level " + (index).ToString();
    }
}

using GameInstance;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class Statistics : MonoBehaviour
{
    [SerializeField] private GameObject panelStatistic;
    [SerializeField] private TextMeshProUGUI panelText;

    private bool show = false;


    //PlayerInstance playerInstance;

    [Inject]
    PlayerInstance playerInstance;
    //public void Constructor(PlayerInstance _playerInstance)
    //{
    //    playerInstance = _playerInstance;
    //    Debug.Log(SceneManager.GetActiveScene().name);
    //}


    public void OnClick()
    {
        show = !show;

        if(show) ShowPanel();
        else HidePanel();
    }

    private void ShowPanel()
    {
        panelStatistic.SetActive(true);

        panelText.text = $"Total Progres: {playerInstance.TotalProgress}";
       
        playerInstance.TotalProgress++;
    }

    private void HidePanel()
    {
        panelStatistic.SetActive(false);
    }
}

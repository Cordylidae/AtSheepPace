using GameInstance;
using System;
using UnityEngine;
using Zenject;

public class ResultConfig : MonoBehaviour
{
    [SerializeField] private GameObject Win_Panel;
    [SerializeField] private GameObject Lose_Panel;

    [Inject]
    LevelResultInctance levelResultInctance;

    private void Awake()
    {
        Win_Panel.SetActive(false);
        Lose_Panel.SetActive(false);

        switch (levelResultInctance.status) {
            case LevelResultInctance.Status.Win:
                Win_Panel.SetActive(true);
                break;
            case LevelResultInctance.Status.Lose:
                Lose_Panel.SetActive(true);
                break;
            case LevelResultInctance.Status.None:
                throw new NotImplementedException("Haven't status of results");
        }

        LevelResultsReset();
    }

    void LevelResultsReset()
    {
        levelResultInctance.status = LevelResultInctance.Status.None;
    }
}

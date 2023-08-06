using GameInstance;
using UnityEngine;
using Zenject;

// # TODO Splite UI and Contrpled things

public class GameCoreGameplay : MonoBehaviour
{
    // Control
    [SerializeField] private LevelGeneration levelGeneration;

    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    [Inject]
    MapInstance mapInstance;

    public void Awake()
    {
        levelGeneration.Win += Win;
        levelGeneration.Lose += Lose;

        if (mapInstance.levels[mapInstance.currentUniqIndex].type == LevelType.Simple) levelGeneration.myAwake();
        else Win();
    }

    public void Start()
    {
        if (mapInstance.levels[mapInstance.currentUniqIndex].type == LevelType.Simple) levelGeneration.myStart();
    }

    void Win()
    { 
        mapInstance.CompleteLevel();
        winPanel.SetActive(true);
    }

    void Lose()
    {
        losePanel.SetActive(true);
    }
}

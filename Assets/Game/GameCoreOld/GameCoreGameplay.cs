using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// # TODO Splite UI and Contrpled things

public class GameCoreGameplay : MonoBehaviour
{
    // Control
    [SerializeField] private SpawnControlAnimals spawnControlAnimals;

    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    public void Awake()
    {
        spawnControlAnimals.Win += Win;
        spawnControlAnimals.Lose += Lose;
    }

    public void Start()
    {
        spawnControlAnimals.PreStartGenerate();
        spawnControlAnimals.StartGenerate();
    }

    void Update()
    {
        
    }

    void Win()
    { 
        spawnControlAnimals.ClearAllShots();

        winPanel.SetActive(true);
    }

    void Lose()
    {
        spawnControlAnimals.ClearAllShots();

        losePanel.SetActive(true);
    }
}

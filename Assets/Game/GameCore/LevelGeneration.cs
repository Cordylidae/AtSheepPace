using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    struct SpawnHardProperties
    {
        public List<string> baseElementsAtLevel;
        public List<string> additionElementsAtLevel;

        public int minElementsOnCloseElement;
        public int maxElementsOnCloseElement;

        public int CountOfBaseElements;
    };

    SpawnHardProperties hardProperties = new SpawnHardProperties();

    List<GamburgerElement> gamburgerElements = new List<GamburgerElement>();

    List<Round> rounds = new List<Round>();

    [SerializeField] private RoundGeneration roundGeneration;

    void Awake()
    {
        SetHardProperties();

        PreStartLevel();
    }

    void SetHardProperties()
    {
        hardProperties.CountOfBaseElements = 14;

        hardProperties.baseElementsAtLevel = new List<string> { AnimalType.Sheep, AnimalType.Wolf };
        hardProperties.additionElementsAtLevel = new List<string> { AnimalType.Deer, AnimalType.Boar, AnimalType.Hedgehog };

        hardProperties.minElementsOnCloseElement = 1;
        hardProperties.maxElementsOnCloseElement = 3;
    }

    void PreStartLevel()
    {
        SetBaseElementsList();
        SetRounds();
    }

    // Set count of Wolves and Sheeps and set their sequence 
    void SetBaseElementsList()
    {
        //int allCount = hardProperties.CountOfBaseElements;

        //int allSheep = 8; //#### NEED Initialise
        //int allWolves = allCount - allSheep;

        // Genereate sequence
        List<string> elements = new List<string>
        {
            AnimalType.Sheep, AnimalType.Wolf,
            AnimalType.Sheep, AnimalType.Wolf,
            AnimalType.Sheep, AnimalType.Wolf,
            AnimalType.Sheep, AnimalType.Wolf,
            AnimalType.Sheep, AnimalType.Wolf, 
            AnimalType.Wolf,  AnimalType.Sheep, 
            AnimalType.Sheep, AnimalType.Sheep,
            AnimalType.Sheep, AnimalType.Wolf,
            AnimalType.Sheep, AnimalType.Wolf,
            AnimalType.Sheep, AnimalType.Wolf,
            AnimalType.Sheep, AnimalType.Wolf,
            AnimalType.Sheep, AnimalType.Wolf,
            AnimalType.Wolf,  AnimalType.Sheep,
            AnimalType.Sheep, AnimalType.Sheep
        };

        List<int> closeElementIndex = new List<int>
        {
            0, 5, 6, 7, 9, 12, 13, 14, 16, 19, 20, 21, 23, 24, 25,
        };

        GamburgerElement gamburgerElement;

        // Initialize baseElements
        for (int i = 0, j = 0, z = 0; i < elements.Count; i++)
        {
            if (elements[i] == AnimalType.Sheep) { j++;}
            gamburgerElement = new GamburgerElement(new BaseElement(elements[i], j));

            if (closeElementIndex[z] == i)
            {
                gamburgerElement.baseE.IsOpen = false;

                gamburgerElement.additionE = SetAdditionalElements();
                z = z + 1 < closeElementIndex.Count ? z+1 : z;
            }

            gamburgerElements.Add(gamburgerElement);
        }
    }

    List<AdditionalElement> SetAdditionalElements()
    {
        // Genereate sequence
        List<string> elements = new List<string>
        {
            AnimalType.Deer, AnimalType.Boar, AnimalType.Hedgehog
        };

        List<AdditionalElement> additionalElements = new List<AdditionalElement>();

        int randCount = 3;// UnityEngine.Random.RandomRange(1, elements.Count);

        // Initialize additionalElements
        for (int i = 0; i < randCount; i++)
        {
            additionalElements.Add(new AdditionalElement(elements[i]));
        }
        if(additionalElements.Count != 0) additionalElements.Last().IsOpen = true;

        return additionalElements;
    }

    void SetRounds()
    {
        rounds = new List<Round>
        {
            new Round(gamburgerElements.GetRange(0,4)),
            new Round(gamburgerElements.GetRange(4,2)),
            new Round(gamburgerElements.GetRange(6,2)),
            new Round(gamburgerElements.GetRange(8,4)),
            new Round(gamburgerElements.GetRange(12,2))
        };

        foreach (Round round in rounds)
        {
            round.zeroElements += NextRound;
        }
    }
    void Start()
    {
        Debug.Log("-------Starts level-------");
        StartLevel();
    }

    void StartLevel()
    {
        ShowRound();
    }

    void EndLevel()
    {
        Debug.Log("-------Finish level-------");
    }

    void ShowRound()
    {
        Debug.Log($"-------Round Start #{rounds.Count}-------");

        Round round = rounds.First();

        roundGeneration.currentRound = round;

        roundGeneration.PreStartRound();
    }

    void NextRound()
    {
        Debug.Log($"-------Round End #{rounds.Count}-------");

        Round round = rounds.First();

        round.zeroElements -= NextRound;

        rounds.Remove(round);

        if (rounds.Count == 0) EndLevel(); // finish level
        else ShowRound();
    }
}

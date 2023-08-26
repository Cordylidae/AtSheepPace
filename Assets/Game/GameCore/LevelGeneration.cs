using GameInstance;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

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
    [SerializeField] private PlayerInput playerInput;

    // ### NEED Initialaize like INJECT
    [SerializeField] private Sun_Moon_View sun_moon_View;

    // ### NEED Initialaize like INJECT
    [SerializeField] private FearBarView fearBarView;

    public Action Win;
    public Action Lose;

    public void myAwake()
    {
        fearBarView.fearBar.fullFearBar += LoseLevel;

        SetHardProperties();

        PreStartLevel();
    }

    void SetHardProperties()
    {
        hardProperties.CountOfBaseElements = 12;

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
            //AnimalType.Sheep,
            //AnimalType.Sheep,
            
            //AnimalType.Sheep,
            //AnimalType.Sheep,
            //AnimalType.Sheep,
            //AnimalType.Sheep,

            //AnimalType.Sheep,
            //AnimalType.Sheep,
            //AnimalType.Sheep,
            //AnimalType.Sheep
            AnimalType.Sheep, AnimalType.Wolf,
            AnimalType.Sheep, AnimalType.Wolf,
            AnimalType.Wolf, AnimalType.Wolf,
            AnimalType.Sheep, AnimalType.Sheep,
            AnimalType.Sheep, AnimalType.Wolf,
            //AnimalType.Wolf,  AnimalType.Sheep,
            //AnimalType.Sheep, AnimalType.Sheep,
            //AnimalType.Sheep, AnimalType.Wolf,
            //AnimalType.Sheep, AnimalType.Wolf,
            //AnimalType.Sheep, AnimalType.Wolf,
            //AnimalType.Sheep, AnimalType.Wolf,
            //AnimalType.Sheep, AnimalType.Wolf,
            //AnimalType.Wolf,  AnimalType.Sheep,
            //AnimalType.Sheep, AnimalType.Sheep,
            //AnimalType.Wolf,  AnimalType.Wolf,
            //AnimalType.Sheep, AnimalType.Wolf,
            //AnimalType.Sheep, AnimalType.Wolf,
        };

        List<int> closeElementIndex = new List<int>
        {
            0, 4, 6, 8, 9, 12, 13, 14, 16, 19, 20, //21, 23, 24, 25,
        };

        GamburgerElement gamburgerElement;

        // Initialize baseElements
        for (int i = 0, j = 0, z = 0; i < elements.Count; i++)
        {
            if (elements[i] == AnimalType.Sheep) { j++;}
            gamburgerElement = new GamburgerElement(new BaseElement(elements[i], j));

            if (closeElementIndex.Count > 0 && closeElementIndex[z] == i)
            {
                gamburgerElement.baseE.IsOpen = false;

                gamburgerElement.additionE = SetAdditionalElements();
                z = z + 1 < closeElementIndex.Count ? z + 1 : z;
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
            //new Round(gamburgerElements.GetRange(0,2))

            new Round(gamburgerElements.GetRange(0,4)),
            new Round(gamburgerElements.GetRange(4,2)),
            new Round(gamburgerElements.GetRange(6,4)),
            //new Round(gamburgerElements.GetRange(8,3)),
            //new Round(gamburgerElements.GetRange(11,5)),
            //new Round(gamburgerElements.GetRange(16,5))
        };

        foreach (Round round in rounds)
        {
            round.zeroElements += NextRound;
        }
    }
    private RoundControl roundControl;

    private void SetUpAnimalRules()
    {
        roundControl = new RoundControl(
            0,
            fearBarView.fearBar,
            sun_moon_View);

        roundControl.SubscribeBaseTap(playerInput);
    }

    private void UpdateAnimalRules(int index)
    {
        roundControl.indexOfCurrentButtons = index;
    }

    public void myStart()
    {
        StartLevel();
    }

    void StartLevel()
    {
        Debug.Log("-------Starts level-------");

        SetUpAnimalRules();

        ShowRound();
    }
    void LoseLevel()
    {
        Lose?.Invoke();
    }
    void EndLevel()
    {
        Debug.Log("-------Finish level-------");

        roundControl.UnSubscribeBaseTap(playerInput);

        Win?.Invoke();
    }

    void ShowRound()
    {
        Debug.Log($"-------Round Start #{rounds.Count}-------");

        Round round = rounds.First();
        UpdateAnimalRules(round.elementsDictionary.First().Key.number);

        roundGeneration.currentRound = round;
        roundGeneration.PreStartRound(roundControl);
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

    private void OnDestroy()
    {
        fearBarView.fearBar.fullFearBar -= LoseLevel;
    }
}

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

    List<BaseElement> baseElements = new List<BaseElement>();
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
        int allCount = hardProperties.CountOfBaseElements;

        int allSheep = 8; //#### NEED Initialise
        int allWolves = allCount - allSheep;

        // Genereate sequence
        List<string> elements = new List<string>
        {
            AnimalType.Sheep, AnimalType.Wolf,
            AnimalType.Sheep, AnimalType.Sheep,
            AnimalType.Wolf, AnimalType.Sheep,
            AnimalType.Wolf, AnimalType.Sheep,
            AnimalType.Wolf,
            AnimalType.Sheep, AnimalType.Sheep,
            AnimalType.Sheep, AnimalType.Wolf,
            AnimalType.Sheep

        };

        // Initialize baseElements
        for (int i = 0, j = 0; i < elements.Count; i++)
        {
            if (elements[i] == AnimalType.Sheep) { j++; }
            baseElements.Add(new BaseElement(elements[i], j));
        }
    }

    void SetRounds()
    {
        rounds = new List<Round>
        {
            new Round(baseElements.GetRange(0,4)),
            new Round(baseElements.GetRange(4,2)),
            new Round(baseElements.GetRange(6,2)),
            new Round(baseElements.GetRange(8,4)),
            new Round(baseElements.GetRange(12,2))
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

public class BaseElement
{
    public readonly string animalType;
    public readonly int number;
    bool isOpen = true;

    public BaseElement(string type, int num)
    {
        animalType = type;
        number = num;
    }
}

public class AdditionalElement
{
    public readonly string animalType;
    int number;
    bool isOpen = true;

    public AdditionalElement(string type, int num)
    {
        animalType = type;
        number = num;
    }
}
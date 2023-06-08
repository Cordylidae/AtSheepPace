using System.Collections.Generic;
using System;
using UnityEngine;

public class RoundControl
{
    public class CorrectTapState
    {
        public const string UncorrectUndestroy = "UncorrectUndestroy";
        public const string UncorrectDestroy = "UncorrectDestroy";

        public const string CorrectUndestroy = "CorrectUndestroy";
        public const string CorrectDestroy = "CorrectDestroy";
    };

    public int indexOfCurrentButtons;
    public List<BaseElement> currentIndexElements;

    public FearBar fearBar;

    public string OnButtonTap(string animalType, int index)
    {
        switch (animalType)
        {
            case AnimalType.Sheep:
                {
                    if (index - 1 <= indexOfCurrentButtons)
                    {
                        fearBar.SheepGood();
                        return CorrectTapState.CorrectDestroy;
                    }
                    else
                    {
                        fearBar.SheepBad();
                        return CorrectTapState.UncorrectUndestroy;
                    }
                }
            case AnimalType.Wolf:
                {
                    fearBar.WolfBad();

                    return CorrectTapState.UncorrectDestroy;
                }
        }

        throw new NotImplementedException();
    }

    public string GoneButtonRadius(string animalType)
    {
        switch (animalType)
        {
            case AnimalType.Sheep:
                {
                    fearBar.SheepBad();

                    return CorrectTapState.UncorrectDestroy;
                }
            case AnimalType.Wolf:
                {
                    fearBar.WolfGood();

                    return CorrectTapState.CorrectDestroy;
                }
        }

        throw new NotImplementedException();
    }
}

public class Round
{
    public Action zeroElements;

    public readonly Dictionary<BaseElement, List<AdditionalElement>> elementsDictionary;

    public Round(List<GamburgerElement> _gamburgerElement)
    {
        elementsDictionary = new Dictionary<BaseElement, List<AdditionalElement>>();

        foreach (GamburgerElement element in _gamburgerElement)
        {
            elementsDictionary.Add(element.key, element.value);
        }
    }

    public bool checkEmptyBaseKey(BaseElement key)
    {
        if (elementsDictionary[key].Count == 0) return true;
        return false;
    }

    public void checkEmptyDictionary()
    {
        if (elementsDictionary.Count == 0) zeroElements?.Invoke();
    }

}


using System.Collections.Generic;
using System;
using UnityEngine;

public class RoundControl
{
    private class CorrectTapState
    {
        public const string UncorrectUndestroy = "UncorrectUndestroy";
        public const string UncorrectDestroy = "UncorrectDestroy";

        public const string CorrectUndestroy = "CorrectUndestroy";
        public const string CorrectDestroy = "CorrectDestroy";
    };

    public int indexOfCurrentButtons;
    public List<BaseElement> currentIndexElements;

    public FearBar fearBar;

    private string OnButtonTap(string animalType, int index)
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

    public void GoneButtonRadius(string animalType)
    {
        switch (animalType)
        {
            case AnimalType.Sheep:
                {
                    fearBar.SheepBad();

                    return;
                }
            case AnimalType.Wolf:
                {
                    fearBar.WolfGood();

                    return;
                }
        }
    }

    public bool IsTappedButtonDestroy(string animalType, int index)
    {
        switch (OnButtonTap(animalType, index))
        {
            case CorrectTapState.UncorrectUndestroy: return false;
            case CorrectTapState.UncorrectDestroy: return true;
            case CorrectTapState.CorrectUndestroy: return false;
            case CorrectTapState.CorrectDestroy: return true;
        }

        throw new NotImplementedException();
    }
}

public class Round
{
    public Action zeroElements;
    public Action zeroElementsOfBaseKey;

    public readonly Dictionary<BaseElement, List<AdditionalElement>> elementsDictionary;

    public Round(List<BaseElement> _baseElements)
    {
        elementsDictionary = new Dictionary<BaseElement, List<AdditionalElement>>();

        foreach (BaseElement baseElement in _baseElements)
        {
            elementsDictionary.Add(baseElement, new List<AdditionalElement>());
        }
    }

    public void checkEmptyBaseKey(BaseElement key)
    {
        if (elementsDictionary[key].Count == 0) zeroElementsOfBaseKey?.Invoke();
    }

    public void checkEmptyDictionary()
    {
        if (elementsDictionary.Count == 0) zeroElements?.Invoke();
    }

}


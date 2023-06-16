using System.Collections.Generic;
using System;
using UnityEngine;

// ### NEED Add Animal Rules Class

public class Rule
{
    public class DayTime
    {
        public const string Sun = "Sun";
        public const string Moon = "Moon";
    }

    public string dayTime { set; get; }
};

public class RoundControl
{
    public class CorrectTapState
    {
        public const string UncorrectUndestroy = "UncorrectUndestroy";
        public const string UncorrectDestroy = "UncorrectDestroy";

        public const string CorrectUndestroy = "CorrectUndestroy";
        public const string CorrectDestroy = "CorrectDestroy";
    };

    public Rule rule = new Rule();

    public int indexOfCurrentButtons;
    public List<BaseElement> currentIndexElements;

    public FearBar fearBar;

    public RoundControl(int firstIndex, FearBar fBar, string dayTime = Rule.DayTime.Sun)
    {
        indexOfCurrentButtons = firstIndex;
        fearBar = fBar;
        currentIndexElements = new List<BaseElement>();

        rule.dayTime = dayTime;
    }

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
    public string OnButtonTap(string animalType, SignState signState)
    {
        switch (animalType)
        {
            case AnimalType.Deer:
                {
                    switch (signState)
                    {
                        case SignState.True:
                            {
                                if (rule.dayTime == Rule.DayTime.Sun)
                                {
                                    return CorrectTapState.CorrectUndestroy;
                                }
                                else
                                {
                                    fearBar.DeerBad(rule.dayTime);
                                    return CorrectTapState.UncorrectDestroy;
                                }
                            }
                        case SignState.False:
                            {
                                if (rule.dayTime == Rule.DayTime.Moon)
                                {
                                    return CorrectTapState.CorrectUndestroy;
                                }
                                else
                                {
                                    fearBar.DeerBad(rule.dayTime);
                                    return CorrectTapState.UncorrectDestroy;
                                }
                            }
                    }

                    throw new NotImplementedException();
                }
        }

        throw new NotImplementedException();
    }
}

public class Round
{
    public Action zeroElements;

    //public readonly Dictionary<BaseElement, List<AdditionalElement>> elementsDictionary;
    public readonly Dictionary<BaseElement, GamburgerElement> elementsDictionary;

    public Round(List<GamburgerElement> _gamburgerElement)
    {
        elementsDictionary = new Dictionary<BaseElement, GamburgerElement>();

        foreach (GamburgerElement element in _gamburgerElement)
        {
            elementsDictionary.Add(element.baseE, element);
        }
    }

    public void checkEmptyDictionary()
    {
        if (elementsDictionary.Count == 0) zeroElements?.Invoke();
    }

}
public struct PairAnimalView
{
    public GameObject animal;
    public ButtonView view;

    public PairAnimalView(GameObject _animal, ButtonView _view)
    {
        animal = _animal;
        view = _view;
    }
}

public class GamburgerAnimalGroup
{
    public GamburgerElement gamburgerElement;

    public GameObject baseParentObject;

    public PairAnimalView baseObject;
    public List<PairAnimalView> additionObjects;

    public GamburgerAnimalGroup(GamburgerElement element)
    {
        gamburgerElement = element;
        additionObjects = new List<PairAnimalView>();
    }

    public void SetParentObject(Transform transform)
    {
        baseParentObject = new GameObject($"BaseObject #{gamburgerElement.baseE.animalType}-{gamburgerElement.baseE.number}");
        baseParentObject.transform.SetParent(transform);
    }

    public void SetBaseObject(GameObject gameObject, int index, int countBaseElement)
    {
        baseObject.animal = gameObject;
        baseObject.animal.transform.SetParent(baseParentObject.transform);

        baseObject.animal.transform.localPosition = new Vector3(
                            UnityEngine.Random.RandomRange(-450, 450) / 1080.0f * 6.0f,
                            UnityEngine.Random.RandomRange(-520, 520) / 1920.0f * 10.0f,
                            //0.0f, 0.0f,
                            index * (-4.0f / countBaseElement));

        SetBaseButtonView();
    }
    private void SetBaseButtonView()
    {
        baseObject.view = baseObject.animal.GetComponent<BaseButtonView>();

        BaseButtonView view = baseObject.view as BaseButtonView;
        view.AnimalUniqIndex.Index = gamburgerElement.baseE.number;

        switch (view.AnimalType.buttonType)
        {
            case AnimalType.Sheep:
                {
                    SheepView sheepView = view as SheepView;
                }
                break;
            case AnimalType.Wolf:
                {
                    WolfView wolfView = view as WolfView;
                }
                break;
        }

        SetIOpenEvent(gamburgerElement.baseE, view);
    }

    public void SetAdditionObject(GameObject gameObject, int countBaseElement, int i)
    {
        gameObject.name += $" Index : {i}";
        gameObject.transform.SetParent(baseParentObject.transform);

        gameObject.transform.localPosition = new Vector3(
                       baseObject.animal.transform.localPosition.x + UnityEngine.Random.RandomRange(-4.5f, 4.5f) / 1080.0f * 6.0f,
                       baseObject.animal.transform.localPosition.y + UnityEngine.Random.RandomRange(-5.2f, 5.2f) / 1920.0f * 10.0f,
                       //0.0f, 0.0f,
                       baseObject.animal.transform.localPosition.z + (i + 1) * (-3.9f / countBaseElement / gamburgerElement.additionE.Count));

        ButtonView buttonView = SetAdditionalButtonView(gameObject, i);
        SetIOpenEvent(gamburgerElement.additionE[i], buttonView);

        PairAnimalView additionE = new PairAnimalView(gameObject, buttonView);
        additionObjects.Add(additionE);
    }

    private ButtonView SetAdditionalButtonView(GameObject go, int i)
    {
        ButtonView buttonView = go.GetComponent<ButtonView>();

        switch (buttonView.AnimalType.buttonType)
        {
            case AnimalType.Deer:
                {
                    DeerView deerView = buttonView as DeerView;
                    deerView.AnimalNumberIndex.Index =  UnityEngine.Random.RandomRange(2, 5); // i + 1;//

                    return deerView;
                }
        }

        throw new NotImplementedException();
    }

    public bool checkEmptyBaseKey()
    {
        if (gamburgerElement.additionE.Count == 0) return true;
        return false;
    }

    public void OpenBaseElement()
    {
        BaseButtonView view = baseObject.view as BaseButtonView;

        gamburgerElement.baseE.IsOpen = true;
        view.DrawCircle.StartDrawing(2.7f);
    }

    private void SetIOpenEvent(Element element, ButtonView view)
    {
        element.IOpen += () =>
        {
            if (element.IsOpen)
            {
                view.SetOpen();
            }
            else view.SetClose();
        };

        element.IOpen?.Invoke();
    }
}

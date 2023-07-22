using System.Collections.Generic;
using System;
using UnityEngine;

// ### NEED Add Animal Rules Class

public class RoundControl
{
    public class CorrectTapState
    {
        public const string UncorrectUndestroy = "UncorrectUndestroy";
        public const string UncorrectDestroy = "UncorrectDestroy";

        public const string CorrectUndestroy = "CorrectUndestroy";
        public const string CorrectDestroy = "CorrectDestroy";
    };

    public RuleDayTime rule;

    public int indexOfCurrentButtons;
    public List<BaseElement> currentIndexElements;

    public FearBar fearBar;
    public Sun_Moon_View sun_moon_View;

    public RoundControl(int firstIndex, FearBar fBar, Sun_Moon_View smView)
    {
        indexOfCurrentButtons = firstIndex;
        fearBar = fBar;
        currentIndexElements = new List<BaseElement>();

        sun_moon_View = smView;
        rule = smView.ruleDay;
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
    public string OnButtonTap(string animalType, SignState signState, int index)
    {
        switch (animalType)
        {
            case AnimalType.Deer:
                {
                    switch (signState)
                    {
                        case SignState.True:
                            {
                                if (rule.dayTime == RuleDayTime.Time.Sun)
                                {
                                    if (index - 1 <= 0)
                                    {
                                        fearBar.DeerGood(rule.dayTime);
                                        return CorrectTapState.CorrectDestroy;
                                    }
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
                                if (rule.dayTime == RuleDayTime.Time.Moon)
                                {
                                    if (index - 1 <= 0)
                                    {
                                        fearBar.DeerGood(rule.dayTime);
                                        return CorrectTapState.CorrectDestroy;
                                    }
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
    public string OnButtonTap(string animalType, SubSignCounter answer, int index = 1)
    {
        switch (animalType)
        {
            case AnimalType.Boar:
                {
                    if (rule.dayTime == RuleDayTime.Time.Sun)
                    {
                        if (answer.Count[(int)SignState.False] >= index && 
                            answer.Count[(int)SignState.True] == 0)
                        {
                            fearBar.BoarGood(rule.dayTime);
                            return CorrectTapState.CorrectDestroy;
                        }
                        else
                        {
                            fearBar.BoarBad(rule.dayTime);
                            return CorrectTapState.UncorrectDestroy;
                        }
                    }
                    else
                    {
                        if (answer.Count[(int)SignState.True] >= index && 
                            answer.Count[(int)SignState.False] == 0)
                        {
                            fearBar.BoarGood(rule.dayTime);
                            return CorrectTapState.CorrectDestroy;
                        }
                        else
                        {
                            fearBar.BoarBad(rule.dayTime);
                            return CorrectTapState.UncorrectDestroy;
                        }
                    }
                }
        }

        throw new NotImplementedException();
    }
    public string OnButtonTap(string animalType, SubColorFormCounter answer, 
        SignState signState, FormState formState, ColorState colorState)
    {
        switch (animalType)
        {
            case AnimalType.Hedgehog:
                {
                    int form = answer.getFormCount(formState);
                    int color = answer.getColorsCount(colorState);
                    int form_color = answer.getFormAndColorCount(formState, colorState);
                    int result = form + color - form_color;

                    if (rule.dayTime == RuleDayTime.Time.Sun)
                    {
                        if (signState == SignState.True)
                        {
                            if (form_color >= 1 && form_color == answer.AllCount)
                            {
                                fearBar.HedgehogGood(rule.dayTime);
                                return CorrectTapState.CorrectDestroy;
                            }
                            else
                            {
                                fearBar.HedgehogBad(rule.dayTime);
                                return CorrectTapState.UncorrectDestroy;
                            }
                        }
                        else 
                        {
                            if (result == 0 && answer.AllCount > 0)
                            {
                                fearBar.BoarGood(rule.dayTime);
                                return CorrectTapState.CorrectDestroy;
                            }
                            else
                            {
                                fearBar.BoarBad(rule.dayTime);
                                return CorrectTapState.UncorrectDestroy;
                            }
                        }
                    }
                    else if(rule.dayTime == RuleDayTime.Time.Moon)
                    {
                        if (signState == SignState.True)
                        {
                            if (result >= 1 && result == answer.AllCount)
                            {
                                fearBar.HedgehogGood(rule.dayTime);
                                return CorrectTapState.CorrectDestroy;
                            }
                            else
                            {
                                fearBar.HedgehogBad(rule.dayTime);
                                return CorrectTapState.UncorrectDestroy;
                            }
                        }
                        else
                        {
                            if (form_color == 0 && result == answer.AllCount)
                            {
                                fearBar.HedgehogGood(rule.dayTime);
                                return CorrectTapState.CorrectDestroy;
                            }
                            else
                            {
                                fearBar.HedgehogBad(rule.dayTime);
                                return CorrectTapState.UncorrectDestroy;
                            }
                        }
                    }
                    else throw new NotImplementedException();
                }
        }

        throw new NotImplementedException();
    }

    public void SubscribeBaseTap(PlayerInput playerInput)
    {
        playerInput.baseTap += BaseTapRule;
    }

    public void UnSubscribeBaseTap(PlayerInput playerInput)
    {
        playerInput.baseTap -= BaseTapRule;
    }

    private void BaseTapRule()
    {
        rule.DecriseTimeCount();
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

    public void SetBaseObject(GameObject gameObject, Vector3 position)//, int index, int countBaseElement)
    {
        baseObject.animal = gameObject;
        baseObject.animal.transform.SetParent(baseParentObject.transform);

        baseObject.animal.transform.localPosition = position;

        //baseObject.animal.transform.localPosition = new Vector3(
        //                    UnityEngine.Random.RandomRange(-450, 450) / 1080.0f * 6.0f,
        //                    UnityEngine.Random.RandomRange(-520, 520) / 1920.0f * 10.0f,
        //                    //0.0f, 0.0f,
        //                    index * (-4.0f / countBaseElement));

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
                       baseObject.animal.transform.localPosition.x ,//+ UnityEngine.Random.RandomRange(-4.5f, 4.5f) / 1080.0f * 6.0f,
                       baseObject.animal.transform.localPosition.y ,//+ UnityEngine.Random.RandomRange(-5.2f, 5.2f) / 1920.0f * 10.0f,
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

        int randCount = UnityEngine.Random.RandomRange(0, 100);

        switch (buttonView.AnimalType.buttonType)
        {
            case AnimalType.Deer:
                {
                    DeerView deerView = buttonView as DeerView;
                   
                    if (randCount < 25)
                    {
                        deerView.AnimalNumberIndex.Index = 1;
                    }
                    else if (randCount >= 25 && randCount < 58)
                    {
                        deerView.AnimalNumberIndex.Index = 2;
                    }
                    else if (randCount >= 58 && randCount < 90)
                    {
                        deerView.AnimalNumberIndex.Index = 3;
                    }
                    else 
                    {
                        deerView.AnimalNumberIndex.Index = 4;
                    }

                    return deerView;
                }
            case AnimalType.Boar:
                {
                    BoarView boarView = buttonView as BoarView;

                    if (randCount < 25)
                    {
                        boarView.AnimalNumberIndex.Index = 1;
                    }
                    else if (randCount >= 25 && randCount < 58)
                    {
                        boarView.AnimalNumberIndex.Index = 2;
                    }
                    else if (randCount >= 58 && randCount < 90)
                    {
                        boarView.AnimalNumberIndex.Index = 3;
                    }
                    else
                    {
                        boarView.AnimalNumberIndex.Index = 4;
                    }

                    return boarView;
                }
            case AnimalType.Hedgehog:
                {
                    HedgehogView hedgehogView = buttonView as HedgehogView;

                    //if (randCount < 25)
                    //{
                    //    hedgehogView.AnimalNumberIndex.Index = 1;
                    //}
                    //else if (randCount >= 25 && randCount < 58)
                    //{
                    //    hedgehogView.AnimalNumberIndex.Index = 2;
                    //}
                    //else if (randCount >= 58 && randCount < 90)
                    //{
                    //    hedgehogView.AnimalNumberIndex.Index = 3;
                    //}
                    //else
                    //{
                    //    hedgehogView.AnimalNumberIndex.Index = 4;
                    //}

                    return hedgehogView;
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
        view.DrawCircle.StartDrawing(1.85f);
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
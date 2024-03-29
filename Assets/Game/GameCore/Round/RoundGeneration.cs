using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;

public class RoundGeneration : MonoBehaviour
{
    [SerializeField] private Entities animalsEntities;

    // ### NEED Initialaize like INJECT
    [SerializeField] private ActiveZone activeZone;

    Dictionary<BaseElement, GamburgerAnimalGroup> animalsGroupDictionary = new Dictionary<BaseElement, GamburgerAnimalGroup>();

    public Round currentRound;
    private RoundControl roundControl;

    public async void PreStartRound(RoundControl _roundControl)
    {
        roundControl = _roundControl;

        BuildUpAnimalDicionary();
        
        await ShowElements();

        StartRoundControlElements();
    }

    private void StartRoundControlElements()
    {
        UpdateRoundControlCurrentIndex();
    }

    private void BuildUpAnimalDicionary()
    {
        int index = currentRound.elementsDictionary.Count;

        foreach (KeyValuePair<BaseElement, GamburgerElement> element in currentRound.elementsDictionary)
        {
            GamburgerAnimalGroup animalGroup = new GamburgerAnimalGroup(element.Value);
            animalsGroupDictionary.Add(element.Key, animalGroup);

            animalGroup.SetParentObject(this.transform);

            animalGroup.SetBaseObject(
                SetAnimalEntities(element.Key.animalType),
                activeZone.SetBaseObjectPosition(index, currentRound.elementsDictionary.Count)
                );
            
            /// <summary> 
            /// Event on click on interective element
            /// </summary>
            
            BaseElement baseE = animalGroup.gamburgerElement.baseE;
            BaseButtonView baseView = animalGroup.baseObject.view as BaseButtonView;

            if (baseView != null)
            {
                baseView.BaseTapHandel.isTap += () =>
                {
                    baseView.DrawCircle.ResetSubscriptions();
                    checkTheCorrectTapState(roundControl.OnButtonTap(baseE.animalType, baseE.number));
                };
                baseView.DrawCircle.radiusZero += () =>
                {
                    baseView.DrawCircle.ResetSubscriptions();
                    checkTheCorrectTapState(roundControl.GoneButtonRadius(baseE.animalType));
                };
            }

            for (int i = 0; i < element.Value.additionE.Count; i++)
            {
                animalGroup.SetAdditionObject(
                    SetAnimalEntities(element.Value.additionE[i].animalType), 
                    currentRound.elementsDictionary.Count, i);

                AdditionalElement additionalE = animalGroup.gamburgerElement.additionE[i];

                int ind = i;

                if (additionalE.animalType == AnimalType.Deer)
                {
                    DeerView deerView = animalGroup.additionObjects[i].view as DeerView;

                    if (deerView != null)
                    {
                        deerView.AnimalSign.SetRandomSign(); 

                        deerView.BaseTapHandel.isTap += async () =>
                        {
                            string correctTap = roundControl.OnButtonTap(additionalE.animalType, deerView.AnimalSign.Sign, deerView.AnimalNumberIndex.Index);

                            switch (correctTap)
                            {
                                case RoundControl.CorrectTapState.UncorrectDestroy:
                                    {
                                        // shake it
                                        ResetBaseSubscriptions(additionalE, deerView);

                                        await ShakeWithAnim(animalGroup.additionObjects[ind].animal);

                                        DestroyAdditionObject(animalGroup, ind);
                                    }
                                    break;
                                case RoundControl.CorrectTapState.CorrectDestroy:
                                    {
                                        ResetBaseSubscriptions(additionalE, deerView);

                                        DestroyAdditionObject(animalGroup, ind);
                                    }
                                    break;
                                case RoundControl.CorrectTapState.UncorrectUndestroy:
                                    {

                                    }
                                    break;
                                case RoundControl.CorrectTapState.CorrectUndestroy:
                                    {
                                        deerView.AnimalNumberIndex.Index--;
                                    }
                                    break;
                            }
                        };
                    }
                    else Debug.Log("Incorrect Dynamic Cast Deer");
                }
                if (additionalE.animalType == AnimalType.Boar)
                {
                    BoarView boarView = animalGroup.additionObjects[i].view as BoarView;

                    if (boarView != null)
                    {
                        boarView.circleOfSubs.MakeSubsBoar(boarView.AnimalNumberIndex.Index); // how much right answer

                        boarView.BaseTapHandel.isTap += async () =>
                        {
                            string correctTap = roundControl.OnButtonTap(additionalE.animalType, boarView.circleOfSubs.GetAnswerBoar(), boarView.AnimalNumberIndex.Index);

                            switch (correctTap)
                            {
                                case RoundControl.CorrectTapState.UncorrectDestroy:
                                    {
                                        // shake it
                                        ResetBaseSubscriptions(additionalE, boarView);

                                        await ShakeWithAnim(animalGroup.additionObjects[ind].animal);

                                        DestroyAdditionObject(animalGroup, ind);
                                    }
                                    break;
                                case RoundControl.CorrectTapState.CorrectDestroy:
                                    {
                                        ResetBaseSubscriptions(additionalE, boarView);

                                        DestroyAdditionObject(animalGroup, ind);
                                    }
                                    break;
                                case RoundControl.CorrectTapState.UncorrectUndestroy:
                                    {

                                    }
                                    break;
                                case RoundControl.CorrectTapState.CorrectUndestroy:
                                    {
                 
                                    }
                                    break;
                            }
                        };
                    }
                    else Debug.Log("Incorrect Dynamic Cast Boar");
                }
                if (additionalE.animalType == AnimalType.Hedgehog)
                {
                    HedgehogView hedgehogView = animalGroup.additionObjects[i].view as HedgehogView;

                    if (hedgehogView != null)
                    {
                        hedgehogView.AnimalSign.SetRandomSign();
                        hedgehogView.AnimalForm.SetRandomForm();
                        hedgehogView.AnimalColor.SetRandomColor();
                        
                        hedgehogView.freeSpaceOfSubs.MakeSubsHedgehog(
                            hedgehogView.AnimalForm.Form,
                            hedgehogView.AnimalColor.myColor
                            );

                        activeZone.SetHedgehogSubsPosition(hedgehogView.freeSpaceOfSubs.subViews,
                            animalGroup.additionObjects[i].animal.transform.position
                            );

                        hedgehogView.BaseTapHandel.isTap += async () =>
                        {
                            string correctTap = roundControl.OnButtonTap(additionalE.animalType,
                                hedgehogView.freeSpaceOfSubs.GetAnswerHedgehog(),
                                hedgehogView.AnimalSign.Sign,
                                hedgehogView.AnimalForm.Form,
                                hedgehogView.AnimalColor.myColor
                                );

                            switch (correctTap)
                            {
                                case RoundControl.CorrectTapState.UncorrectDestroy:
                                    {
                                        hedgehogView.freeSpaceOfSubs.DisableLines();
                                        // shake it
                                        ResetBaseSubscriptions(additionalE, hedgehogView);

                                        await ShakeWithAnim(animalGroup.additionObjects[ind].animal);

                                        DestroyAdditionObject(animalGroup, ind);
                                    }
                                    break;
                                case RoundControl.CorrectTapState.CorrectDestroy:
                                    {
                                        hedgehogView.freeSpaceOfSubs.DisableLines();
                                        
                                        ResetBaseSubscriptions(additionalE, hedgehogView);

                                        DestroyAdditionObject(animalGroup, ind);
                                    }
                                    break;
                                case RoundControl.CorrectTapState.UncorrectUndestroy:
                                    {

                                    }
                                    break;
                                case RoundControl.CorrectTapState.CorrectUndestroy:
                                    {

                                    }
                                    break;
                            }
                        };
                    }
                    else Debug.Log("Incorrect Dynamic Cast Hedgehog");
                }
            }

            async void checkTheCorrectTapState(string state)
            {
                switch (state)
                {
                    case RoundControl.CorrectTapState.UncorrectDestroy:
                        {
                            ResetBaseSubscriptions(animalGroup.gamburgerElement.baseE, animalGroup.baseObject.view);

                            await ShakeWithAnim(animalGroup.baseObject.animal);
                            DestroyBaseObject(animalGroup);
                        }
                        break;
                    case RoundControl.CorrectTapState.CorrectDestroy:
                        {
                            ResetBaseSubscriptions(animalGroup.gamburgerElement.baseE, animalGroup.baseObject.view);
                            
                            DestroyBaseObject(animalGroup);
                        }
                        break;
                    case RoundControl.CorrectTapState.UncorrectUndestroy:
                        {
                            await ShakeWithAnim(animalGroup.baseObject.animal);
                        }
                        break;
                    case RoundControl.CorrectTapState.CorrectUndestroy:
                        {
                        }
                        break;
                }
            }

            index--;
        }
    }

    private void UpdateRoundControlCurrentIndex()
    {
        if (roundControl.currentIndexElements.Count == 0) {
            do {
                AddAllIndex(roundControl.indexOfCurrentButtons);
                if (roundControl.currentIndexElements.Count == 0) roundControl.indexOfCurrentButtons++;
            } while (roundControl.currentIndexElements.Count == 0 && currentRound.elementsDictionary.Count != 0);
            
        }

        StartOpenBaseElementsByIndex();
        StartOpenAdditionalElements();
    }

    void AddAllIndex(int indexOfStart)
    {
        foreach (KeyValuePair<BaseElement, GamburgerElement> element in currentRound.elementsDictionary)
        {
            if (roundControl.currentIndexElements.Contains(element.Key)) continue;

            if (element.Key.number == indexOfStart) {
                roundControl.currentIndexElements.Add(element.Key);
            }
            else if (element.Key.number > indexOfStart) break;
        }
    }

    void StartOpenBaseElementsByIndex()
    {
        foreach (BaseElement key in roundControl.currentIndexElements)
        {
            if (key.IsOpen)
            {
                animalsGroupDictionary[key].OpenBaseElement();
            }
            else
            {
                if (animalsGroupDictionary[key].checkEmptyBaseKey())
                {
                    animalsGroupDictionary[key].OpenBaseElement();
                }
            }
        }
    }

    // #NEED TODO to Recheck hpw it works
    void StartOpenAdditionalElements()
    {
        foreach (var element in animalsGroupDictionary)
        {
            if (animalsGroupDictionary[element.Key].checkEmptyBaseKey()) continue;

            string animalType = animalsGroupDictionary[element.Key].gamburgerElement.additionE.Last().animalType;
            var additionalE = animalsGroupDictionary[element.Key].additionObjects.Last();

            if (animalType == AnimalType.Deer)
            {
                DeerView deerView = additionalE.view as DeerView;

                if (deerView != null)
                {
                    if (!deerView.DeerSwapSign.CanStart && deerView.AnimalOpenView.IsOpen) { deerView.DeerSwapSign.CanStart = true; }
                }
                continue;
            }
            if (animalType == AnimalType.Boar)
            {
                BoarView boarView = additionalE.view as BoarView;

                continue;
            }
            if (animalType == AnimalType.Hedgehog)
            {
                HedgehogView hedgehogView = additionalE.view as HedgehogView;

                if (hedgehogView != null)
                {
                    hedgehogView.freeSpaceOfSubs.ShowLines();
                }

                continue;
            }
        }
    }

    // #IN_FACTORY
    private GameObject SetAnimalEntities(string animalType)
    {
        GameObject entity = null;
        Vector3 startVector = Vector3.zero;

        switch (animalType)
        {
            case AnimalType.Sheep:
                {
                    entity = Instantiate(
                        animalsEntities.entities[0],
                        startVector,
                        animalsEntities.entities[0].transform.rotation);

                    entity.GetComponent<IAnimalType>().buttonType = AnimalType.Sheep;
                }
                break;
            case AnimalType.Wolf:
                {
                    entity = Instantiate(
                        animalsEntities.entities[1],
                        startVector,    
                        animalsEntities.entities[1].transform.rotation);

                    entity.GetComponent<IAnimalType>().buttonType = AnimalType.Wolf;
                }
                break;
            case AnimalType.Deer:
                {
                    entity = Instantiate(
                        animalsEntities.entities[2],
                        startVector,
                        animalsEntities.entities[2].transform.rotation);

                    entity.GetComponent<IAnimalType>().buttonType = AnimalType.Deer;
                }
                break;
            case AnimalType.Boar:
                {
                    entity = Instantiate(
                        animalsEntities.entities[3],
                        startVector,
                        animalsEntities.entities[3].transform.rotation);

                    entity.GetComponent<IAnimalType>().buttonType = AnimalType.Boar;
                }
                break;
            case AnimalType.Hedgehog:
                {
                    entity = Instantiate(
                        animalsEntities.entities[4],
                        startVector,
                        animalsEntities.entities[4].transform.rotation);

                    entity.GetComponent<IAnimalType>().buttonType = AnimalType.Hedgehog;
                }
                break;
        }

        return entity;
    }

    private async Task ShowElements()
    {
        System.Random random = new System.Random();

        List<int> randIndex = new List<int>();

        IAnimalType[] animalsOfBaseObject;
        List<Task> tasks = new List<Task>();

        int length = 0;
        float time = 0;


        for (int i = 0; i < animalsGroupDictionary.Count; i++)
        {
            randIndex.Add(i);
        }
        randIndex = randIndex.OrderBy(a => random.Next()).ToList();

        for (int i = 0; i < animalsGroupDictionary.Count; i++)
        {
            GamburgerAnimalGroup gag = animalsGroupDictionary.ElementAt(randIndex[i]).Value;
            animalsOfBaseObject = gag.baseParentObject.GetComponentsInChildren<IAnimalType>(true);
            length = animalsOfBaseObject.Length;

            for (int j = 0; j < length; j++) {
                time = 0.25f + (float)(i) * 0.45f + (float)(j) * (0.44f / length);
                tasks.Add(CreateWithAnim(animalsOfBaseObject[j].gameObject, time));
            }
        }

        await Task.WhenAll(tasks);
    }


    #region Create|Delete AnimalObject

    void DestroyBaseObject(GamburgerAnimalGroup animalGroup)
    {
        roundControl.currentIndexElements.Remove(animalGroup.gamburgerElement.baseE);
        currentRound.elementsDictionary.Remove(animalGroup.gamburgerElement.baseE);
        animalsGroupDictionary.Remove(animalGroup.gamburgerElement.baseE);
        
        DestroyWithAnimBase(animalGroup.baseObject.animal, animalGroup.baseParentObject);

        UpdateRoundControlCurrentIndex();
        currentRound.checkEmptyDictionary();
    }

    async void DestroyAdditionObject(GamburgerAnimalGroup animalGroup, int index)
    {
        PairAnimalView pair = animalGroup.additionObjects[index];
        AdditionalElement A_Element = animalGroup.gamburgerElement.additionE[index];

        animalGroup.gamburgerElement.additionE.Remove(A_Element);
        animalGroup.additionObjects.Remove(pair);

        await DestroyWithAnim(pair.animal);

        if (animalGroup.checkEmptyBaseKey()) animalGroup.gamburgerElement.baseE.IsOpen = true;
        else
        {
            animalGroup.gamburgerElement.additionE.Last().IsOpen = true;
        }

        UpdateRoundControlCurrentIndex();
    }

    void ResetBaseSubscriptions(Element element, ButtonView view)
    {
        view.BaseTapHandel.ResetSubscriptions();
        view.BaseTapHandel.gameObject.tag = "Untagged";// "UnClickable";
        element.ResetSubscriptions();
    }

    private async Task CreateWithAnim(GameObject go, float time)
    {
        Animation anim = go.GetComponent<Animation>();

        await Task.Delay((int)(time * 1000));

        go.SetActive(true);
        anim.Play("OpenShot");
    }

    private async Task DestroyWithAnimBase(GameObject go, GameObject baseObject)
    {
        //go.GetComponentInChildren<DrawCircle>().StopDrawing();
        
        await DestroyWithAnim(go);

        if (baseObject != null) Destroy(baseObject);
    }

    private async Task DestroyWithAnim(GameObject go)
    {
        Animation anim = go.GetComponent<Animation>();
        anim.Play("CloseShot");

        await Task.Delay((int)(anim["CloseShot"].length * 1000));

        if (go != null) Destroy(go);
    }

    private async Task ShakeWithAnim(GameObject go)
    {
        Animation anim = go.GetComponent<Animation>();
        anim.Play("ShakeShot");

        await Task.Delay((int)(anim["ShakeShot"].length * 1000));
    }

    #endregion
}
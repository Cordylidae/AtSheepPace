using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;

public class RoundGeneration : MonoBehaviour
{
    [SerializeField] private AnimalEntities animalsEntities;

    // ### NEED Initialaize like INJECT
    [SerializeField] private Sun_Moon_View sun_moon_View;

    // ### NEED Initialaize like INJECT
    [SerializeField] private FearBarView fearBarView;

    Dictionary<BaseElement, GamburgerAnimalGroup> animalsGroupDictionary = new Dictionary<BaseElement, GamburgerAnimalGroup>();

    public Round currentRound;
    private RoundControl roundControl;

    private bool lastClickTime;

    public void FixedUpdate()
    {
        lastClickTime = true;
    }

    public async void PreStartRound()
    {
        SetUpAnimalRuleVaribles();

        BuildUpAnimalDicionary();
        
        await ShowElements();

        StartRoundControlElements();
    }

    private void StartRoundControlElements()
    {
        StartDrawCircleOpenIndex();
        StartShakeOpenDeer();
    }

    private void SetUpAnimalRuleVaribles()
    {
        roundControl = new RoundControl(
            currentRound.elementsDictionary.First().Key.number, 
            fearBarView.fearBar,
            sun_moon_View);

        AddAllIndex(roundControl.indexOfCurrentButtons);
    }

    private void BuildUpAnimalDicionary()
    {
        int index = currentRound.elementsDictionary.Count;

        foreach (KeyValuePair<BaseElement, GamburgerElement> element in currentRound.elementsDictionary)
        {
            GamburgerAnimalGroup animalGroup = new GamburgerAnimalGroup(element.Value);
            animalsGroupDictionary.Add(element.Key, animalGroup);

            animalGroup.SetParentObject(this.transform);

            animalGroup.SetBaseObject(SetAnimalEntities(element.Key.animalType), index, currentRound.elementsDictionary.Count);

            /// <summary> 
            /// Event on click on interective element
            /// </summary>
            
            BaseElement baseE = animalGroup.gamburgerElement.baseE;
            BaseButtonView baseView = animalGroup.baseObject.view as BaseButtonView;

            if (baseView != null)
            {
                baseView.BaseTapHandel.isTap += () =>
                {
                    if (lastClickTime)
                    {
                        roundControl.rule.DecriseTimeCount();
                        checkTheCorrectTapState(roundControl.OnButtonTap(baseE.animalType, baseE.number));
                    }
                    lastClickTime = false;
                };

                baseView.DrawCircle.radiusZero += () =>
                {
                    checkTheCorrectTapState(roundControl.GoneButtonRadius(baseE.animalType));
                };
            }

            for (int i = 0; i < element.Value.additionE.Count; i++)
            {
                animalGroup.SetAdditionObject(
                    SetAnimalEntities(element.Value.additionE[i].animalType), 
                    currentRound.elementsDictionary.Count, i);

                AdditionalElement additionalE = animalGroup.gamburgerElement.additionE[i];
                DeerView deerView = animalGroup.additionObjects[i].view as DeerView;

                int ind = i;

                if (deerView != null)
                {
                    deerView.AnimalSign.SetRandomSign();//if(additionalE.IsOpen) 

                    deerView.BaseTapHandel.isTap += async () =>
                    {
                        if (lastClickTime)
                        {
                            string correctTap = roundControl.OnButtonTap(additionalE.animalType, deerView.AnimalSign.Sign);

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

                                    }
                                    break;
                                case RoundControl.CorrectTapState.UncorrectUndestroy:
                                    {

                                    }
                                    break;
                                case RoundControl.CorrectTapState.CorrectUndestroy:
                                    {
                                        deerView.AnimalNumberIndex.Index--;
                                        if (deerView.AnimalNumberIndex.Index == 0)
                                        {
                                            ResetBaseSubscriptions(additionalE, deerView);

                                            fearBarView.fearBar.DeerGood(roundControl.rule.dayTime);
                                            DestroyAdditionObject(animalGroup, ind);
                                        }
                                    }
                                    break;
                            }

                            roundControl.rule.DecriseTimeCount();
                        }
                        lastClickTime = false;
                    };
                }
                else Debug.Log("Incorrect Dynamic Cast");
            }

            async void checkTheCorrectTapState(string state)
            {
                switch (state)
                {
                    case RoundControl.CorrectTapState.UncorrectDestroy:
                        {
                            baseView.DrawCircle.ResetSubscriptions();
                            ResetBaseSubscriptions(animalGroup.gamburgerElement.baseE, animalGroup.baseObject.view);

                            await ShakeWithAnim(animalGroup.baseObject.animal);
                            DestroyBaseObject(animalGroup);
                        }
                        break;
                    case RoundControl.CorrectTapState.CorrectDestroy:
                        {
                            baseView.DrawCircle.ResetSubscriptions();
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
                roundControl.indexOfCurrentButtons++;
                AddAllIndex(roundControl.indexOfCurrentButtons);
            } while (roundControl.currentIndexElements.Count == 0 && currentRound.elementsDictionary.Count != 0);
            StartDrawCircleOpenIndex();
            StartShakeOpenDeer();
        }
    }

    void AddAllIndex(int indexOfStart)
    {
        foreach (KeyValuePair<BaseElement, GamburgerElement> element in currentRound.elementsDictionary)
        {
            if (!roundControl.currentIndexElements.Contains(element.Key) && element.Key.number == indexOfStart) { 
                roundControl.currentIndexElements.Add(element.Key);
            }
            else if (element.Key.number > indexOfStart) break;
        }
    }

    void StartDrawCircleOpenIndex()
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

    void StartShakeOpenDeer()
    {
        foreach (var element in animalsGroupDictionary)
        {
            if (animalsGroupDictionary[element.Key].checkEmptyBaseKey()) continue;
            var additionalE = animalsGroupDictionary[element.Key].additionObjects.Last();


            DeerView deerView = additionalE.view as DeerView;

            if (deerView != null)
            {
                if (!deerView.DeerSwapSign.CanStart) { deerView.DeerSwapSign.CanStart = true; }
            }
        }
    }

    // #IN_FACTORY
    private GameObject SetAnimalEntities(string animalType)
    {
        GameObject entity = null;

        switch (animalType)
        {
            case AnimalType.Sheep:
                {
                    entity = Instantiate(
                        animalsEntities.animal[0],
                        new Vector3(0.0f, 0.0f, 0.0f),
                        animalsEntities.animal[0].transform.rotation);

                    entity.GetComponent<IAnimalType>().buttonType = AnimalType.Sheep;
                }
                break;
            case AnimalType.Wolf:
                {
                    entity = Instantiate(
                        animalsEntities.animal[1],
                        new Vector3(0.0f, 0.0f, 0.0f),
                        animalsEntities.animal[1].transform.rotation);

                    entity.GetComponent<IAnimalType>().buttonType = AnimalType.Wolf;
                }
                break;
            case AnimalType.Deer:
                {
                    entity = Instantiate(
                        animalsEntities.animal[2],
                        new Vector3(0.0f, 0.0f, 0.0f),
                        animalsEntities.animal[2].transform.rotation);

                    entity.GetComponent<IAnimalType>().buttonType = AnimalType.Deer;
                }
                break;
            case AnimalType.Boar:
                {
                    entity = Instantiate(
                        animalsEntities.animal[3],
                        new Vector3(0.0f, 0.0f, 0.0f),
                        animalsEntities.animal[3].transform.rotation);

                    entity.GetComponent<IAnimalType>().buttonType = AnimalType.Boar;
                }
                break;
            case AnimalType.Hedgehog:
                {
                    entity = Instantiate(
                        animalsEntities.animal[4],
                        new Vector3(0.0f, 0.0f, 0.0f),
                        animalsEntities.animal[4].transform.rotation);

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
        currentRound.checkEmptyDictionary();

        UpdateRoundControlCurrentIndex();

        DestroyWithAnimBase(animalGroup.baseObject.animal, animalGroup.baseParentObject);
    }

    async void DestroyAdditionObject(GamburgerAnimalGroup animalGroup, int index)
    {
        //Debug.Log($"{index}, {animalGroup.additionObjects.Count}");

        PairAnimalView pair = animalGroup.additionObjects[index];
        AdditionalElement A_Element = animalGroup.gamburgerElement.additionE[index];

        animalGroup.gamburgerElement.additionE.Remove(A_Element);
        animalGroup.additionObjects.Remove(pair);

        //Debug.Log($"{index}, {animalGroup.additionObjects.Count}");

        await DestroyWithAnim(pair.animal);

        if (animalGroup.checkEmptyBaseKey()) animalGroup.OpenBaseElement();
        else animalGroup.gamburgerElement.additionE.Last().IsOpen = true;

        UpdateRoundControlCurrentIndex();
    }

    void ResetBaseSubscriptions(Element element, ButtonView view)
    {
        view.BaseTapHandel.ResetSubscriptions();
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
        go.GetComponentInChildren<DrawCircle>().StopDrawing();
        
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
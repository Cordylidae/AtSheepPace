using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Codice.Client.Common;
using System.Threading.Tasks;
using PlasticGui;

public class RoundGeneration : MonoBehaviour
{
    [SerializeField] private AnimalEntities animalsEntities;

    // ### NEED Initialaize like INJECT
    [SerializeField] private FearBarView fearBarView;

    List<GameObject> animalsElements = new List<GameObject>();

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

        StartDrawCircleElements(roundControl.indexOfCurrentButtons);
        currentRound.checkEmptyBaseKey(currentRound.elementsDictionary.First().Key);

    }

    private void SetUpAnimalRuleVaribles()
    {
        roundControl = new RoundControl();

        // ### NEED setup round controll from Constructor and set its public varibles to private 
        roundControl.indexOfCurrentButtons = currentRound.elementsDictionary.First().Key.number;
        roundControl.currentOpenElements = new List<BaseElement>();

        roundControl.fearBar = fearBarView.fearBar;
    }

    private void BuildUpAnimalDicionary()
    {
        int index = currentRound.elementsDictionary.Count;

        foreach (KeyValuePair<BaseElement, List<AdditionalElement>> element in currentRound.elementsDictionary)
        {
            BaseElement baseElement = element.Key;

            GameObject baseObject = new GameObject($"BaseObject #{baseElement.animalType}-{baseElement.number}");
            baseObject.transform.SetParent(this.transform);
            animalsElements.Add(baseObject);

            GameObject animal = SetAnimalEntities(baseElement.animalType);
            animal.transform.SetParent(baseObject.transform);

            animal.transform.localPosition = new Vector3(
                            UnityEngine.Random.RandomRange(-450, 450) / 1080.0f * 6.0f,
                            UnityEngine.Random.RandomRange(-520, 520) / 1920.0f * 10.0f,
                            //0.0f, 0.0f,
                            index * (-0.5f / currentRound.elementsDictionary.Count));

            roundControl.currentOpenElements.Add(baseElement);

            BaseButtonView animalView = animal.GetComponent<BaseButtonView>();
            animalView.AnimalUniqIndex.Index = baseElement.number;
            animalView.isOpen = baseElement.isOpen;

            switch (animalView.AnimalType.buttonType)
            {
                case AnimalType.Sheep:
                    {
                        SheepView sheepView = (SheepView)animalView;
                    }
                    break;
                case AnimalType.Wolf:
                    {
                        WolfView wolfView = (WolfView)animalView;
                    }
                    break;
            }

            /// <summary> 
            /// Event on click on interective element
            /// </summary>
            animalView.BaseTapHandel.isTap += () => {
                if (lastClickTime)
                {
                    if (roundControl.IsTappedButtonDestroy(baseElement.animalType, baseElement.number))
                    {
                        DestroyBaseObject();
                    }

                    lastClickTime = false;
                }
            };

            animalView.DrawCircle.radiusZero += () =>
            {
                roundControl.GoneButtonRadius(baseElement.animalType);

                DestroyBaseObject();
            };

            // event on is open
            currentRound.zeroElementsOfBaseKey += () =>
            {
                baseElement.isOpen = true;
                animalView.isOpen = baseElement.isOpen;
                UpdateRoundControlCurrentIndex();
            };

            void DestroyBaseObject()
            {
                roundControl.currentOpenElements.Remove(baseElement);

                currentRound.elementsDictionary.Remove(baseElement);
                animalsElements.Remove(baseObject);
                currentRound.checkEmptyDictionary();

                UpdateRoundControlCurrentIndex();

                DestroyWithAnimBase(animal, baseObject);
            }



            //List<AdditionalElement> aA = element.Value;  other addition buttons

            index--;
        }
    }

    private void UpdateRoundControlCurrentIndex()
    {
        Debug.Log("Kuku");
    }

    private void StartDrawCircleElements(int indexOfStart)
    {
        foreach (GameObject baseObject in animalsElements)
        {
            BaseButtonView animalView = baseObject.GetComponentInChildren<BaseButtonView>();

            if (animalView.isOpen && animalView.AnimalUniqIndex.Index == indexOfStart) animalView.DrawCircle.StartDrawing(2.7f);
            else if (animalView.AnimalUniqIndex.Index > indexOfStart) break;
        }
    }
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
                        animalsEntities.animal[2],
                        new Vector3(0.0f, 0.0f, 0.0f),
                        animalsEntities.animal[2].transform.rotation);

                    entity.GetComponent<IAnimalType>().buttonType = AnimalType.Boar;
                }
                break;
            case AnimalType.Hedgehog:
                {
                    entity = Instantiate(
                        animalsEntities.animal[3],
                        new Vector3(0.0f, 0.0f, 0.0f),
                        animalsEntities.animal[3].transform.rotation);

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


        for (int i = 0; i < animalsElements.Count; i++)
        {
            randIndex.Add(i);
        }
        randIndex = randIndex.OrderBy(a => random.Next()).ToList();

        for (int i = 0; i < animalsElements.Count; i++)
        {
            animalsOfBaseObject = animalsElements[randIndex[i]].GetComponentsInChildren<IAnimalType>(true);
            length = animalsOfBaseObject.Length;

            for (int j = 0; j < length; j++) {
                time = 0.25f + (float)(i) * 0.45f + (float)(j) * 0.75f;
                tasks.Add(CreateWithAnim(animalsOfBaseObject[length - 1 - j].gameObject, time));
            }
        }

        await Task.WhenAll(tasks);
    }

    private async Task CreateWithAnim(GameObject go, float time)
    {
        Animation anim = go.GetComponent<Animation>();

        await Task.Delay((int)(time * 1000));

        go.SetActive(true);
        anim.Play("OpenShot");

        //go.GetComponentInChildren<DrawCircle>().StartDrawing(2.7f);
    }

    private async Task DestroyWithAnimBase(GameObject go, GameObject baseObject)
    {
        go.GetComponentInChildren<DrawCircle>().StopDrawing();

        Animation anim = go.GetComponent<Animation>();
        anim.Play("CloseShot");

        await Task.Delay((int)(anim["CloseShot"].length * 1000));

        if (go != null) Destroy(go);
        if (baseObject != null) Destroy(baseObject);
    }

    IEnumerator DestroyWithAnim(GameObject go)
    {
        Animation anim = go.GetComponent<Animation>();
        anim.Play("CloseShot");

        yield return new WaitForSeconds(anim["CloseShot"].length);

        Destroy(go);
    }
}

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
    public List<BaseElement> currentOpenElements;

    public FearBar fearBar; 

    private string OnButtonTap(string animalType, int index)
    {
        switch (animalType)
        {
            case AnimalType.Sheep:
                {
                    if (index == indexOfCurrentButtons)
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

    void NextElement()
    {
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

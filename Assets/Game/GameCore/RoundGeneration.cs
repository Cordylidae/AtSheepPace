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

    Dictionary<BaseElement, GameObject> animalsElements = new Dictionary<BaseElement, GameObject>();

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

        StartRoundControlElements(roundControl.indexOfCurrentButtons);
        currentRound.checkEmptyBaseKey(currentRound.elementsDictionary.First().Key);

    }

    private void SetUpAnimalRuleVaribles()
    {
        roundControl = new RoundControl();

        // ### NEED setup round controll from Constructor and set its public varibles to private 
        roundControl.indexOfCurrentButtons = currentRound.elementsDictionary.First().Key.number;
        roundControl.currentIndexElements = new List<BaseElement>();
        AddAllIndex(roundControl.indexOfCurrentButtons);

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
            animalsElements.Add(baseElement, baseObject);

            GameObject animal = SetAnimalEntities(baseElement.animalType);
            animal.transform.SetParent(baseObject.transform);

            animal.transform.localPosition = new Vector3(
                            UnityEngine.Random.RandomRange(-450, 450) / 1080.0f * 6.0f,
                            UnityEngine.Random.RandomRange(-520, 520) / 1920.0f * 10.0f,
                            //0.0f, 0.0f,
                            index * (-0.5f / currentRound.elementsDictionary.Count));

            
            BaseButtonView animalView = animal.GetComponent<BaseButtonView>();
            animalView.AnimalUniqIndex.Index = baseElement.number;

            if (baseElement.isOpen)
            {
                animalView.SetOpen();
            }else animalView.SetClose();

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
            //currentRound.zeroElementsOfBaseKey += () =>
            //{
            //    baseElement.isOpen = true;
            //    animalView.isOpen = baseElement.isOpen;
            //    UpdateRoundControlCurrentIndex();
            //};

            void DestroyBaseObject()
            {
                roundControl.currentIndexElements.Remove(baseElement);

                currentRound.elementsDictionary.Remove(baseElement);
                animalsElements.Remove(baseElement);
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
        if (roundControl.currentIndexElements.Count == 0) { 
            roundControl.indexOfCurrentButtons++;
            StartRoundControlElements(roundControl.indexOfCurrentButtons);
        }
    }

    private void StartRoundControlElements(int indexOfStart)
    {
        AddAllIndex(indexOfStart);
        StartDrawCircleOpenIndex();
    }

    void AddAllIndex(int indexOfStart)
    {
        foreach (KeyValuePair<BaseElement, List<AdditionalElement>> element in currentRound.elementsDictionary)
        {
            //GameObject baseObject = animalsElements[element.Key];
            //BaseButtonView animalView = baseObject.GetComponentInChildren<BaseButtonView>();

            if (!roundControl.currentIndexElements.Contains(element.Key) && element.Key.number == indexOfStart) { 
                roundControl.currentIndexElements.Add(element.Key);
            }
            else if (element.Key.number > indexOfStart) break;
        }
        Debug.Log(roundControl.currentIndexElements.Count);
    }

    void StartDrawCircleOpenIndex()
    {
        foreach (BaseElement key in roundControl.currentIndexElements)
        {
            GameObject baseObject = animalsElements[key];
            BaseButtonView animalView = baseObject.GetComponentInChildren<BaseButtonView>();

            if (key.isOpen)
            {
                animalView.DrawCircle.StartDrawing(2.7f);
            }
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
            animalsOfBaseObject = animalsElements.ElementAt(randIndex[i]).Value.GetComponentsInChildren<IAnimalType>(true);
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
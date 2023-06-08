using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;

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

        StartRoundControlElements();
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
                            index * (-4.0f / currentRound.elementsDictionary.Count));

            
            BaseButtonView animalView = animal.GetComponent<BaseButtonView>();
            animalView.AnimalUniqIndex.Index = baseElement.number;

            // event on is open
            baseElement.IOpen += () =>
            {
                if (baseElement.IsOpen)
                {
                    animalView.SetOpen();
                }
                else animalView.SetClose();
            };

            baseElement.IOpen?.Invoke();

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
                    checkTheCorrectBaseTapState(roundControl.OnButtonTap(baseElement.animalType, baseElement.number));
                }
                lastClickTime = false;
            };

            animalView.DrawCircle.radiusZero += () =>
            {
                checkTheCorrectBaseTapState(roundControl.GoneButtonRadius(baseElement.animalType));
            };

            void checkTheCorrectBaseTapState(string state)
            {
                switch (state)
                {
                    case RoundControl.CorrectTapState.UncorrectDestroy:
                        {
                            //shake it
                            DestroyBaseObject();
                        }
                        break;
                    case RoundControl.CorrectTapState.CorrectDestroy:
                        {
                            DestroyBaseObject();
                        }
                        break;
                    case RoundControl.CorrectTapState.UncorrectUndestroy:
                        {
                            //shake it
                        }
                        break;
                    case RoundControl.CorrectTapState.CorrectUndestroy:
                        {
                        }
                        break;
                }
            }

            void OpenBaseElement()
            {
                baseElement.IsOpen = true;

                animalView.DrawCircle.StartDrawing(2.7f);
            }

            void DestroyBaseObject()
            {
                roundControl.currentIndexElements.Remove(baseElement);

                currentRound.elementsDictionary.Remove(baseElement);
                animalsElements.Remove(baseElement);
                currentRound.checkEmptyDictionary();

                UpdateRoundControlCurrentIndex();

                DestroyWithAnimBase(animal, baseObject);
            }

    
            for (int i = 0; i < element.Value.Count; i++)
            {
                AdditionalElement A_Element = element.Value[i];

                GameObject A_Animal = SetAnimalEntities(element.Value[i].animalType);
                A_Animal.name += $" Index : {i}";
                A_Animal.transform.SetParent(baseObject.transform);

                A_Animal.transform.localPosition = new Vector3(
                               animal.transform.localPosition.x + UnityEngine.Random.RandomRange(-4.5f, 4.5f) / 1080.0f * 6.0f,
                               animal.transform.localPosition.y + UnityEngine.Random.RandomRange(-5.2f, 5.2f) / 1920.0f * 10.0f,
                               //0.0f, 0.0f,
                               animal.transform.localPosition.z + (i+1) * ((-3.9f / currentRound.elementsDictionary.Count) / element.Value.Count));

                DeerView A_AnimalView = A_Animal.GetComponent<DeerView>();
                A_AnimalView.AnimalNumberIndex.Index = UnityEngine.Random.RandomRange(2, 4);

                // event on is open
                A_Element.IOpen += () =>
                {
                    if (A_Element.IsOpen)
                    {
                        A_AnimalView.SetOpen();
                    }
                    else A_AnimalView.SetClose();
                };

                A_Element.IOpen?.Invoke();

                //switch (A_AnimalView.AnimalType.buttonType)
                //{
                //    case AnimalType.Sheep:
                //        {
                //            SheepView sheepView = (SheepView)animalView;
                //        }
                //        break;
                //    case AnimalType.Wolf:
                //        {
                //            WolfView wolfView = (WolfView)animalView;
                //        }
                //        break;
                //}

                A_AnimalView.BaseTapHandel.isTap += () => {
                    if (lastClickTime)
                    {
                        //checkTheCorrectBaseTapState(roundControl.OnButtonTap(A_Element.animalType, baseElement.number));
                        
                        A_AnimalView.AnimalNumberIndex.Index--;
                        if (A_AnimalView.AnimalNumberIndex.Index == 0) DestroyAdditionObject(A_Element, A_Animal);
                    }
                    lastClickTime = false;
                };
            }

            async void DestroyAdditionObject(AdditionalElement A_Element, GameObject A_Animal)
            {
                currentRound.elementsDictionary[baseElement].Remove(A_Element);

                if (currentRound.checkEmptyBaseKey(baseElement)) OpenBaseElement();
                else currentRound.elementsDictionary[baseElement].Last().IsOpen = true;

                UpdateRoundControlCurrentIndex();

                await DestroyWithAnimAddition(A_Animal);
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
            StartRoundControlElements();
        }
    }

    private void StartRoundControlElements()
    {
        StartDrawCircleOpenIndex();
    }

    void AddAllIndex(int indexOfStart)
    {
        foreach (KeyValuePair<BaseElement, List<AdditionalElement>> element in currentRound.elementsDictionary)
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
            GameObject baseObject = animalsElements[key];
            BaseButtonView animalView = baseObject.GetComponentInChildren<BaseButtonView>();

            if (key.IsOpen)
            {
                animalView.DrawCircle.StartDrawing(2.7f);
            }
            else
            {
                if (currentRound.checkEmptyBaseKey(key))
                {
                    key.IsOpen = true;

                    animalView.DrawCircle.StartDrawing(2.7f);
                }
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
                time = 0.25f + (float)(i) * 0.45f + (float)(j) * (0.44f / length);
                tasks.Add(CreateWithAnim(animalsOfBaseObject[j].gameObject, time));
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

    private async Task DestroyWithAnimAddition(GameObject go)
    {
        Animation anim = go.GetComponent<Animation>();
        anim.Play("CloseShot");

        await Task.Delay((int)(anim["CloseShot"].length * 1000));

        if (go != null) Destroy(go);
    }
}
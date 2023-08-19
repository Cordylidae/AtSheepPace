using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnControlAnimals : MonoBehaviour
{
    public System.Action Win;
    public System.Action Lose;

    struct SpawnHardPropities
    {
        public int count;

        public int minAtView;
        public int maxAtView;

        public int radiusOfSpawn;
        public int intervalOfSpawn;

        //public int pauseSpawnAtView;
        //public int partSpawnAtView;

        //public bool orderChaos;
    };

    private int SheepCount = 0;
    private int SheepCatched = 0;
    private int WolvesCount = 0;
    private int WolvesCatched = 0;
    private int DeerCount = 0;
    private int DeerCatched = 0;


    [SerializeField] private Entities animalsEntities;
    [SerializeField] private int Count = 12;
    [SerializeField] private float SpeedSpawn = 1.05f;

    [Space]

    [SerializeField] private TextMeshProUGUI textGoal;
    [SerializeField] private TextMeshProUGUI textTotal;

    private SpawnHardPropities hardPropities = new SpawnHardPropities();
    private int lastIndex;
    private int levelOfHard;

    //void Start()
    //{
    //    PreStartGenerate();
    //    StartGenerate();
    //}

    public void PreStartGenerate(int level = 0)
    {
        lastIndex = -1;

        //levelOfHard = level;

        hardPropities.count = Count;
        hardPropities.minAtView = 1;
        hardPropities.maxAtView = 4;

        //float time = (0.7f * Count) + 10.0f;
        //rope.ReStartRopeTime(true, time);
    }

    List<GameObject> Animals = new List<GameObject>();
    List<GameObject> CurrentAnimals = new List<GameObject>();

    // TODO remove index for object entities take dictionary

    public void StartGenerate()
    {
        for (int i = hardPropities.count - 1; i >= 0; i--)
        {
            GameObject go = SetAnimalType();

            go.transform.SetParent(this.transform);
            go.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

            go.transform.localPosition = new Vector3(
                            Random.RandomRange(-450, 450) / 1080.0f * 6.0f,
                            Random.RandomRange(-520, 520) / 1920.0f * 10.0f,
                            i * (10.0f / hardPropities.count));

            //go.SetActive(false);

            //tap need target to set
            int index = go.GetComponentInChildren<IAnimalUniqIndex>().Index = i;

            go.GetComponentInChildren<IsTapHandel>().isTapWithIndex += OnShotButtonClick;
            go.GetComponentInChildren<DrawCircle>().isGoneRadius += OnWolvesDestroy;

            Animals.Add(go);
        }

        textGoal.text = SheepCount.ToString();
        textTotal.text = hardPropities.count.ToString();

        ShowAll();
        //OnNextShot();
    }

    private void ShowAll()
    {
        for (int i = 0; i < Animals.Count; i++)
        {
            //CurrentAnimals.Add(Animals[Animals.Count - 1 - i]);

            StartCoroutine(CreateWithAnim(Animals[Animals.Count - 1 - i], (float)(i + 1) * SpeedSpawn));
        }
    }

    //private void OnNextShot()
    //{
    //    int CountAtNextView = Random.RandomRange(hardPropities.minAtView, hardPropities.maxAtView);

    //    for (int i = 0; i < CountAtNextView; i++)
    //    {

    //        SetAnimalType(Animals[Animals.Count - 1 - i]);

    //        CurrentAnimals.Add(Animals[Animals.Count - 1 - i]);

    //        StartCoroutine(CreateWithAnim(Animals[Animals.Count - 1 - i], (float)(i + 1) * 0.05f));
    //    }
    //}

    private GameObject SetAnimalType()
    {
        GameObject go;
        int index = Random.RandomRange(0, 100);

        if (index < 50) // Sheep
        {
            go = Instantiate(animalsEntities.entities[0], new Vector3(0.0f, 0.0f, 0.0f), animalsEntities.entities[0].transform.rotation);
            go.GetComponent<IAnimalType>().buttonType = AnimalType.Sheep;

            SheepCount++;
        }
        else if (index >= 50 && index < 80) // Wolf
        {
            go = Instantiate(animalsEntities.entities[1], new Vector3(0.0f, 0.0f, 0.0f), animalsEntities.entities[1].transform.rotation);
            go.GetComponent<IAnimalType>().buttonType = AnimalType.Wolf;

            WolvesCount++;

            {
                //GameObject goFrost = Instantiate(buttonModificator.buttonEntities[0], new Vector3(0.0f, 0.0f, 0.0f), buttonModificator.buttonEntities[0].transform.rotation);

                //goFrost.SetActive(true);
                //goFrost.transform.SetParent(go.transform);

                //goFrost.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                //goFrost.transform.localPosition = new Vector3(0.0f, 5.0f, 0.0f);

                //goFrost.name = "Button_Type_Frost";

                //IFrostButtonTrigger frostTrigger = goFrost.GetComponentInChildren<IFrostButtonTrigger>();

                //frostTrigger.OnTrigger.AddListener(() =>
                //{
                //    Destroy(goFrost);
                //});

                //frostTrigger.Durability = Random.Range(1, 5);
            }
        }
        else // Deer
        {
            go = Instantiate(animalsEntities.entities[2], new Vector3(0.0f, 0.0f, 0.0f), animalsEntities.entities[2].transform.rotation);
            go.GetComponent<IAnimalType>().buttonType = AnimalType.Deer;

            DeerCount++;
        }

        return go;
    }

    // TODO # 2 Need go to another script for tap control

    private bool lastClickTime;

    void FixedUpdate()
    {
        lastClickTime = true;
    }

    // Not working right cant skip wolves

    private void OnWolvesDestroy(int indexButton)
    {
        lastIndex = indexButton;

        GameObject go = Animals[Animals.Count - 1];

        Debug.Log("Destroy Wolf");

        Animals.RemoveAt(Animals.Count - 1);

        go.GetComponentInChildren<IsTapHandel>().isTapWithIndex -= OnShotButtonClick;
        go.GetComponentInChildren<DrawCircle>().isGoneRadius -= OnWolvesDestroy;

        StartCoroutine(DestroyWithAnim(go));
        
    }

    private void OnShotButtonClick(int indexButton)
    {
        if (lastClickTime)
        {
            if (indexButton - lastIndex == 1)
            {
                lastIndex = indexButton;

                GameObject go = Animals[Animals.Count - 1];
                IAnimalType animalType = go.GetComponent<IAnimalType>();

                Debug.Log("Click norlmal: " + animalType.buttonType);

                if (animalType.buttonType == AnimalType.Sheep) { SheepCatched++; textGoal.text = (SheepCount - SheepCatched).ToString(); }
                if (animalType.buttonType == AnimalType.Wolf) WolvesCatched++;
                if (animalType.buttonType == AnimalType.Deer) DeerCatched++;

                if (SheepCatched >= SheepCount) Win.Invoke();
                if (WolvesCatched > WolvesCount / 2) Lose.Invoke();

                Animals.RemoveAt(Animals.Count - 1);
                //CurrentButtons.RemoveAt(CurrentButtons.Count - 1);
                StartCoroutine(DestroyWithAnim(go));
            }
            {
                //else if (indexButton - lastIndex == 2)
                //{

                //    float seconds = 2.0f;
                //    Debug.Log("Click no norlmal - add " + seconds + " sec.");

                //    rope.addTimeRope(seconds);

                //    lastIndex = indexButton;


                //    for (int i = 0; i < 2; i++)
                //    {
                //        GameObject go = Buttons[Buttons.Count - 1];
                //        Buttons.RemoveAt(Buttons.Count - 1);
                //        CurrentButtons.RemoveAt(CurrentButtons.Count - 1);
                //        StartCoroutine(DestroyWithAnim(go));
                //    }

                //}
                //else if (indexButton - lastIndex > 2)
                //{

                //    float seconds = 1.0f;
                //    Debug.Log("Miss click - add " + seconds + " sec.");

                //    rope.addTimeRope(seconds);

                //}
            }

            //if (Animals.Count == 0) EndGameShot();
            //else if (CurrentButtons.Count == 0) OnNextShot();

            lastClickTime = false;
        }
    }

    IEnumerator CreateWithAnim(GameObject go, float time)
    {
        Animation anim = go.GetComponent<Animation>();

        yield return new WaitForSeconds(time);

        go.SetActive(true);
        anim.Play("OpenShot");
    }

    IEnumerator DestroyWithAnim(GameObject go)
    {
        Animation anim = go.GetComponent<Animation>();
        anim.Play("CloseShot");

        yield return new WaitForSeconds(anim["CloseShot"].length);

        Destroy(go);
    }

    public void ClearAllShots()
    {
        while (Animals.Count != 0)
        {
            GameObject go = Animals[0];
            Animals.RemoveAt(0);

            go.GetComponentInChildren<IsTapHandel>().isTapWithIndex -= OnShotButtonClick;
            go.GetComponentInChildren<DrawCircle>().isGoneRadius -= OnWolvesDestroy;

            Destroy(go);
        }

        CurrentAnimals.Clear();
    }
}

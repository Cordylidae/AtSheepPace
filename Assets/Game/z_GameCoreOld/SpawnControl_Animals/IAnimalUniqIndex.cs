using System;
using UnityEngine;

public class IAnimalUniqIndex : MonoBehaviour
{
    [SerializeField] private SubElements numbers;

    [SerializeField] private int index = 0;

    [SerializeField] private SpriteRenderer[] numeric;


    private void Start()
    {
        ChangeSprites();
    }

    public int Index
    {
        set
        {
            index = value;
            if (index < 0) throw new Exception();

            ChangeSprites();
        }
        get
        {
            return index;
        }
    }

    private void ChangeSprites()
    {
        if (index >= 0 && index <= 9)
        {
            numeric[0].gameObject.SetActive(true);
            numeric[1].gameObject.SetActive(false);
            numeric[2].gameObject.SetActive(false);

            numeric[0].sprite = numbers.sprites[index];
        }
        if (index >= 10 && index <= 99)
        {
            numeric[0].gameObject.SetActive(false);
            numeric[1].gameObject.SetActive(true);
            numeric[2].gameObject.SetActive(true);

            numeric[1].sprite = numbers.sprites[index / 10];
            numeric[2].sprite = numbers.sprites[index % 10];
        }
    }

    private void OnValidate()
    {
        ChangeSprites();
    }
}
using System;
using UnityEngine;

public class IAnimalOpenView : MonoBehaviour
{
    [SerializeField] private bool isOpen = true;

    [SerializeField] private SpriteRenderer[] view;


    private void Start()
    {
        ChangeView();
    }

    public bool IsOpen
    {
        set
        {
            isOpen = value;

            ChangeView();
        }
        get
        {
            return isOpen;
        }
    }

    private void ChangeView()
    {
        if (isOpen)
        {
            view[0].gameObject.SetActive(true);
            view[1].gameObject.SetActive(false);

        }
        else
        {
            view[0].gameObject.SetActive(false);
            view[1].gameObject.SetActive(true);
        }
    }
}

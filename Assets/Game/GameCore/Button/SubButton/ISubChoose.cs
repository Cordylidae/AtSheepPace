using UnityEngine;

public class ISubChoose : MonoBehaviour
{
    [SerializeField] private bool isChoose = true;
    [SerializeField] private SpriteRenderer view;


    private void Start()
    {
        ChangeView();
    }

    public bool IsChoose
    {
        set
        {
            isChoose = value;

            ChangeView();
        }
        get
        {
            return isChoose;
        }
    }

    private void ChangeView()
    {
        if (isChoose)
        {
            view.color = Color.yellow;
        }
        else
        {
            view.color = Color.white;
        }
    }
}

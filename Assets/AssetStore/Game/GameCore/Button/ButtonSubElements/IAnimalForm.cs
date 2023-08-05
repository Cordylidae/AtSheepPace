using UnityEngine;

public class IAnimalForm : MonoBehaviour
{
    [SerializeField] private SubElements formSprites;
    [SerializeField] private SpriteRenderer formView;

    [SerializeField] private FormState form;

    private void Start()
    {
        ChangeForm();
    }

    public FormState Form
    {
        set
        {
            form = value;
            ChangeForm();
        }
        get
        {
            return form;
        }
    }

    public void SetRandomForm()
    {
        int rand = UnityEngine.Random.RandomRange(0, 100);

        if (rand < 33) Form = FormState.Circle;
        else if (rand >= 33 && rand <= 66) Form = FormState.Triangle;
        else Form = FormState.Hex;
    }
    private void ChangeForm()
    {
        switch (form)
        {
            case FormState.Circle:
                {
                    formView.sprite = formSprites.sprites[0];
                }
                break;

            case FormState.Triangle:
                {
                    formView.sprite = formSprites.sprites[1];
                }
                break;
            case FormState.Hex:
                {
                    formView.sprite = formSprites.sprites[2];
                }
                break;
        }
    }
}

public enum FormState { Circle, Triangle, Hex };
using UnityEngine;

public class IAnimalSign : MonoBehaviour
{
    [SerializeField] private SubElements signSprites;
    [SerializeField] private SpriteRenderer signView;

    [SerializeField] private SignState sign;

    private void Start()
    {
        ChangeSign();
    }

    public SignState Sign
    {
        set
        {
            sign = value;
            ChangeSign();
        }
        get
        {
            return sign;
        }
    }

    private void ChangeSign()
    {
        switch (sign)
        {
            case SignState.True:
                {
                    signView.sprite = signSprites.sprites[0];
                }break;

            case SignState.False:
                {
                    signView.sprite = signSprites.sprites[1];
                }
                break;
        }
    }
}

public enum SignState { True, False };
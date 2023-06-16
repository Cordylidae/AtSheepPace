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

    public void SetRandomSign()
    {
        int rand = UnityEngine.Random.RandomRange(0, 100);

        if (rand < 50) Sign = SignState.True;
        else Sign = SignState.False;
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
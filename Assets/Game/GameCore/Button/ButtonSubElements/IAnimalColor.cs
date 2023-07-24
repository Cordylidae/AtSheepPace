using UnityEngine;
using NaughtyAttributes;

public class IAnimalColor : MonoBehaviour
{
    public bool fromSprite = false;
    [ShowIf("fromSprite")][SerializeField] private SubElements colorSprites;
    [SerializeField] private SpriteRenderer colorView;

    [SerializeField] private ColorState color;

    private void Start()
    {
        ChangeColor();
    }

    public ColorState myColor
    {
        set
        {
            color = value;
            ChangeColor();
        }
        get
        {
            return color;
        }
    }

    [Button]
    public void SetRandomColor()
    {
        int rand = UnityEngine.Random.RandomRange(0, 100);

        if (rand < 25) myColor = ColorState.Red;
        else if (rand >= 25 && rand < 50) myColor = ColorState.Yellow;
        else if (rand >= 50 && rand < 75) myColor = ColorState.Green;
        else myColor = ColorState.Blue;
    }

    public Color getColor()
    {
        return colorView.color;
    }

    private void ChangeColor()
    {
        switch (color)
        {
            case ColorState.Red:
                {
                    if (fromSprite) colorView.sprite = colorSprites.sprites[0];
                    else colorView.color = Color.red;
                }
                break;

            case ColorState.Yellow:
                {
                    if(fromSprite) colorView.sprite = colorSprites.sprites[1];
                    else colorView.color = Color.yellow;
                }
                break;
            case ColorState.Green:
                {
                    if (fromSprite) colorView.sprite = colorSprites.sprites[2];
                    else colorView.color = Color.green;
                }
                break;
            case ColorState.Blue:
                {
                    if (fromSprite) colorView.sprite = colorSprites.sprites[3];
                    else colorView.color = Color.blue;
                } break;
        }
    }
}

public enum ColorState { Red, Yellow, Green, Blue };
using UnityEngine;

public class BoarColor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteCircle;
    private void Awake()
    {
        Color color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        spriteCircle.color = color;
    }
}

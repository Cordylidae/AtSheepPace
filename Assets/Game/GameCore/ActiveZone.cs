using UnityEngine;

public class ActiveZone : MonoBehaviour
{
    RectTransform active;

    public Vector2 topLeft;
    public Vector2 downRight;

    private void Awake()
    {
        active = GetComponent<RectTransform>();
    }

    void Start()
    {
        topLeft = active.transform.TransformPoint(
            active.transform.localPosition.x - active.rect.width / 2, 
            active.transform.localPosition.y + active.rect.height / 2, 
            active.transform.localPosition.z);

        downRight = active.transform.TransformPoint(
            active.transform.localPosition.x + active.rect.width / 2,
            active.transform.localPosition.y - active.rect.height / 2,
            active.transform.localPosition.z);
    }
}

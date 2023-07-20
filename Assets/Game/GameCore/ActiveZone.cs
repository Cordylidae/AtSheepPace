using UnityEngine;

public class ActiveZone : MonoBehaviour
{
    RectTransform active;

    public Vector2 topLeft;
    public Vector2 downRight;

    FreeSpaceMassive freeSpaceMassive;

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

        freeSpaceMassive = new FreeSpaceMassive(new SquareArea(topLeft, downRight));
    }

    public void SetBaseObjectPosition(Transform _transform, int index, int countBaseElement)
    {
        Vector2 areaPos = SetArea(_transform, 1.0f);

        transform.localPosition = new Vector3(
            areaPos.x,
            areaPos.y,
            index * (-4.0f / countBaseElement));
    }

    public Vector2 SetArea(Transform _transform, float radius)
    {
        SquareArea area = new SquareArea(_transform.position, radius);

        if (freeSpaceMassive.AddInFreeSpace(area)) Debug.Log("All good");
        else
        {
            freeSpaceMassive.NewLayout();

            if (freeSpaceMassive.AddInFreeSpace(area)) Debug.Log("Make new layout and All good");
            else Debug.Log("Something went wrong");
        }

        return area.center;
    }
}

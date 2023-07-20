using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class ActiveZone : MonoBehaviour
{
    RectTransform active;

    public Vector2 topLeft;
    public Vector2 downRight;

    FreeSpaceMassive freeSpaceMassive;

    private void Awake()
    {
        active = GetComponent<RectTransform>();

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

    public Vector3 SetBaseObjectPosition(int index, int countBaseElement)
    {
        Vector2 areaPos = SetArea(0.8f);

        return new Vector3(
            areaPos.x,
            areaPos.y,
            index * (-4.0f / countBaseElement));
    }

    public Vector2 SetArea(float radius)
    {
        SquareArea area = new SquareArea(Vector2.zero, radius);

        if (freeSpaceMassive.AddInFreeSpace(area)) { Debug.Log("All good " + area.topLeft + area.downRight + area.center); }
        else
        {
            freeSpaceMassive.NewLayout();

            if (freeSpaceMassive.AddInFreeSpace(area)) Debug.Log("Make new layout and All good");
            else Debug.Log("Something went wrong");
        }

        foreach (SquareArea square in freeSpaceMassive.freeSpace)
        {
            Color color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);

            Debug.DrawLine(square.GlobalTL, new Vector2(square.GlobalDR.x, square.GlobalTL.y), color , 10f);
            Debug.DrawLine(new Vector2(square.GlobalDR.x, square.GlobalTL.y), square.downRight, color, 10f);
            Debug.DrawLine(square.downRight, new Vector2(square.GlobalTL.x, square.GlobalDR.y), color, 10f);
            Debug.DrawLine(new Vector2(square.GlobalTL.x, square.GlobalDR.y), square.topLeft, color, 10f);
        }

        return area.center;
    }
}

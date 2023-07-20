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
        Color color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        Vector2 areaPos = SetArea(0.6f, index * (-14.0f / countBaseElement), color);

        return new Vector3(
            areaPos.x,
            areaPos.y,
            index * (-14.0f / countBaseElement));
    }

    public Vector2 SetArea(float radius, float z, Color color)
    {
        SquareArea area = new SquareArea(Vector2.zero, radius);

        if (freeSpaceMassive.AddInFreeSpace(area)) { Debug.Log("All good " + area.topLeft + area.downRight + area.center); }
        else
        {
            Debug.Log(area.topLeft + " " + area.downRight + " " + area.center);

            foreach (SquareArea square in freeSpaceMassive.freeSpace)
            {
                Debug.Log(square.topLeft + " " + square.downRight + " " + square.center);
            }

            freeSpaceMassive.NewLayout();

            if (freeSpaceMassive.AddInFreeSpace(area)) Debug.Log("Make new layout and All good");
            else Debug.Log("Something went wrong");
        }
      
        foreach (SquareArea square in freeSpaceMassive.freeSpace)
        {
            Debug.DrawLine(new Vector3(square.GlobalTL.x, square.GlobalTL.y, z), new Vector3(square.GlobalDR.x, square.GlobalTL.y, z), color , 10f);
            Debug.DrawLine(new Vector3(square.GlobalDR.x, square.GlobalTL.y, z), new Vector3(square.GlobalDR.x, square.GlobalDR.y, z) , color, 10f);
            Debug.DrawLine(new Vector3(square.GlobalDR.x, square.GlobalDR.y, z), new Vector3(square.GlobalTL.x, square.GlobalDR.y, z), color, 10f);
            Debug.DrawLine(new Vector3(square.GlobalTL.x, square.GlobalDR.y, z), new Vector3(square.GlobalTL.x, square.GlobalTL.y, z), color, 10f);
        }

        return area.center;
    }
}

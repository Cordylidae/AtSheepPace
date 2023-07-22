using System.Collections.Generic;
using System.ComponentModel;
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
        Vector2 areaPos = SetArea(0.6f, index * (-4.0f / countBaseElement), color);

        return new Vector3(
            areaPos.x,
            areaPos.y,
            index * (-4.0f / countBaseElement));
    }
    
    public void SetHedgehogSubsPosition(List<SubViewHedgehog> subViews, Vector3 position)
    {
        SquareArea area = new SquareArea(position * 0.8f, 1.7f);
        Debug.Log("Start area" + area.center + area.topLeft + area.GlobalTL);

        freeSpaceMassive.NewLayout(area);
        Debug.Log(area.center);

        foreach (SubViewHedgehog subView in subViews)
        {
            Vector2 areaPos = SetArea(0.35f, position.z, Color.white);
            subView.transform.position = new Vector3(areaPos.x, areaPos.y, position.z);
            subView.transform.localPosition /= 0.8f;
            subView.transform.localPosition -= Vector3.forward * 0.01f;



            Vector3 posSub = subView.transform.position.normalized * (subView.transform.position.magnitude * 0.8f);

            subView.lineRenderer.SetPosition(0, new Vector3(areaPos.x, areaPos.y, subView.transform.position.z + 0.05f));
            subView.lineRenderer.SetPosition(1, new Vector3(position.x, position.y, subView.transform.position.z + 0.05f));
        }
    }
    

    public Vector2 SetArea(float radius, float z, Color color)
    {
        SquareArea area = new SquareArea(Vector2.zero, radius);

        if (freeSpaceMassive.AddInFreeSpace(area));
        else
        {
            freeSpaceMassive.NewLayout();

            if (freeSpaceMassive.AddInFreeSpace(area));
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

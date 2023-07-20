using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FreeSpaceMassive
{
    public List<SquareArea> freeSpace = new List<SquareArea>();
    private float minBorder = 0.8f;

    private SquareArea startArea;
    private SquareArea lastArea;

    public FreeSpaceMassive(SquareArea touchArea)
    {
        if (CheckMoreMinRadius(touchArea))
        {
            startArea = touchArea;
            StartLayout();
        }
        else throw new Exception();
    }

    private void StartLayout()
    {
        freeSpace.Add(startArea);
        RandomShuffle();
    }

    public void NewLayout()
    {
        freeSpace.Clear();
        
        StartLayout();
        if(lastArea != null) AddInFreeSpace(lastArea);
        else throw new Exception();
    }

    private void RandomShuffle()
    {
        var shuffledcards = freeSpace.OrderBy(a => Guid.NewGuid()).ToList();
    }

    private bool CheckMoreMinRadius(SquareArea squerArea)
    {
        if (minBorder > squerArea.width) return false;
        if (minBorder > squerArea.height) return false;

        return true;
    }

    private bool CanBeAinB(SquareArea temp, SquareArea curr)
    {
        if (temp.topLeft.x < curr.topLeft.x || temp.topLeft.y > curr.topLeft.y) return false;
        if (temp.downRight.x > curr.downRight.x || temp.downRight.y < curr.downRight.y) return false;

        return true;
    }

    private void FillAinB(SquareArea temp, SquareArea curr)
    {
        float offsetX = Mathf.Abs(temp.width - curr.width) / 2;
        float offsetY = Mathf.Abs(temp.height - curr.height) / 2;

        temp.center = curr.center;

        temp.center.x += UnityEngine.Random.Range(-offsetX, offsetX);
        temp.center.y += UnityEngine.Random.Range(-offsetY, offsetY);

        SquareArea sq1 = new SquareArea(curr.GlobalTL, new Vector2(curr.GlobalDR.x, temp.GlobalTL.y));
        if (CheckMoreMinRadius(sq1)) freeSpace.Add(sq1);

        SquareArea sq2 = new SquareArea(new Vector2(curr.GlobalTL.x, temp.GlobalDR.y), curr.GlobalDR);
        if (CheckMoreMinRadius(sq2)) freeSpace.Add(sq2);

        SquareArea sq3 = new SquareArea(new Vector2(curr.GlobalTL.x, temp.GlobalTL.y), new Vector2(temp.GlobalTL.x, temp.GlobalDR.y));
        if (CheckMoreMinRadius(sq3)) freeSpace.Add(sq3);

        SquareArea sq4 = new SquareArea(new Vector2(temp.GlobalDR.x, temp.GlobalTL.y), new Vector2(curr.GlobalDR.x, temp.GlobalDR.y));
        if (CheckMoreMinRadius(sq4)) freeSpace.Add(sq4);

    }

    public bool AddInFreeSpace(SquareArea localSquareArea)
    {
        bool canAdd = false;

        foreach (SquareArea currentSquare in freeSpace)
        {
            if (CanBeAinB(localSquareArea, currentSquare))
            {
                lastArea = localSquareArea;

                FillAinB(localSquareArea, currentSquare);
                RandomShuffle();

                canAdd = true;
                break;
            }
        }

        return canAdd;
    }
}

public class SquareArea
{
    public Vector2 center = Vector2.zero;

    public Vector2 topLeft = Vector2.zero;
    public Vector2 downRight = Vector2.zero;

    public Vector2 GlobalTL => topLeft + center;
    public Vector2 GlobalDR => downRight + center;

    public float width = 0;
    public float height = 0;


    public SquareArea(Vector2 _topLeft, Vector2 _downRight)
    {
        center = (topLeft + downRight) / 2.0f;

        topLeft = _topLeft - center;
        downRight = _downRight - center;

        AfterConstructor();
    }

    public SquareArea(Vector2 _center, float radius)
    {
        center = _center;

        Vector2 posX = new Vector2(radius, 0);
        Vector2 posY = new Vector2(0, radius);

        topLeft = center - posX + posY;
        downRight = center + posX - posY;

        AfterConstructor();
    }

    void AfterConstructor()
    {
        width = Mathf.Abs(topLeft.x - downRight.x);
        height = Mathf.Abs(topLeft.y - downRight.y);
    }
}
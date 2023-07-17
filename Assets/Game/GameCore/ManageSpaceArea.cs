using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ManageSpaceArea
{

}

public class FreeSpaceMassive
{
    private List<SquareArea> freeSpace = new List<SquareArea>();
    private float minBorder = 1.0f;

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
        AddInFreeSpace(lastArea);
    }

    private void RandomShuffle()
    {
        var shuffledcards = freeSpace.OrderBy(a => Guid.NewGuid()).ToList();
    }

    private bool CheckMoreMinRadius(SquareArea squerArea)
    {
        Vector2 vec2 = squerArea.corners[0] - squerArea.corners[1];
        if (MathF.Sqrt(minBorder) > vec2.sqrMagnitude) return false;

        vec2 = squerArea.corners[0] - squerArea.corners[3];
        if (MathF.Sqrt(minBorder) > vec2.sqrMagnitude) return false;

        return true;
    }

    private bool CanBeAinB(SquareArea temp, SquareArea curr)
    {
        if (temp.corners[0].x < curr.corners[0].x || temp.corners[0].y > curr.corners[0].y) return false; // check on left top corner
        if (temp.corners[1].x > curr.corners[1].x || temp.corners[1].y > curr.corners[1].y) return false; // check on right top corner
        if (temp.corners[2].x > curr.corners[2].x || temp.corners[2].y < curr.corners[2].y) return false; // check on right down corner
        if (temp.corners[3].x < curr.corners[3].x || temp.corners[3].y < curr.corners[3].y) return false; // check on left down corner

        return true;
    }

    private void FillAinB(SquareArea temp, SquareArea curr)
    {
        SquareArea sq1 = new SquareArea(curr.corners[0], new Vector2(curr.corners[1].x, temp.corners[1].y));
        if(CheckMoreMinRadius(sq1)) freeSpace.Add(sq1);

        SquareArea sq2 = new SquareArea(new Vector2(curr.corners[0].x, temp.corners[0].y), temp.corners[3]);
        if (CheckMoreMinRadius(sq2)) freeSpace.Add(sq2);

        SquareArea sq3 = new SquareArea(temp.corners[1], new Vector2(curr.corners[1].x, temp.corners[2].y));
        if (CheckMoreMinRadius(sq3)) freeSpace.Add(sq3);

        SquareArea sq4 = new SquareArea(new Vector2(curr.corners[0].x, temp.corners[3].y), curr.corners[3]);
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
    public Vector2[] corners = new Vector2[4]; // coners in clockwise diraction in local space

    public SquareArea(Vector2 topLeft, Vector2 downRight)
    {
        center = (topLeft + downRight) / 2.0f;

        corners[0] = topLeft;

        corners[1].x = downRight.x;
        corners[1].y = topLeft.y;

        corners[2] = downRight;

        corners[3].x = topLeft.x;
        corners[3].y = downRight.y;

        for (int i = 0; i < 4; i++)
            corners[i] -= center;
    }

    public SquareArea(Vector2 _center, float radius)
    {
        center = _center;

        Vector2 posX = new Vector2(radius, 0);
        Vector2 posY = new Vector2(0, radius);

        corners[0] = center - posX + posY;
        corners[1] = center + posX + posY;
        corners[2] = center + posX - posY;
        corners[3] = center - posX - posY;
    }
}
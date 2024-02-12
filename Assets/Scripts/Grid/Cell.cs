using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    private bool isEmptyCell;
    public bool IsEmptyCell
    {
        get
        {
            return isEmptyCell;
        }
        set
        {
            isEmptyCell = value;
        }
    }
    private int myRowOrder;
    private int myColumnOrder;

    public int MyRowOrder
    {
        get => myRowOrder;
    }

    public int MyColumnOrder
    {
        get => myColumnOrder;
    }

    private Vector3 myWorldPosition;
    public Cell(bool walkable , int row , int column)
    {
        IsEmptyCell = walkable;
        myRowOrder = row;
        myColumnOrder = column;
        SetMyWorldPosition();
    }

    public void SetMyWorldPosition()
    {
        myWorldPosition = new Vector3(myRowOrder, myColumnOrder, 0f);
    }
    public Vector3 GetMyWorldPos()
    {
        return myWorldPosition;
    }
    public Vector2Int GetMyXYCoordinates()
    {
        return new Vector2Int(myRowOrder, myColumnOrder);
    }
  

    public float GetDistanceToWorldPosition(Vector2 toWorldPosition)
    {
        return Vector3.Distance(myWorldPosition, toWorldPosition);
    }
}

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

    public Cell(bool walkable , int row , int column)
    {
        IsEmptyCell = walkable;
        myRowOrder = row;
        myColumnOrder = column;
    }
    public Vector2Int GetMyXYCoordinates()
    {
        return new Vector2Int(myRowOrder, myColumnOrder);
    }
    public Vector3 GetMyWorldPos()
    {
        return new Vector3(myRowOrder, myColumnOrder, 0f);
    }
}

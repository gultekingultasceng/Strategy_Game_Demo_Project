using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Cell
{
    #region Pathfinding_Variables
    public float G { get; private set; }
    public float H { get; private set; }
    public float F => G + H;
    public void SetConnection(Cell nodeBase) {
        Connection = nodeBase;
    }
    public void SetG(float g) {
        G = g;
    }
    public void SetH(float h) {
        H = h;
    }
    public Cell Connection { get; private set; }
    public List<Cell> Neighbors { get; private set; }
    public float GetDistance(Cell other)
    {
        Vector3 myWorldPos = GetMyWorldPos();
        Vector3 otherWorldPos = other.GetMyWorldPos();
        var dist = new Vector2Int(Mathf.Abs((int)myWorldPos.x - (int)otherWorldPos.x), Mathf.Abs((int)myWorldPos.y - (int)otherWorldPos.y));
        var lowest = Mathf.Min(dist.x, dist.y);
        var highest = Mathf.Max(dist.x, dist.y);
        var horizontalMovesRequired = highest - lowest;
        return lowest * 14 + horizontalMovesRequired * 10 ;
    }
    public  void SetNeightbors(List<Cell> neighbors)
    {
        Neighbors = neighbors;
    }
    #endregion

    public bool IsEmptyCell { get; set; }

    private readonly int _myRowOrder;
    private readonly int _myColumnOrder;

    public int MyRowOrder
    {
        get => _myRowOrder;
    }

    public int MyColumnOrder
    {
        get => _myColumnOrder;
    }

    private Vector3 _myWorldPosition;
   
    public Cell(bool walkable , int row , int column)
    {
        IsEmptyCell = walkable;
        _myRowOrder = row;
        _myColumnOrder = column;
        SetMyWorldPosition();
    }

    public void SetMyWorldPosition()
    {
        _myWorldPosition = new Vector3(_myRowOrder, _myColumnOrder, 0f);
    }
    public Vector3 GetMyWorldPos()
    {
        return _myWorldPosition;
    }
    public Vector2Int GetMyXYCoordinates()
    {
        return new Vector2Int(_myRowOrder, _myColumnOrder);
    }

 
}

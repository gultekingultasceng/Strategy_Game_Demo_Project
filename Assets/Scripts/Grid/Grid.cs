using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class Grid
{
    private readonly Cell[,] _cells;
    private readonly int _rowCount;
    private readonly int _columnCount;

    public int RowCount
    {
        get
        {
            return _rowCount;
        }
    }
    public int ColumnCount
    {
        get
        {
            return _columnCount;
        }
    }
    
    public Cell GetCellFromVector2Int(Vector2Int position)
    {
        return IsTargetCellValid(position) ? _cells[position.x, position.y] : null;
    }

    public bool IsTargetCellEmpty(Vector2Int coordinate)
    {
        return _cells[coordinate.x, coordinate.y].IsEmptyCell;
    }
    public bool IsTargetCellValid(Vector2Int coordinate)
    {
        return coordinate.x >= 0 && coordinate.x <= _rowCount - 1 && coordinate.y >= 0 &&
               coordinate.y <= _columnCount - 1;
    }
    
    private void SetCellNeighbors()
    {
        for (int i = 0; i < _rowCount; i++)
        {
            for (int j = 0; j < _columnCount; j++)
            {
                List<Cell> neighbors = new List<Cell>();
                for (int k = 0; k < GridUtils.Directions.Count; k++)
                {
                    Vector2Int neighborCoord = new Vector2Int(i, j) + GridUtils.Directions[k];
                    if (IsTargetCellValid(neighborCoord))
                    {
                        neighbors.Add(_cells[neighborCoord.x , neighborCoord.y]);
                    }
                }
                _cells[i, j].SetNeightbors(neighbors);
            }
        }
    }
    public Grid(int rowCount, int columnCount)
    {
        this._rowCount = rowCount;
        this._columnCount = columnCount;
        _cells = new Cell[rowCount, columnCount];
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                _cells[i, j] = new Cell(true, i, j);
            }
        }
        SetCellNeighbors();
    }
}

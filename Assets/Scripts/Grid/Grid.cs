using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class Grid
{
    private Cell[,] cells;
    private int rowCount , columnCount;
    
    public int RowCount
    {
        get
        {
            return rowCount;
        }
    }
    public int ColumnCount
    {
        get
        {
            return columnCount;
        }
    }
    
    public Cell GetCellFromVector2Int(Vector2Int position)
    {
        if (IsTargetCellValid(position))
        {
            return cells[position.x, position.y];
        }
        else
        {
            return null;
        }
    }

    public bool IsTargetCellEmpty(Vector2Int coordinate)
    {
        return cells[coordinate.x, coordinate.y].IsEmptyCell;
    }
    public bool IsTargetCellValid(Vector2Int coordinate)
    {
        return coordinate.x >= 0 && coordinate.x <= rowCount - 1 && coordinate.y >= 0 &&
               coordinate.y <= columnCount - 1;
    }
    
    private void SetCellNeighbors()
    {
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                List<Cell> neighbors = new List<Cell>();
                for (int k = 0; k < GridUtils.Dirs.Count; k++)
                {
                    Vector2Int neighborCoord = new Vector2Int(i, j) + GridUtils.Dirs[k];
                    if (IsTargetCellValid(neighborCoord))
                    {
                        neighbors.Add(cells[neighborCoord.x , neighborCoord.y]);
                    }
                }
                cells[i, j].SetNeightbors(neighbors);
            }
        }
    }
    public Grid(int rowCount, int columnCount)
    {
        this.rowCount = rowCount;
        this.columnCount = columnCount;
        cells = new Cell[rowCount, columnCount];
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                cells[i, j] = new Cell(true, i, j);
            }
        }
        SetCellNeighbors();
    }
}

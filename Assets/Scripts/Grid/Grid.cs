using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (position.x >= 0 && position.x <= rowCount - 1 && position.y >= 0 && position.y <= columnCount -1)
        {
            return cells[position.x, position.y];
        }
        else
        {
            return null;
        }
    }
    public List<Cell> GetNeighbours(Cell cell)
    {
        List<Cell> neighbours = new List<Cell>();

        if (cell.MyRowOrder > 0)
            neighbours.Add(cells[cell.MyRowOrder - 1, cell.MyColumnOrder]);

        if (cell.MyRowOrder < rowCount - 1) 
            neighbours.Add(cells[cell.MyRowOrder + 1, cell.MyColumnOrder]);
            
        if (cell.MyColumnOrder > 0) 
            neighbours.Add(cells[cell.MyRowOrder, cell.MyColumnOrder - 1]);

        if (cell.MyColumnOrder < columnCount - 1) 
            neighbours.Add(cells[cell.MyRowOrder, cell.MyColumnOrder + 1]);
 
        return neighbours;
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
    }
}

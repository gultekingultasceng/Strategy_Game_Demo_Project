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
    public Grid(int rowCount, int columnCount)
    {
        this.rowCount = rowCount;
        this.columnCount = columnCount;
        cells = new Cell[rowCount, columnCount];
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                cells[i, j] = new Cell(true, rowCount, this.columnCount);
            }
        }
    }
}

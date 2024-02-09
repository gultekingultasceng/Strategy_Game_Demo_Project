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
}

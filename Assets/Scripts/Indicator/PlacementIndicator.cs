using System;
using System.Collections;
using System.Collections.Generic;
using ConstantsVariables;
using UnityEngine;

public class PlacementIndicator : MonoBehaviour
{
    private Unit specifiedUnit;
    private Cell targetCell;
    [SerializeField] private bool isPlaceable;

    public Unit SpecifiedUnit
    {
        get => specifiedUnit;
    }
    public bool IsPlaceable
    {
        get => isPlaceable;
    }
    [SerializeField] private SpriteRenderer spriteRenderer;
    public void SetUnit(Unit unit)
    {
        isPlaceable = false;
        targetCell = null;
        spriteRenderer.sprite = unit._UnitUISettings.UnitIcon;
        specifiedUnit = unit;
    }
    public Vector2Int GetLastPosition()
    {
        if (targetCell != null)
        {
            if (isPlaceable)
            {
                return targetCell.GetMyXYCoordinates();
            }
            else
            {
                return new Vector2Int(-1, -1);
            }
        }
        else
        {
            return new Vector2Int(-1, -1);
        }
    }
    private void Update()
    {
        if (specifiedUnit == null)
        {
            return;
        }
        spriteRenderer.color = isPlaceable ? GameConsts.PlacementFitColor : GameConsts.PlacementErrorColor;
        targetCell = MapGenerateManager.Instance.GeneratedGrid.GetCellFromVector2Int(InputHandler.Instance.getMouseButtonData());
        if (targetCell != null)
        {
            transform.position = targetCell.GetMyWorldPos();
            isPlaceable = MapGenerateManager.Instance.IsCellBuildable(targetCell.GetMyXYCoordinates() , specifiedUnit.Width , specifiedUnit.Height);
        }
        else
        {
            isPlaceable = false;
            transform.position = InputHandler.Instance.Mousepos;
        }
    }
}

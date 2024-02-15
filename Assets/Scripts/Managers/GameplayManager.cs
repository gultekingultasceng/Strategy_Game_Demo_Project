using System.Collections;
using System.Collections.Generic;
using EventHandler;
using UnityEngine;

public class GameplayManager : Singleton<GameplayManager>
{
    protected override void Awake()
    {
        base.Awake();
    }
    [System.Serializable]
    enum Stages
    {
        Selection,
        Production
    }

    [SerializeField] private Stages stage;

    [System.Serializable]
    public enum UnitsMovemewntType
    {
        Orthogonal,
        Cardinal
    }

    [SerializeField] private UnitsMovemewntType unitsMovement;

    public UnitsMovemewntType selectedUnitMovementType
    {
        get => unitsMovement;
    }


    public void ProductionStage()
    {
        stage = Stages.Production;
    }
    public void SelectionStage()
    {
        stage = Stages.Selection;
    }

    private Unit lastSelectedUnit;
    private Unit rightClickTarget;
    public EventThrower<Unit> OnSelectUnit;
    public void Initialize()
    {
        OnSelectUnit = new EventThrower<Unit>();
        EventCatcher<Vector2Int>.Catch(InputHandler.Instance.OnLeftMouseButtonClick , LeftMouseClicked);
        EventCatcher<Vector2Int>.Catch(InputHandler.Instance.OnRightMouseButtonClick , RightMouseClicked);
        EventCatcher.Catch(MapGenerateManager.Instance.OnCreateBuilding , SelectionStage);
    }
    private void LeftMouseClicked(Vector2Int coordinate)
    {
        if (stage == Stages.Selection)
        {
            lastSelectedUnit = MapGenerateManager.Instance.IsUnitExistOnPosition(coordinate);
            OnSelectUnit.Throw(lastSelectedUnit);
        }
        else if (stage == Stages.Production)
        {
           MapGenerateManager.Instance.CreateBuilding();
        }
        else
        {
            Debug.Log("UNDEFINED STAGE !");
        }
    }
    private void RightMouseClicked(Vector2Int coordinate)
    {
        if (lastSelectedUnit is Soldier soldier)
        {
            rightClickTarget = MapGenerateManager.Instance.IsUnitExistOnPosition(coordinate);
            if (lastSelectedUnit == rightClickTarget) // IF TARGET AND SELECTED SAME POSITION
            {
                return;
            }
            if (rightClickTarget == null)
            {
                soldier.Move(PathFinder.FindPath(
                    MapGenerateManager.Instance.GeneratedGrid.GetCellFromVector2Int(soldier.MyPosition),
                    MapGenerateManager.Instance.GeneratedGrid.GetCellFromVector2Int(coordinate)
                ) , null);
            }
            else
            {
                Vector2Int nearestEmptyCellCoordinate =
                    MapGenerateManager.Instance.GetNearestEmptyCell(coordinate, soldier.Width, soldier.Height);
                soldier.Move(PathFinder.FindPath(
                    MapGenerateManager.Instance.GeneratedGrid.GetCellFromVector2Int(soldier.MyPosition),
                    MapGenerateManager.Instance.GeneratedGrid.GetCellFromVector2Int(nearestEmptyCellCoordinate)
                ) , rightClickTarget);
            }
           
        }
    }


 
   
}

using SGD.Core.EventHandler;
using SGD.Core.Pathfinding;
using UnityEngine;
using SGD.Core.Singleton;
using SGD.Core.Base;
namespace SGD.Core.Managers
{
    public class GameplayManager : Singleton<GameplayManager>
{
    protected override void Awake()
    {
        base.Awake();
    }
    [System.Serializable]
    private enum Stages
    {
        Selection,
        Production
    }

    [SerializeField] private Stages stage;

    [System.Serializable]
    public enum UnitsMovementType
    {
        Orthogonal,
        Cardinal
    }

    [SerializeField] private UnitsMovementType unitsMovement;

    public UnitsMovementType SelectedUnitMovementType
    {
        get => unitsMovement;
    }


    public void ProductionStage()
    {
        stage = Stages.Production;
    }
    
    private void SelectionStage()
    {
        stage = Stages.Selection;
    }

    private Unit _lastSelectedUnit;
    private Unit _rightClickTarget;
    public EventThrower<Unit> OnSelectUnit  = new EventThrower<Unit>();
    public void Initialize()
    {
        EventCatcher<Vector2Int>.Catch(InputHandler.Instance.OnLeftMouseButtonClick , LeftMouseClicked);
        EventCatcher<Vector2Int>.Catch(InputHandler.Instance.OnRightMouseButtonClick , RightMouseClicked);
        EventCatcher.Catch(MapGenerateManager.Instance.OnCreateBuilding , SelectionStage);
    }
    private void LeftMouseClicked(Vector2Int coordinate)
    {
        if (stage == Stages.Selection)
        {
            _lastSelectedUnit = MapGenerateManager.Instance.IsUnitExistOnPosition(coordinate);
            OnSelectUnit.Throw(_lastSelectedUnit);
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
        if (_lastSelectedUnit is Soldier soldier)
        {
            _rightClickTarget = MapGenerateManager.Instance.IsUnitExistOnPosition(coordinate);
            if (_lastSelectedUnit == _rightClickTarget) // IF TARGET AND SELECTED OBJECT SAME POSITION
            {
                return;
            }
            if (_rightClickTarget == null)
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
                ) , _rightClickTarget);
            }
           
        }
    }
}
}


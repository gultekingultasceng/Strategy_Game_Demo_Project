using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EventHandler;
using ObjectPoolingSystem;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;
using Configs;
public class MapGenerateManager : Singleton<MapGenerateManager>
{
    [SerializeField] private MapConfig mapConfig; 
    [SerializeField] private Transform gridParent;
    [SerializeField] private BuildingPool buildingPool;
    [SerializeField] private SoldierPool soldierPool;
    private Grid _generatedGrid;
    public Grid GeneratedGrid {
        get
        {
            return _generatedGrid;
        }
    }
    protected override void Awake()
    {
        base.Awake();
    }

    public readonly EventThrower OnCreateBuilding = new EventThrower();
    private readonly List<Building> _buildingListOnGrid = new List<Building>();
    private readonly List<Soldier> _soldiersListOnGrid = new List<Soldier>();
    public void CreateBuilding()
    {
        var placeIndicator = UIManager.Instance.GetPlacementIndicator();
        if (!placeIndicator.IsPlaceable)
            return;
        Unit targetUnit = placeIndicator.SpecifiedUnit;
        SetCellsNotEmptyForCoveredUnitArea(placeIndicator.GetLastPosition() , targetUnit.Width , targetUnit.Height);
        UIManager.Instance.ClosePlacementIndicator();
        Building createdUnit = buildingPool.GetObject(
            targetUnit.UniqueIDForType,
            targetUnit.gameObject, 
            VectorUtils.GetWorldPositionFromCoordinates(placeIndicator.GetLastPosition()),
            buildingPool.transform);
        createdUnit.gameObject.SetActive(true);
        _buildingListOnGrid.Add(createdUnit);
        OnCreateBuilding.Throw();
        EventCatcher<Soldier,Building,Vector2Int>.Catch(createdUnit.OnProduceSoldier, OnProduceSoldier);
        EventCatcher<Unit>.Catch(createdUnit.OnDestroy, UnitDestroyed);
    }


    public Vector2Int GetNearestEmptyCell(Vector2Int coordinate , int width , int height) // INITIAL SPAWN POINT  => coordinate
    {
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();
        queue.Enqueue(coordinate);
        visited.Add(coordinate);
        while (queue.Count > 0)
        {
            Vector2Int currentCell = queue.Dequeue();
            if (IsCellBuildable(currentCell , width , height))  // IF coordinate IS EMPTY
            {
               return currentCell;                              // JUST RETURN 
            }
            EnqueueNeighbors(currentCell, queue, visited); // ELSE SEARCH THE CLOSEST(neighbor) CELLS
        }
        return new Vector2Int(0,0);
    }
     void EnqueueNeighbors(Vector2Int cell, Queue<Vector2Int> queue, HashSet<Vector2Int> visited) // Soldier Spawn point neighbors
     {
         List<Vector2Int> neighbors = new List<Vector2Int>();
         for (int i = 0; i < GridUtils.Directions.Count; i++)
         {
             neighbors.Add(cell + GridUtils.Directions[i]);
         }
         foreach (var neighbor in neighbors)
         {
             if (_generatedGrid.IsTargetCellValid(neighbor) && !visited.Contains(neighbor))
             {
                 queue.Enqueue(neighbor);
                 visited.Add(neighbor);
             }
         }
     }
    private void OnProduceSoldier(Soldier soldier , Building fromBuild , Vector2Int targetPos)
    {
        Vector2Int targetPosition = GetNearestEmptyCell(targetPos, soldier.Width, soldier.Height);
        Soldier createdSoldier =
            soldierPool.GetObject(soldier.UniqueIDForType,soldier.gameObject,VectorUtils.GetWorldPositionFromCoordinates(targetPosition) , soldierPool.transform);
        SetCellsNotEmptyForCoveredUnitArea(targetPosition , createdSoldier.Width , createdSoldier.Height);
        createdSoldier.gameObject.SetActive(true);
        EventCatcher<Unit>.Catch(createdSoldier.OnDestroy , UnitDestroyed);
        EventCatcher<Soldier>.Catch(createdSoldier.OnSoldierMovementStart , OnSoldierBeginMovement);
        EventCatcher<Soldier>.Catch(createdSoldier.OnSoldierMovementStop , OnSoldierStopMovement);
        EventCatcher<Soldier,Vector2Int , Action<bool>>.Catch(createdSoldier.StartMoveToNextCell ,CheckCellIsValidToMove);
        _soldiersListOnGrid.Add(createdSoldier);
    }
    
    private void CheckCellIsValidToMove(Soldier soldier,Vector2Int coordinate , Action<bool> onComplete)
    {
        onComplete?.Invoke(_generatedGrid.IsTargetCellEmpty(coordinate));
    }
    private void OnSoldierBeginMovement(Soldier soldier)
    {
        SetCellsEmpty(soldier.MyPosition, soldier.Width , soldier.Height);
    }
    
    private void OnSoldierStopMovement(Soldier soldier)
    {
        SetCellsNotEmptyForCoveredUnitArea(soldier.MyPosition, soldier.Width , soldier.Height);
    }
    public void UnitDestroyed(Unit destroyedUnit)
    {
        EventCatcher<Unit>.ReleaseEvent(destroyedUnit.OnDestroy , UnitDestroyed);
        if (destroyedUnit is Building building)
        {
            if (building.CanProduceSoldier)
            {
                EventCatcher<Soldier,Building,Vector2Int>.ReleaseEvent(building.OnProduceSoldier ,OnProduceSoldier);
            }
            buildingPool.ReturnObject(building , building.UniqueIDForType);
            _buildingListOnGrid.Remove(building);
        }
        else if(destroyedUnit is Soldier soldier)
        {
            EventCatcher<Soldier>.ReleaseEvent(soldier.OnSoldierMovementStart , OnSoldierBeginMovement);
            EventCatcher<Soldier>.ReleaseEvent(soldier.OnSoldierMovementStop , OnSoldierStopMovement);
            EventCatcher<Soldier,Vector2Int , Action<bool>>.ReleaseEvent(soldier.StartMoveToNextCell ,CheckCellIsValidToMove);
            soldierPool.ReturnObject(soldier , soldier.UniqueIDForType);
            _soldiersListOnGrid.Remove(soldier);
        }
        else
        {
            Debug.LogError("UNDEFINED UNIT AND POOL");
        }
        SetCellsEmpty(destroyedUnit.MyPosition , destroyedUnit.Width , destroyedUnit.Height);
    }


    public Unit IsUnitExistOnPosition(Vector2Int coordinate)
    {
        Cell cell = GeneratedGrid.GetCellFromVector2Int(coordinate);
        IEnumerable<Unit> units = GetTotalUnitsOnGrid();
        foreach (var unit in units)
        {
            if (IsUnitContainCell(unit, cell))
            {
                return unit;
            }
        }
        return null;
    }
    private bool IsUnitContainCell(Unit unit, Cell cell) 
    {
        List<Cell> cells = new List<Cell>();
        for (int x = 0; x < unit.Width; x++)
        {
            for (int y = 0; y < unit.Height; y++)
            {
                cells.Add(GeneratedGrid.GetCellFromVector2Int(new Vector2Int(unit.MyPosition.x + x, unit.MyPosition.y + y)));
            }
        }
        return cells.Contains(cell);
    }
    private IEnumerable<Unit> GetTotalUnitsOnGrid() // LIST SOLDERS + LIST BUILDINGS
    {
        return _buildingListOnGrid.Concat<Unit>(_soldiersListOnGrid);
    }
    public void Initialize()
    {
        GenerateGrid();
    }
    public void SetCellsNotEmptyForCoveredUnitArea(Vector2Int cellPosition, int width , int height)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                _generatedGrid.GetCellFromVector2Int(new Vector2Int(cellPosition.x + i, cellPosition.y + j))
                    .IsEmptyCell = false;
            }
        }
    }
    
    public void SetCellsEmpty(Vector2Int cellPosition, int width, int height)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                _generatedGrid.GetCellFromVector2Int(new Vector2Int(cellPosition.x + i, cellPosition.y + j))
                    .IsEmptyCell = true;
            }
        }
    }
    public bool IsCellBuildable(Vector2Int cellPosition, int width , int height)
    {
        Vector2Int targetpos;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                targetpos = new Vector2Int(cellPosition.x + i, cellPosition.y + j);
                Cell targetCell = _generatedGrid.GetCellFromVector2Int(targetpos);
                if ( targetCell == null)
                {
                    return false;
                }
                if (targetCell.IsEmptyCell == false)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private void GenerateGrid()
    {
        int row = mapConfig.GridXSize;
        int column = mapConfig.GridYSize;
        _generatedGrid = new Grid(row, column);
        InputHandler.Instance.SetEdges(GeneratedGrid);
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                Instantiate(mapConfig.GroundTile.gameObject, new Vector2(i,j),Quaternion.identity, gridParent);
            }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EventHandler;
using ObjectPoolingSystem;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;
using MAP = Configs.MapConfig;
public class MapGenerateManager : Singleton<MapGenerateManager>
{
    [SerializeField] private MAP mapConfig; 
    [SerializeField] private Transform gridParent;
    [SerializeField] private BuildingPool buildingPool;
    [SerializeField] private SoldierPool soldierPool;
    private Grid generatedGrid;
    public Grid GeneratedGrid {
        get
        {
            return generatedGrid;
        }
    }
    protected override void Awake()
    {
        base.Awake();
    }

    public EventThrower OnCreateBuilding = new EventThrower();
    private List<Building> buildingListOnGrid = new List<Building>();
    private List<Soldier> soldiersListOnGrid = new List<Soldier>();
    public void CreateBuilding()
    {
        var placeIndicator = UIManager.Instance.GetPlacementIndicator();
        if (placeIndicator.IsPlaceable)
        {
            Unit targetUnit = placeIndicator.SpecifiedUnit;
            SetCellsNotEmptyForCoveredBuildArea(placeIndicator.GetLastPosition() , targetUnit.Width , targetUnit.Height);
            UIManager.Instance.ClosePlacementIndicator();
            Building createdUnit = buildingPool.GetObject(targetUnit.UniqueIDForType,targetUnit.gameObject, VectorUtils.GetWorldPositionFromCoordinates(placeIndicator.GetLastPosition()), buildingPool.transform);
            createdUnit.gameObject.SetActive(true);
            buildingListOnGrid.Add(createdUnit);
            OnCreateBuilding.Throw();
            EventCatcher<Soldier,Building,Vector2Int>.Catch(createdUnit.OnProduceSoldier, OnProduceSoldier);
            EventCatcher<Unit>.Catch(createdUnit.OnDestroy, UnitDestroyed);
           
        }
    }
     Vector2Int GetNearestEmptyCell(Vector2Int coordinate , int width , int height) // INITIAL SPAWN POINT  => coordinate
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
         for (int i = 0; i < GridUtils.Dirs.Count; i++)
         {
             neighbors.Add(cell + GridUtils.Dirs[i]);
         }
         foreach (var neighbor in neighbors)
         {
             if (generatedGrid.IsTargetCellValid(neighbor) && !visited.Contains(neighbor))
             {
                 queue.Enqueue(neighbor);
                 visited.Add(neighbor);
             }
         }
     }
    private void OnProduceSoldier(Soldier soldier , Building fromBuild , Vector2Int targetPos)
    {
        Vector2Int _targetPos = GetNearestEmptyCell(targetPos, soldier.Width, soldier.Height);
        Soldier createdSoldier =
            soldierPool.GetObject(soldier.UniqueIDForType,soldier.gameObject,VectorUtils.GetWorldPositionFromCoordinates(_targetPos) , soldierPool.transform);
        SetCellsNotEmptyForCoveredBuildArea(_targetPos , createdSoldier.Width , createdSoldier.Height);
        EventCatcher<Unit>.Catch(createdSoldier.OnDestroy , UnitDestroyed);
        soldiersListOnGrid.Add(createdSoldier);
    }
    public void UnitDestroyed(Unit destroyedUnit)
    {
        EventCatcher<Unit>.ReleaseEvent(destroyedUnit.OnDestroy , UnitDestroyed);
        if (destroyedUnit is Building building)
        {
            buildingPool.ReturnObject(building , building.UniqueIDForType);
        }
        else if(destroyedUnit is Soldier soldier)
        {
            soldierPool.ReturnObject(soldier , soldier.UniqueIDForType);
        }
        else
        {
            Debug.LogError("UNDEFINED UNIT AND POOL");
        }
        SetCellsEmptyForDestroyedUnitCoveredArea(destroyedUnit.MyPosition , destroyedUnit.Width , destroyedUnit.Height);
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
        return buildingListOnGrid.Concat<Unit>(soldiersListOnGrid);
    }
    public void Initialize()
    {
        GenerateGrid();
    }
    public void SetCellsNotEmptyForCoveredBuildArea(Vector2Int cellPosition, int width , int height)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                generatedGrid.GetCellFromVector2Int(new Vector2Int(cellPosition.x + i, cellPosition.y + j))
                    .IsEmptyCell = false;
            }
        }
    }
    
    public void SetCellsEmptyForDestroyedUnitCoveredArea(Vector2Int cellPosition, int width, int height)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                generatedGrid.GetCellFromVector2Int(new Vector2Int(cellPosition.x + i, cellPosition.y + j))
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
                Cell targetcell = generatedGrid.GetCellFromVector2Int(targetpos);
                if ( targetcell == null)
                {
                    return false;
                }
                if (targetcell.IsEmptyCell == false)
                {
                    return false;
                }
            }
        }
        return true;
    }
    public void GenerateGrid()
    {
        int row = mapConfig.GridXSize;
        int column = mapConfig.GridYSize;
        generatedGrid = new Grid(row, column);
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
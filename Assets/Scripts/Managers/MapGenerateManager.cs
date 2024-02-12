using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EventHandler;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;
using MAP = Configs.MapConfig;
public class MapGenerateManager : Singleton<MapGenerateManager>
{
    [SerializeField] private MAP mapConfig; 
    [SerializeField] private Transform gridParent;
    [SerializeField] private BuildingFactory buildingFactory;
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
            Building createdUnit = buildingFactory.Create(targetUnit.gameObject, VectorUtils.GetWorldPositionFromCoordinates(placeIndicator.GetLastPosition()), this.transform);
            createdUnit.Initialize();
            buildingListOnGrid.Add(createdUnit);
            OnCreateBuilding.Throw();
        }
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
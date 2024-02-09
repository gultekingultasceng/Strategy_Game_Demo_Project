using System;
using System.Collections;
using System.Collections.Generic;
using EventHandler;
using UnityEngine;
using UnityEngine.Serialization;
using MAP = Configs.MapConfig;
public class MapGenerateManager : Singleton<MapGenerateManager>
{
    [SerializeField] private MAP mapConfig; 
    [SerializeField] private Transform gridParent;
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

    public void Initialize()
    {
        GenerateGrid();
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
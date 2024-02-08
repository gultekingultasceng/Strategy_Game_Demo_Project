using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MAP = Configs.MapConfig;
public class GridCreator : MonoBehaviour
{
    [SerializeField] private MAP mapConfig;
    [SerializeField] private Transform tilesParent;
    public void GenerateGrid()
    {
        int row = mapConfig.GridXSize;
        int column = mapConfig.GridYSize;
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                Instantiate(mapConfig.GroundTile.gameObject, new Vector2(i,j),Quaternion.identity, tilesParent);
            }
        }
    }

    public void MoveCamToCenter()
    {
        Camera.main.transform.position = new Vector3(mapConfig.GridXSize * .5f, mapConfig.GridYSize * .5f,
            Camera.main.transform.position.z);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateGrid();
            MoveCamToCenter();
        }
    }
}

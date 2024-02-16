using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
    public static class PathFinder { 
    public static List<Cell> FindPath(Cell startCell, Cell targetCell) 
    {
        var toSearch = new List<Cell>() { startCell };
        var processed = new List<Cell>();
        while (toSearch.Any()) {
            var current = toSearch[0];
            foreach (var t in toSearch) 
                if (t.F < current.F || t.F == current.F && t.H < current.H) current = t;
            processed.Add(current);
            toSearch.Remove(current);
            if (current == targetCell) {
                var currentPathTile = targetCell;
                var path = new List<Cell>();
                var count = 100;
                while (currentPathTile != startCell) {
                    path.Add(currentPathTile);
                    currentPathTile = currentPathTile.Connection;
                    count--;
                    if (count < 0) throw new Exception();
                }
                return path;
            }
            foreach (var neighbor in current.Neighbors.Where(t => t.IsEmptyCell && !processed.Contains(t))) {
                if (GameplayManager.Instance.SelectedUnitMovementType == 
                    GameplayManager.UnitsMovementType.Orthogonal) // this condition should set as static bool once for performance.
                {                                                   // The reason I did it this way is so you can see the change in runtime.
                    if (IsHorizontalOrVerticalMove(current,neighbor))
                    {
                        var inSearch = toSearch.Contains(neighbor);
                        var costToNeighbor = current.G + current.GetDistance(neighbor);
                        if (!inSearch || costToNeighbor < neighbor.G) {
                            neighbor.SetG(costToNeighbor);
                            neighbor.SetConnection(current);
                            if (!inSearch) {
                                neighbor.SetH(neighbor.GetDistance(targetCell));
                                toSearch.Add(neighbor);
                            }
                        }
                    }
                }
                else if (GameplayManager.Instance.SelectedUnitMovementType == 
                         GameplayManager.UnitsMovementType.Cardinal)
                {
                    var inSearch = toSearch.Contains(neighbor);
                    var costToNeighbor = current.G + current.GetDistance(neighbor);
                    if (!inSearch || costToNeighbor < neighbor.G) {
                        neighbor.SetG(costToNeighbor);
                        neighbor.SetConnection(current);
                        if (!inSearch) {
                            neighbor.SetH(neighbor.GetDistance(targetCell));
                            toSearch.Add(neighbor);
                        }
                    }
                }
                else
                {
                    Debug.LogError("UNDEFINED MOVEMENT TYPE ! ");
                }
               
            
            }
        }
        return null;
    }
    private static bool IsHorizontalOrVerticalMove(Cell current, Cell neighbor) {
        return Mathf.Abs(current.MyRowOrder - neighbor.MyRowOrder) + Mathf.Abs(current.MyColumnOrder - neighbor.MyColumnOrder) == 1;
    }
    }

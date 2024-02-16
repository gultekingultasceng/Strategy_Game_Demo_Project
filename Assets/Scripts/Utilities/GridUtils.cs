using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class GridUtils
    {
        public static readonly List<Vector2Int> Directions = new List<Vector2Int>() {
            new Vector2Int(0, 1), new Vector2Int(-1, 0), new Vector2Int(0, -1), new Vector2Int(1, 0),
            new Vector2Int(1, 1), new Vector2Int(1, -1), new Vector2Int(-1, -1), new Vector2Int(-1, 1)
        };
    }
}


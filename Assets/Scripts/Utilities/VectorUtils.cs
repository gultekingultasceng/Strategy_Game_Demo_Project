using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class VectorUtils
    {
        public static Vector2 GetWorldPositionFromMousePos(Camera cam)
        {
            return cam.ScreenToWorldPoint(Input.mousePosition);
        }
        public static Vector2Int GetVector2Int(Vector3 worldpos)
        {
            return new Vector2Int(Mathf.FloorToInt(worldpos.x), Mathf.FloorToInt(worldpos.y));
        }

        public static Vector3 GetWorldPositionFromCoordinates(Vector2Int coordinates)
        {
            return new Vector3(coordinates.x, coordinates.y, 0f);
        }
    }
}


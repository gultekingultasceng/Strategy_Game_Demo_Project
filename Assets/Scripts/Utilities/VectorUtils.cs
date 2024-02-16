using UnityEngine;

namespace SGD.Core.Utilities
{
    public static class VectorUtils
    {
        public static Vector2 GetWorldPositionFromMousePos(Camera cam)
        {
            return cam.ScreenToWorldPoint(Input.mousePosition);
        }
        public static Vector2Int GetVector2Int(Vector3 worldPosition)
        {
            return new Vector2Int(Mathf.FloorToInt(worldPosition.x), Mathf.FloorToInt(worldPosition.y));
        }

        public static Vector3 GetWorldPositionFromCoordinates(Vector2Int coordinates)
        {
            return new Vector3(coordinates.x, coordinates.y, 0f);
        }
    }
}


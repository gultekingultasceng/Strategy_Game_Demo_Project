using SGD.Core.Constants;
using SGD.Core.Managers;
using SGD.Core.Pathfinding;
using UnityEngine;

namespace SGD.Core.Base
{
    public class PlacementIndicator : MonoBehaviour
    {
        private Unit _specifiedUnit;
        private Cell _targetCell;
        [SerializeField] private bool isPlaceable;
    
        public Unit SpecifiedUnit
        {
            get => _specifiedUnit;
        }
        public bool IsPlaceable
        {
            get => isPlaceable;
        }
        [SerializeField] private SpriteRenderer spriteRenderer;
        public void SetUnit(Unit unit)
        {
            isPlaceable = false;
            _targetCell = null;
            spriteRenderer.sprite = unit.UnitUISettings.UnitIcon;
            _specifiedUnit = unit;
        }
        public Vector2Int GetLastPosition()
        {
            if (_targetCell != null)
            {
                if (isPlaceable)
                {
                    return _targetCell.GetMyXYCoordinates();
                }
                else
                {
                    return new Vector2Int(-1, -1);
                }
            }
            else
            {
                return new Vector2Int(-1, -1);
            }
        }
        private void Update()
        {
            if (_specifiedUnit)
            {
                spriteRenderer.color = isPlaceable ? GameConstants.PlacementFitColor : GameConstants.PlacementErrorColor;
                _targetCell = MapGenerateManager.Instance.GeneratedGrid.GetCellFromVector2Int(InputHandler.Instance.GetMouseButtonData());
                if (_targetCell != null)
                {
                    transform.position = _targetCell.GetMyWorldPos();
                    isPlaceable = MapGenerateManager.Instance.IsCellBuildable(_targetCell.GetMyXYCoordinates() , _specifiedUnit.Width , _specifiedUnit.Height);
                }
                else
                {
                    isPlaceable = false;
                    transform.position = InputHandler.Instance.MousePosition;
                }
            }
           
        }
    }
}


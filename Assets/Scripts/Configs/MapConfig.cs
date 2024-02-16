using UnityEngine;
namespace SGD.Core.Configs
{
    [CreateAssetMenu(fileName = "MapConfig", menuName = "Configs/MapConfig")]
    public class MapConfig : ScriptableObject
    {
        [SerializeField] private int gridXSize;
        [SerializeField] private int gridYSize;
        [SerializeField] private Transform groundTile;
        public int GridXSize { get => gridXSize; }
        public int GridYSize { get => gridYSize;}
        public Transform GroundTile { get => groundTile; }
    }
}


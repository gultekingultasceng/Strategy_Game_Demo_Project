using System.Collections.Generic;
using SGD.Core.Base;
using UnityEngine;



namespace SGD.Core.Behaviours
{
    [CreateAssetMenu(fileName = "ProduceSoldierBehaviour", menuName = "Behaviours/Buildings/ProduceSoldierBehaviour")]
    public class ProduceSoldierBehaviour : ScriptableObject
    {
        public List<Soldier> ProduceableList = new List<Soldier>();
        [System.Serializable]
        public enum SoldierSpawnDirection
        {
            Left,
            Right,
            Top,
            Bot
        }
        public SoldierSpawnDirection SpawnDirection;
    }
}


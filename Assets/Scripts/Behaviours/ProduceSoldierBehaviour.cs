using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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

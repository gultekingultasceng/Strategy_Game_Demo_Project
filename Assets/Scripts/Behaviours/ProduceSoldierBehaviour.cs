using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProduceSoldierBehaviour", menuName = "Behaviours/Buildings/ProduceSoldierBehaviour")]
public class ProduceSoldierBehaviour : ScriptableObject
{
    public List<Soldier> producableList = new List<Soldier>();
    [System.Serializable]
    public enum soldierSpawnDirection
    {
        left,
        right,
        top,
        bot
    }

     public soldierSpawnDirection spawnDirection;
}

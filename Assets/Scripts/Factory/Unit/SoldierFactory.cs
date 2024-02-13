using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierFactory : Factory<Soldier,GameObject,Vector3,Transform>
{
    public override Soldier Create(GameObject unitGO, Vector3 worldPosition, Transform parent)
    {
        return Instantiate(unitGO, worldPosition, Quaternion.identity, parent).GetComponent<Soldier>();
    }
}

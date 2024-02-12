using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFactory : Factory<Building,GameObject,Vector3,Transform>
{
    public override Building Create(GameObject unitGO ,Vector3 worldPosition, Transform parent)
    {
        return Instantiate(unitGO, worldPosition, Quaternion.identity, parent).GetComponent<Building>();
    }
}

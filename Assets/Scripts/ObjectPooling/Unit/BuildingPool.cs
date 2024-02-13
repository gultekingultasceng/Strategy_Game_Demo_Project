using System.Collections;
using System.Collections.Generic;
using ObjectPoolingSystem;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingPool : Pool<Building,int,GameObject,Vector3,Transform>
{
    [SerializeField] private BuildingFactory factory;
    public override Factory<Building,GameObject, Vector3, Transform> Factory { get => factory;
        set => throw new System.NotImplementedException();
    }
}

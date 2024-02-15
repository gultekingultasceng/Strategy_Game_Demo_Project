using System.Collections;
using System.Collections.Generic;
using ObjectPoolingSystem;
using UnityEngine;

public class SoldierCreateButtonPool : Pool<SoldierCreateButton , GameObject , Transform>
{
    [SerializeField] private Factory<SoldierCreateButton, GameObject, Transform> soldierFactory;
    public override Factory<SoldierCreateButton, GameObject, Transform> Factory { get => soldierFactory; set => throw new System.NotImplementedException(); }
}

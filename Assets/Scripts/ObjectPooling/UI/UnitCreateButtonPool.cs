using System.Collections;
using System.Collections.Generic;
using ObjectPoolingSystem;
using UnityEngine;

public class UnitCreateButtonPool : Pool<UnitCreateButton,GameObject,Transform>
{
    [SerializeField] private UnitCreateButtonFactory factory;
    public override Factory<UnitCreateButton, GameObject, Transform> Factory { get => factory;
        set => throw new System.NotImplementedException();
    }
}

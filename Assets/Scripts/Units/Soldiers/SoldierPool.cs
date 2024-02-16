using SGD.Core.ObjectPooling;
using SGD.Core.Factory;
using UnityEngine;

namespace SGD.Core.Base
{
    public class SoldierPool : Pool<Soldier,int,GameObject,Vector3,Transform>
    {
        [SerializeField] private SoldierFactory factory;
        public override Factory<Soldier, GameObject, Vector3, Transform> Factory { get => factory; set => throw new System.NotImplementedException(); }
    }
}


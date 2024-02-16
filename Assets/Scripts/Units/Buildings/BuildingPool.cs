using SGD.Core.ObjectPooling;
using SGD.Core.Factory;
using UnityEngine;

namespace SGD.Core.Base
{
    public class BuildingPool : Pool<Building,int,GameObject,Vector3,Transform>
    {
        [SerializeField] private BuildingFactory factory;
        public override Factory<Building,GameObject, Vector3, Transform> Factory { get => factory;
            set => throw new System.NotImplementedException();
        }
    }
}


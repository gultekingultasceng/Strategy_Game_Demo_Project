using SGD.Core.ObjectPooling;
using SGD.Core.Factory;
using UnityEngine;

namespace SGD.Core.UI
{
    public class SoldierCreateButtonPool : Pool<SoldierCreateButton , GameObject , Transform>
    {
        [SerializeField] private Factory<SoldierCreateButton, GameObject, Transform> soldierFactory;
        public override Factory<SoldierCreateButton, GameObject, Transform> Factory { get => soldierFactory; set => throw new System.NotImplementedException(); }
    }
}


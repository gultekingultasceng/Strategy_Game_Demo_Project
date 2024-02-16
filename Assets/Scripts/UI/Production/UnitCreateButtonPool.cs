using SGD.Core.ObjectPooling;
using SGD.Core.Factory;
using UnityEngine;

namespace SGD.Core.UI
{
    public class UnitCreateButtonPool : Pool<UnitCreateButton,GameObject,Transform>
    {
        [SerializeField] private UnitCreateButtonFactory factory;
        public override Factory<UnitCreateButton, GameObject, Transform> Factory { get => factory;
            set => throw new System.NotImplementedException();
        }
    }
}


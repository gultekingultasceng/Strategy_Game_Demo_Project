using SGD.Core.Factory;
using UnityEngine;

namespace SGD.Core.Base
{
    public class SoldierFactory : Factory<Soldier,GameObject,Vector3,Transform>
    {
        public override Soldier Create(GameObject unitGameObject, Vector3 worldPosition, Transform parent)
        {
            return Instantiate(unitGameObject, worldPosition, Quaternion.identity, parent).GetComponent<Soldier>();
        }
    }
}


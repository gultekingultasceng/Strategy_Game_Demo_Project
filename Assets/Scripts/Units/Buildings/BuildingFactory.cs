using SGD.Core.Factory;
using UnityEngine;

namespace SGD.Core.Base
{
    public class BuildingFactory : Factory<Building,GameObject,Vector3,Transform>
    {
        public override Building Create(GameObject unitGameObject ,Vector3 worldPosition, Transform parent)
        {
            return Instantiate(unitGameObject, worldPosition, Quaternion.identity, parent).GetComponent<Building>();
        }
    }
}


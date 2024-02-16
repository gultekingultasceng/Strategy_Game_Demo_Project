using SGD.Core.Factory;
using UnityEngine;
namespace SGD.Core.UI
{
    public class ProducableSoldierButtonFactory : Factory<SoldierCreateButton,GameObject,Transform>
    {
        public override SoldierCreateButton Create(GameObject button, Transform parent)
        {
            return Instantiate(button, parent).GetComponent<SoldierCreateButton>();
        }
    }
}


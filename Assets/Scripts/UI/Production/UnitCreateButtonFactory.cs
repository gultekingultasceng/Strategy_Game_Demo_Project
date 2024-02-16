using SGD.Core.Factory;
using UnityEngine;

namespace SGD.Core.UI
{
    public class UnitCreateButtonFactory : Factory<UnitCreateButton , GameObject , Transform>
    {
        public override UnitCreateButton Create(GameObject button, Transform parent)
        {
            return Instantiate(button,parent).GetComponent<UnitCreateButton>();
        }
    }
}


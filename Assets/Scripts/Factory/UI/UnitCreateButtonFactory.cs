using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCreateButtonFactory : Factory<UnitCreateButton , GameObject , Transform>
{
    public override UnitCreateButton Create(GameObject button, Transform parent)
    {
        return Instantiate(button,parent).GetComponent<UnitCreateButton>();
    }
}

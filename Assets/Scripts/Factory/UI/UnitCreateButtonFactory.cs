using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCreateButtonFactory : Factory<UnitCreateButton , GameObject , Transform>
{
    public override UnitCreateButton Create(GameObject Button, Transform parent)
    {
        return Instantiate(Button,parent).GetComponent<UnitCreateButton>();
    }
}

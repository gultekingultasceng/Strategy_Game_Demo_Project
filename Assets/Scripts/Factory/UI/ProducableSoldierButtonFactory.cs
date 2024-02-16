using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProducableSoldierButtonFactory : Factory<SoldierCreateButton,GameObject,Transform>
{
    public override SoldierCreateButton Create(GameObject button, Transform parent)
    {
        return Instantiate(button, parent).GetComponent<SoldierCreateButton>();
    }
}

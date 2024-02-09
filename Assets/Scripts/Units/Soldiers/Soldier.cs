using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SoldierUISettings))]
public class Soldier : Unit
{
    [SerializeField]private SoldierUISettings soldierUISettings;

    public SoldierUISettings _SoldierUISettings
    {
        get
        {
            return soldierUISettings;
        }
        set
        {
            soldierUISettings = value;
        }
    }
}

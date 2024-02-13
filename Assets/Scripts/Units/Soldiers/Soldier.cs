using System.Collections;
using System.Collections.Generic;
using ObjectPoolingSystem;
using UnityEngine;
using Utilities;

[RequireComponent(typeof(SoldierUISettings))]
public class Soldier : Unit , IEnableDisable<Vector3>
{
    private SoldierUISettings soldierUISettings;
    
    public SoldierUISettings _SoldierUISettings
    {
        get
        {
            if (soldierUISettings != null)
            {
                return soldierUISettings;
            }
            else
            {
                soldierUISettings = GetComponent<SoldierUISettings>();
                return soldierUISettings;
            }
        }
    }

    
    /*

     */


    public void PerformOnEnable(Vector3 parameter1)
    {
        initialHealth = _UnitConfig.Health;
        currentHealth = initialHealth;     // Get initial health from config
        _SoldierUISettings.SetDefault();
        MyPosition = VectorUtils.GetVector2Int(parameter1);
    }

    public void PerformDisable()
    {
        this.gameObject.SetActive(false);
    }
}

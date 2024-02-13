using System.Collections;
using System.Collections.Generic;
using ObjectPoolingSystem;
using UnityEngine;

[RequireComponent(typeof(SoldierUISettings))]
public class Soldier : Unit , IEnableDisable
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
    public void PerformOnEnable()
    {
        initialHealth = _UnitConfig.Health;
        currentHealth = initialHealth;     // Get initial health from config
        _SoldierUISettings.SetDefault();
    }

    public void PerformOnDisable()
    {
       this.gameObject.SetActive(false);
    }
}

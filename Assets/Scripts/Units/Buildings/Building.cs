using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

[RequireComponent(typeof(BuildingUISettings))]
public class Building : Unit , IEnableDisable<Vector3>
{
    
    private BuildingUISettings buildingUISettings;
    [SerializeField] private List<Soldier> producableList = new List<Soldier>();

    public List<Soldier> ProducableList
    {
        get
        {
            return producableList;
        }
    }
    public BuildingUISettings _BuildingUISettings
    {
        get
        {
            if (buildingUISettings != null)
            {
                return buildingUISettings;
            }
            else
            {
                buildingUISettings = GetComponent<BuildingUISettings>();
                return buildingUISettings;
            }
        }
    }

    public void CreateSoldier(Soldier targetSoldier)
    {
        
    }
    /*

     */


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            DestroyMe();
        }
    }

    public void PerformOnEnable(Vector3 parameter1)
    {
        initialHealth = _UnitConfig.Health;
        currentHealth = initialHealth;     // Get initial health from config
        _BuildingUISettings.SetDefault();
        MyPosition = VectorUtils.GetVector2Int(parameter1);
    }
    
    public void PerformDisable()
    {
        this.gameObject.SetActive(false);
    }
}

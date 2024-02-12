using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

[RequireComponent(typeof(BuildingUISettings))]
public class Building : Unit
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
    public void Initialize()
    {
        initialHealth = _UnitConfig.Health;
        currentHealth = initialHealth;     // Get initial health from config
        _BuildingUISettings.SetDefault();
        MyPosition = VectorUtils.GetVector2Int(transform.position);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Configs;
using UnityEngine;
using Utilities;

[RequireComponent(typeof(UnitUISettings))]
public class Unit : MonoBehaviour
{
    [SerializeField] public UnitConfig _UnitConfig;
    private UnitUISettings unitUISettings;
    
    public UnitUISettings _UnitUISettings
    {
        get
        {
            if (unitUISettings != null)
            {
                return unitUISettings;
            }
            else
            {
                unitUISettings = GetComponent<UnitUISettings>();
                return unitUISettings;
            }
            
        }
    }
    
    [SerializeField] protected int width; // Set these manually from inspector to make it more suitable for various situations
                                         // But we can set it with Sprite resolution divided by pixel per unit Count (cell size)
    public int Width 
    {
        get
        {
            return width;
        }
    } // Read-only
    [SerializeField] protected int height;
    public int Height 
    {
        get
        {
            return height;
        }
    }

    protected int initialHealth;
    protected int currentHealth;
    public int CurrentHealth
    {
        set
        {
            currentHealth = value;
        }
        get
        {
            return currentHealth;
        }
    }
    protected Vector2Int myPosition;
    public Vector2Int MyPosition
    {
        get
        {
            return myPosition;
        }
        set
        {
            myPosition = value;
            MoveToPosition(myPosition);
        }
    }
    
    private void Awake()
    {
        unitUISettings = GetComponent<UnitUISettings>();
    }

    private void MoveToPosition(Vector2Int coordinate)
    {
        transform.position = new Vector3(coordinate.x, coordinate.y, 0f);
    }
    public void UnderAttack(int damagePoint)
    {
        CurrentHealth -= damagePoint;
        if (CurrentHealth > 0)
        {
            unitUISettings.GetDamageEffect();
        }
        else
        {
            // DEAD
        }
    }
    
}

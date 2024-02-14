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
    private Coroutine movementCoroutine;
    public void Move(List<Cell> path)
    {
        if (movementCoroutine != null)
        {
            StopCoroutine(movementCoroutine);
        }
        movementCoroutine =  StartCoroutine(Movevement(path));
    }
    public IEnumerator Movevement(List<Cell> path)
    {
        path.Reverse();
        for (int i = 0; i < path.Count; i++)
        {
            MyPosition = path[i].GetMyXYCoordinates();
            yield return new WaitForSeconds(.2f);
        }
    }
    public void PerformOnEnable(Vector3 parameter1)
    {
        initialHealth = _UnitConfig.Health;
        currentHealth = initialHealth;     // Get initial health from config
        _SoldierUISettings.SetDefault();
        MyPosition = VectorUtils.GetVector2Int(parameter1);
    }
    
    public void PerformDisable()
    {
        if (movementCoroutine != null)
        {
            StopCoroutine(movementCoroutine);
        }
        this.gameObject.SetActive(false);
    }
}

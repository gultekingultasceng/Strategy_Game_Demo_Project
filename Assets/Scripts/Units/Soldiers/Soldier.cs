using System.Collections;
using System.Collections.Generic;
using EventHandler;
using ObjectPoolingSystem;
using UnityEngine;
using Utilities;
using System;
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

    #region Movement
    [SerializeField] private float movementSpeed;
    public EventThrower<Soldier> OnSoldierMovementStart = new EventThrower<Soldier>();
    public EventThrower<Soldier> OnSoldierMovementStop = new EventThrower<Soldier>();
    public EventThrower<Soldier,Vector2Int,Action<bool>> StartMoveToNextCell = new EventThrower<Soldier,Vector2Int,Action<bool>>();
    private Coroutine movementCoroutine;
    private bool isNextTargetCellToMoveStillEmpty = true;

    public void SetNextCellMoveable(bool isAvailable)
    {
        Debug.Log("checking result" + isAvailable);
        isNextTargetCellToMoveStillEmpty = isAvailable;
    }
    public void Move(List<Cell> path , Unit targetUnit)
    {
        StopMovement();
        OnSoldierMovementStart.Throw(this);
        movementCoroutine =  StartCoroutine(Movevement(path , targetUnit));
    }
    
 
    private IEnumerator Movevement(List<Cell> path , Unit targetUnit)
    {
        path.Reverse();
        for (int i = 0; i < path.Count; i++)
        {
            float timer = 0f;
            Vector2Int targetCoordinate = path[i].GetMyXYCoordinates();
            Vector3 targetWorldPos = VectorUtils.GetWorldPositionFromCoordinates(targetCoordinate);
            Vector3 startPos = transform.position;
            float estimatedTime = Vector3.Distance(startPos , targetWorldPos) / movementSpeed;
            StartMoveToNextCell.Throw(this,targetCoordinate,SetNextCellMoveable);
            while (timer < estimatedTime)
            {
                transform.position = Vector3.Lerp(startPos, targetWorldPos, (timer / estimatedTime));
                timer += Time.deltaTime;
                if (!isNextTargetCellToMoveStillEmpty)
                {
                    StopMovement(); // IF LATER THE TARGET CELL FILLED, THEN STOP
                }
                yield return null;
            }
            yield return null;
            MyPosition = targetCoordinate;
        }
        StopMovement();
        if (targetUnit != null)
        {
            AttackToUnit(targetUnit);
        }
    }
    private void StopMovement()
    {
        OnSoldierMovementStop.Throw(this);
        if (movementCoroutine != null)
        {
            StopCoroutine(movementCoroutine);
        }
        movementCoroutine = null;
    }
    #endregion

    #region Attack
    [SerializeField] private int damagePoint;
    [SerializeField] private float attackSpeed;
    private Coroutine attackCoroutine;
    private IEnumerator Attack(Unit targetUnit)
    {
        int damage = 5;
        float attackspeed = 5; // 1-10
        if (targetUnit != null)
        {
            targetUnit.UnderAttack(5);
            yield return new WaitForSeconds(1 / attackspeed);
        }
        else
        {
            yield return null;
        }
    }
    private void AttackToUnit(Unit targetUnit)
    {
        StopAttack();
        attackCoroutine = StartCoroutine(Attack(targetUnit));
    }

    private void StopAttack()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }
        attackCoroutine = null;
    }

    #endregion
   
   
    

   
   
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

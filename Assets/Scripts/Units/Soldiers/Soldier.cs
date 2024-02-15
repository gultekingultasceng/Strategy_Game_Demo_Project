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
        isNextTargetCellToMoveStillEmpty = isAvailable;
    }
    public void Move(List<Cell> path , Unit targetUnit)
    {
        StopAttack();
        StopMovement();
        if (path == null)
        {
            return;
        }
        
        bool isTargetUnitExist = targetUnit != null;
        path.Reverse();
        if (path.Count >= attackRange)
        {
            for (int i = 0; i < attackRange ; i++)
            {
                path.Remove(path[^1]);
            }
        }
        if (isTargetUnitExist)
        {
            if (!isTargetUnitInMyAttackRange(targetUnit.MyPosition))
            {
                movementCoroutine =  StartCoroutine(Movevement(path , targetUnit));
            }
            else
            {
                AttackToUnit(targetUnit);
            }
        }
        else
        {
            movementCoroutine =  StartCoroutine(Movevement(path , targetUnit));
        }
    }


    bool isTargetUnitInMyAttackRange(Vector2Int target)
    {
        return Mathf.Abs(target.x - MyPosition.x) + Mathf.Abs(target.y - myPosition.y) <= attackRange;
    }
    private IEnumerator Movevement(List<Cell> path , Unit targetUnit)
    {
        OnSoldierMovementStart.Throw(this);
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
    [SerializeField] [Range(1 , 5)] private float attackSpeed;
    [SerializeField][Range(0,3)] private int attackRange;
    [SerializeField] private Unit targetUnitToAttack;
    private Coroutine attackCoroutine;
    private IEnumerator Attack(Unit targetUnit)
    {
        targetUnitToAttack = targetUnit;
        while (targetUnitToAttack is {IsDestroyed: false}) // if targetUnit is not null and still exist
        {
            targetUnitToAttack.UnderAttack(damagePoint);
            if (targetUnitToAttack is {IsDestroyed: false})
            {
                yield return new WaitForSeconds(1 / attackSpeed);
            }
        }
        StopAttack();
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
        targetUnitToAttack = null;
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
        StopMovement();
        StopAttack();
        this.gameObject.SetActive(false);
    }
}

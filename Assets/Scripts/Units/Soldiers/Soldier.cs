using System.Collections;
using System.Collections.Generic;
using SGD.Core.EventHandler;
using UnityEngine;
using SGD.Core.Utilities;
using System;
using SGD.Core.ObjectPooling;
using SGD.Core.Pathfinding;

namespace SGD.Core.Base
{
    [RequireComponent(typeof(SoldierUISettings))]
public class Soldier : Unit , IEnableDisable<Vector3>
{
    private SoldierUISettings _soldierUISettings;
    
    public SoldierUISettings SoldierUISettings
    {
        get
        {
            if (_soldierUISettings != null)
            {
                return _soldierUISettings;
            }
            else
            {
                _soldierUISettings = GetComponent<SoldierUISettings>();
                return _soldierUISettings;
            }
        }
    }

    #region Movement
    [SerializeField] private float movementSpeed;
    public readonly EventThrower<Soldier> OnSoldierMovementStart = new EventThrower<Soldier>();
    public readonly EventThrower<Soldier> OnSoldierMovementStop = new EventThrower<Soldier>();
    public readonly EventThrower<Soldier,Vector2Int,Action<bool>> StartMoveToNextCell = new EventThrower<Soldier,Vector2Int,Action<bool>>();
    private Coroutine _movementCoroutine;
    private bool _isNextTargetCellToMoveStillEmpty = true;

    public void SetNextCellMoveable(bool isAvailable)
    {
        _isNextTargetCellToMoveStillEmpty = isAvailable;
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
            if (!IsTargetUnitInMyAttackRange(targetUnit.MyPosition))
            {
                _movementCoroutine =  StartCoroutine(Movement(path , targetUnit));
            }
            else
            {
                AttackToUnit(targetUnit);
            }
        }
        else
        {
            _movementCoroutine =  StartCoroutine(Movement(path , targetUnit));
        }
    }
    bool IsTargetUnitInMyAttackRange(Vector2Int target)
    {
        return Mathf.Abs(target.x - MyPosition.x) + Mathf.Abs(target.y - myPosition.y) <= attackRange;
    }
    private IEnumerator Movement(List<Cell> path , Unit targetUnit)
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
                if (!_isNextTargetCellToMoveStillEmpty)
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
        if (_movementCoroutine != null)
        {
            StopCoroutine(_movementCoroutine);
        }
        _movementCoroutine = null;
    }
    #endregion
    #region Attack
    [SerializeField] private int damagePoint;
    [SerializeField] [Range(1 , 5)] private float attackSpeed;
    [SerializeField][Range(0,3)] private int attackRange;
    private Unit _targetUnitToAttack;
    private Coroutine _attackCoroutine;
    private IEnumerator Attack(Unit targetUnit)
    {
        _targetUnitToAttack = targetUnit;
        while (_targetUnitToAttack is {IsDestroyed: false}) // if targetUnit is not null and still exist
        {
            _targetUnitToAttack.UnderAttack(damagePoint);
            if (_targetUnitToAttack is {IsDestroyed: false})
            {
                yield return new WaitForSeconds(1 / attackSpeed);
            }
        }
        StopAttack();
    }
    private void AttackToUnit(Unit targetUnit)
    {
        StopAttack();
        _attackCoroutine = StartCoroutine(Attack(targetUnit));
    }

    private void StopAttack()
    {
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
        }
        _targetUnitToAttack = null;
        _attackCoroutine = null;
    }
    #endregion
    public void PerformOnEnable(Vector3 parameter1)
    {
        InitialHealth = unitConfig.Health;
        CurrentHealth = InitialHealth;
        SoldierUISettings.SetDefault();
        MyPosition = VectorUtils.GetVector2Int(parameter1);
    }
    public void PerformDisable()
    {
        StopMovement();
        StopAttack();
        this.gameObject.SetActive(false);
    }
}
}


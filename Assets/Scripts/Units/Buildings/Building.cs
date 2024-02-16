using System;
using System.Collections;
using System.Collections.Generic;
using EventHandler;
using UnityEngine;
using UnityEngine.Timeline;
using Utilities;
using UnityEditor;

    [RequireComponent(typeof(BuildingUISettings))]
    public class Building : Unit , IEnableDisable<Vector3>
    {
        
        private BuildingUISettings _buildingUISettings;

        [SerializeField] private ProduceSoldierBehaviour produceSoldierBehaviour;
        public bool CanProduceSoldier
        {
            get => produceSoldierBehaviour != null;
        }
        public readonly EventThrower<Soldier, Building, Vector2Int> OnProduceSoldier =
            new EventThrower<Soldier, Building, Vector2Int>();
        
        public List<Soldier> ProduceableList
        {
            get
            {
                return produceSoldierBehaviour.ProduceableList;
            }
        }
        public BuildingUISettings BuildingUISettings
        {
            get
            {
                if (_buildingUISettings != null)
                {
                    return _buildingUISettings;
                }
                else
                {
                    _buildingUISettings = GetComponent<BuildingUISettings>();
                    return _buildingUISettings;
                }
            }
        }
        private Vector2Int _initialSoldierSpawnCoordinate;
        public void CreateSoldier(Soldier targetSoldier)
        {
            OnProduceSoldier.Throw(targetSoldier , this , _initialSoldierSpawnCoordinate);
        }

        private void SetInitialSpawnPoint()
        {
            var targetDirection = produceSoldierBehaviour.SpawnDirection;
            _initialSoldierSpawnCoordinate = targetDirection switch
            {
                ProduceSoldierBehaviour.SoldierSpawnDirection.Left => new Vector2Int(MyPosition.x - 1, MyPosition.y + Mathf.FloorToInt(0.5f *Height)),
                ProduceSoldierBehaviour.SoldierSpawnDirection.Right => new Vector2Int(MyPosition.x + Width, MyPosition.y + Mathf.FloorToInt(0.5f *Height)),
                ProduceSoldierBehaviour.SoldierSpawnDirection.Top => new Vector2Int(MyPosition.x + Mathf.FloorToInt(0.5f *Width), MyPosition.y + Height),
                ProduceSoldierBehaviour.SoldierSpawnDirection.Bot => new Vector2Int(MyPosition.x + Mathf.FloorToInt(0.5f *Width), MyPosition.y - 1),
                _ => new Vector2Int(0, 0)
            };
        }
        public void PerformOnEnable(Vector3 parameter1)
        {
            InitialHealth = unitConfig.Health;
            CurrentHealth = InitialHealth;
            BuildingUISettings.SetDefault();
            MyPosition = VectorUtils.GetVector2Int(parameter1);
            if (CanProduceSoldier)
            {
                SetInitialSpawnPoint();
            }
        }
        public void PerformDisable()
        {
            this.gameObject.SetActive(false);
        }
    }





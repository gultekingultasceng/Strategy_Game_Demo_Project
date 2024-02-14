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
        
        private BuildingUISettings buildingUISettings;

        [SerializeField] private ProduceSoldierBehaviour produceSoldierBehaviour;
        public bool CanProduceSoldier
        {
            get => produceSoldierBehaviour != null;
        }
      //  private List<Soldier> producableList = new List<Soldier>();
        
        public EventThrower<Soldier, Building, Vector2Int> OnProduceSoldier =
            new EventThrower<Soldier, Building, Vector2Int>();
        
        public List<Soldier> ProducableList
        {
            get
            {
                return produceSoldierBehaviour.producableList;
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
    
        
    
        enum soldierSpawnDirection
        {
            up,
            left,
            right,
            down
        }
        private soldierSpawnDirection soldierSpawnPoint;
        private Vector2Int initialSoldierSpawnCoordinate;
    
        
        public void CreateSoldier(Soldier targetSoldier)
        {
            OnProduceSoldier.Throw(targetSoldier , this , initialSoldierSpawnCoordinate);
        }
        
        public void SetInitialSpawnPoint()
        {
            var targetdirection = produceSoldierBehaviour.spawnDirection;
            initialSoldierSpawnCoordinate = targetdirection switch
            {
                ProduceSoldierBehaviour.soldierSpawnDirection.left => new Vector2Int(MyPosition.x - 1, MyPosition.y + Mathf.FloorToInt(0.5f *Height)),
                ProduceSoldierBehaviour.soldierSpawnDirection.right => new Vector2Int(MyPosition.x + Width, MyPosition.y + Mathf.FloorToInt(0.5f *Height)),
                ProduceSoldierBehaviour.soldierSpawnDirection.top => new Vector2Int(MyPosition.x + Mathf.FloorToInt(0.5f *Width), MyPosition.y + Height),
                ProduceSoldierBehaviour.soldierSpawnDirection.bot => new Vector2Int(MyPosition.x + Mathf.FloorToInt(0.5f *Width), MyPosition.y - 1),
                _ => new Vector2Int(0, 0)
            };
            
        }
        
        public void PerformOnEnable(Vector3 parameter1)
        {
            initialHealth = _UnitConfig.Health;
            currentHealth = initialHealth;     // Get initial health from config
            _BuildingUISettings.SetDefault();
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





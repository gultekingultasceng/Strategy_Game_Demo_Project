using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BuildingUISettings))]
public class Building : Unit
{
   [SerializeField]private BuildingUISettings buildingUISettings;
   
   public BuildingUISettings _BuildingUISettings
   {
      get
      {
         return buildingUISettings;
      }
      set
      {
         buildingUISettings = value;
      }
   }
   
   
   
}

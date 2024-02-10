using System.Collections;
using System.Collections.Generic;
using EventHandler;
using UnityEngine;

public class ProductionUI : MonoBehaviour
{
    [SerializeField] private UnitCreateButton unitCreateButtonPrefab;
    [SerializeField] private Unit[] produceableUnitList;
    [SerializeField] private Transform contentProductsParent;
 

    [SerializeField] private Sprite PlacementIndicator;

    public void Initialize()
    {
        FillProductsMenu();
    }
    private void FillProductsMenu()
    {
        for (int i = 0; i < produceableUnitList.Length; i++)
        {
            UnitCreateButton button = Instantiate(unitCreateButtonPrefab, contentProductsParent);
            button.SetTheCreateButton(produceableUnitList[i]);
        }
    }
    private void OpenPlacementIndicator()
    {
        
    }
}

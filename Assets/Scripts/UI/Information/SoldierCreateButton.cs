using System.Collections;
using System.Collections.Generic;
using EventHandler;
using UnityEngine;
using UnityEngine.UI;
public class SoldierCreateButton : MonoBehaviour
{
    [SerializeField] private Image soldierICon;
    private Button myButton;
    private Building myBuilding;
    private Soldier mySoldier;
    public void SetTheCreateButton(Building building,Soldier soldier)
    {
        soldierICon.sprite = soldier._SoldierUISettings.UnitIcon;
        if (myButton == null)
        {
            myButton = GetComponent<Button>();
        }
        myButton.onClick.AddListener(OnCreateSoldierButtonClicked);
        myBuilding = building;
        mySoldier = soldier;
    }

    public void OnCreateSoldierButtonClicked()
    {
        myBuilding.CreateSoldier(mySoldier);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class InformationUI : MonoBehaviour
{
    [SerializeField] private GameObject produceableProductsPanel; 
    [SerializeField] private Image unitIconImage;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Transform productsContent;
    [SerializeField] private SoldierCreateButton soldierCreateButtonPrefab;
    public void SetInformationUI(Unit unit)
    {
        titleText.text = unit._UnitUISettings.UnitTitle;
        unitIconImage.sprite = unit._UnitUISettings.UnitIcon;
        descriptionText.text = unit._UnitUISettings.UnitDescription;
        if (unit is Building building)
        {
            if (building.CanProduceSoldier)
            {
                produceableProductsPanel.SetActive(true);
                if (productsContent.transform.childCount > 0)
                {
                    foreach (Transform oldButtons in productsContent)
                    {
                        Destroy(oldButtons.gameObject);
                    }
                }
             
                for (int i = 0; i < building.ProducableList.Count; i++)
                {
                    SoldierCreateButton soldierCreateButton = Instantiate(soldierCreateButtonPrefab.gameObject, productsContent).GetComponent<SoldierCreateButton>();
                    soldierCreateButton.SetTheCreateButton(building,building.ProducableList[i]);
                }
            }
            else
            {
                produceableProductsPanel.SetActive(false);
            }
            
        }
        
    }
}

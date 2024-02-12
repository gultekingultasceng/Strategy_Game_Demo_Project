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
    [SerializeField] private GameObject produceButton;
    public void SetInformationUI(Unit unit)
    {
        titleText.text = unit._UnitUISettings.UnitTitle;
        unitIconImage.sprite = unit._UnitUISettings.UnitIcon;
        descriptionText.text = unit._UnitUISettings.UnitDescription;
        if (unit is Building building)
        {
            if (building.ProducableList.Count > 0)
            {
                
            }
            else
            {
                produceableProductsPanel.SetActive(false);
            }
            
        }
        
    }
}

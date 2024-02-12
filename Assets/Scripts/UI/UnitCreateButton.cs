using System;
using System.Collections;
using System.Collections.Generic;
using EventHandler;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UnitCreateButton : MonoBehaviour
{
     [SerializeField] private TextMeshProUGUI unitNameText;
     [SerializeField] private TextMeshProUGUI unitSizeText;
     [SerializeField] private Image unitIconImage;
     private Unit produceableUnit;
     private EventThrower<Unit> OnCreateUnitButtonClicked = new EventThrower<Unit>();
     public void SetTheCreateButton(Unit unit)
     {
          produceableUnit = unit;
          var unitUISettings = produceableUnit._UnitUISettings;
          unitNameText.text = unitUISettings.UnitTitle;
          unitIconImage.sprite = unitUISettings.UnitIcon;
          unitSizeText.text = $"{produceableUnit.Width} x {produceableUnit.Height}";
          Button myButton = GetComponent<Button>();
          myButton.onClick.AddListener(UnitCreateButtonOnClick);
          EventCatcher<Unit>.Catch(OnCreateUnitButtonClicked, UIManager.Instance.CreateUnitButtonClicked);
          
     }
     private void UnitCreateButtonOnClick()
     {
        OnCreateUnitButtonClicked.Throw(produceableUnit);
     }
     
}

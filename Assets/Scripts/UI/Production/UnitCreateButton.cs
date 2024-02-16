using System;
using System.Collections;
using System.Collections.Generic;
using EventHandler;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UnitCreateButton : MonoBehaviour , IEnableDisable
{
     [SerializeField] private TextMeshProUGUI unitNameText;
     [SerializeField] private TextMeshProUGUI unitSizeText;
     [SerializeField] private Image unitIconImage;
     private Unit _produceableUnit;
     private readonly EventThrower<Unit> _onCreateUnitButtonClicked = new EventThrower<Unit>();
     public void SetTheCreateButton(Unit unit)
     {
          _produceableUnit = unit;
          var unitUISettings = _produceableUnit.UnitUISettings;
          unitNameText.text = unitUISettings.UnitTitle;
          unitIconImage.sprite = unitUISettings.UnitIcon;
          unitSizeText.text = $"{_produceableUnit.Width} x {_produceableUnit.Height}";
          Button myButton = GetComponent<Button>();
          myButton.onClick.AddListener(UnitCreateButtonOnClick);
          EventCatcher<Unit>.Catch(_onCreateUnitButtonClicked, UIManager.Instance.CreateUnitButtonClicked);
     }
     private void UnitCreateButtonOnClick()
     {
        _onCreateUnitButtonClicked.Throw(_produceableUnit);
     }

     public void PerformOnEnable()
     {
         
     }
     public void PerformOnDisable()
     {
        
     }
}

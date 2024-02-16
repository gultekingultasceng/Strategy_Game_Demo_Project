using System.Collections;
using System.Collections.Generic;
using EventHandler;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    
    protected override void Awake()
    {
        base.Awake();
    }

    [SerializeField] private ProductionUI productionUI;
    [SerializeField] private InformationUI informationUI;
    [SerializeField] private PlacementIndicator placementIndicator;
    public void Initialize()
    {
        EventCatcher<Unit>.Catch(GameplayManager.Instance.OnSelectUnit , InformationPanelSet);
        productionUI.Initialize();
        InformationPanelSet(null);
    }

    public void InformationPanelSet(Unit selectedUnit)
    {
        if (selectedUnit)
        {
            informationUI.gameObject.SetActive(true);
            informationUI.SetInformationUI(selectedUnit);
        }
        else
        {
            informationUI.gameObject.SetActive(false);
        }
    }
    public PlacementIndicator GetPlacementIndicator()
    {
        return placementIndicator;
    }

    public void ClosePlacementIndicator()
    {
        placementIndicator.gameObject.SetActive(false);
    }
    public void CreateUnitButtonClicked(Unit unit)
    {
        GameplayManager.Instance.ProductionStage();
        placementIndicator.gameObject.SetActive(true);
        placementIndicator.SetUnit(unit);
    }
}

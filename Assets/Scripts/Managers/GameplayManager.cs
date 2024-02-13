using System.Collections;
using System.Collections.Generic;
using EventHandler;
using UnityEngine;

public class GameplayManager : Singleton<GameplayManager>
{
    protected override void Awake()
    {
        base.Awake();
    }
    [System.Serializable]
    enum Stages
    {
        Selection,
        Production
    }

    [SerializeField] private Stages stage;
    
    public void ProductionStage()
    {
        stage = Stages.Production;
    }
    public void SelectionStage()
    {
        stage = Stages.Selection;
    }

    private Unit lastSelectedUnit;
    public EventThrower<Unit> OnSelectUnit;
    public void Initialize()
    {
        OnSelectUnit = new EventThrower<Unit>();
        EventCatcher<Vector2Int>.Catch(InputHandler.Instance.OnLeftMouseButtonClick , LeftMouseClicked);
        EventCatcher<Vector2Int>.Catch(InputHandler.Instance.OnRightMouseButtonClick , RightMouseClicked);
        EventCatcher.Catch(MapGenerateManager.Instance.OnCreateBuilding , SelectionStage);
    }
    private void LeftMouseClicked(Vector2Int coordinate)
    {
        if (stage == Stages.Selection)
        {
            lastSelectedUnit = MapGenerateManager.Instance.IsUnitExistOnPosition(coordinate);
            OnSelectUnit.Throw(lastSelectedUnit);
        }
        else if (stage == Stages.Production)
        {
           MapGenerateManager.Instance.CreateBuilding();
        }
        else
        {
            Debug.Log("UNDEFINED STAGE !");
        }
    }
    private void RightMouseClicked(Vector2Int coordinate)
    {
        Debug.Log(coordinate.ToString());
        //  MapGenerateManager.Instance.GeneratedGrid.GetCellFromVector2Int(coordinate).IsEmptyCell = false; // to-do: silinecek
    }


 
   
}

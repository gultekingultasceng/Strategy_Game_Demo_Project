using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;
using EventHandler;
public class InputHandler : Singleton<InputHandler>
{
    public Camera _MainCamera;
    private Vector2 mousepos;
    private bool isOnUI = false;
    private bool isOnGrid = false;
    public Publisher<Vector2>OnLeftMouseButtonClick, OnRightMouseButtonClick;
    private Vector2Int XEdge, YEdge; // we can get these values from MapGenerateManager (row,column)
                                    // but this way more suitable for various situation
    protected override void Awake()
    {
        base.Awake();
    }
    public void Initialize()
    {
        OnLeftMouseButtonClick = new Publisher<Vector2>();
        OnRightMouseButtonClick = new Publisher<Vector2>();
    }

    public void SetEdges(Grid grid)
    {
        XEdge = new Vector2Int(0, grid.RowCount);
        YEdge = new Vector2Int(0, grid.ColumnCount);
        _MainCamera.transform.position = new Vector3(XEdge.y * .5f, YEdge.y * .5f, _MainCamera.transform.position.z);
    }
    private void Update()
    {
        mousepos = VectorUtils.GetWorldPositionFromMousePos(_MainCamera);
        isOnUI = EventSystem.current.IsPointerOverGameObject();
        isOnGrid = isMousePosInGridArea();
        if (isAvailableToPublishData())
        {
            PublishMouseClickData();
        }
    }

    public void PublishMouseClickData()
    {
        if (isLeftMouseButtonClicked())
        {
            OnLeftMouseButtonClick.Publish(VectorUtils.GetVector2Int(mousepos));
        }
        if (isRightMouseButtonClicked())
        {
            OnRightMouseButtonClick.Publish(VectorUtils.GetVector2Int(mousepos));
        }
    }
    public bool isLeftMouseButtonClicked()
    {
        return Input.GetMouseButtonUp(0);
    }

    public bool isRightMouseButtonClicked()
    {
        return Input.GetMouseButtonUp(0);
    }
    private bool isAvailableToPublishData()
    {
        return isOnGrid && !isOnUI;
    }
    private bool isMousePosInGridArea()
    {
        return mousepos.x >= XEdge.x &&
               mousepos.x <= XEdge.y &&
               mousepos.y >= YEdge.x && 
               mousepos.y <= YEdge.y;
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;
using EventHandler;
public class InputHandler : Singleton<InputHandler>
{
    public Camera mainCamera;
    private Vector2 _mousePosition;
    public Vector2 MousePosition
    {
        get => _mousePosition;
    }
    private bool _isOnUI = false;
    private bool _isOnGrid = false;
    public EventThrower<Vector2Int>OnLeftMouseButtonClick, OnRightMouseButtonClick;
    private Vector2Int _xEdge, _yEdge;
    protected override void Awake()
    {
        base.Awake();
    }
    public void Initialize()
    {
        OnLeftMouseButtonClick = new EventThrower<Vector2Int>();
        OnRightMouseButtonClick = new EventThrower<Vector2Int>();
    }

    public void SetEdges(Grid grid)
    {
        _xEdge = new Vector2Int(0, grid.RowCount);
        _yEdge = new Vector2Int(0, grid.ColumnCount);
        Transform mainCamTransform = mainCamera.transform;
        mainCamTransform.position = new Vector3(_xEdge.y * .5f, _yEdge.y * .5f, mainCamTransform.position.z);
    }
    private void Update()
    {
        _mousePosition = VectorUtils.GetWorldPositionFromMousePos(mainCamera);
        _isOnUI = EventSystem.current.IsPointerOverGameObject();
        _isOnGrid = IsMousePosInGridArea();
        if (IsAvailableToPublishData())
        {
            PublishMouseClickData();
        }
    }

    public Vector2Int GetMouseButtonData()
    {
        return VectorUtils.GetVector2Int(_mousePosition);
    }
    public void PublishMouseClickData()
    {
        if (IsLeftMouseButtonClicked())
        {
            OnLeftMouseButtonClick.Throw(GetMouseButtonData());
        }
        if (IsRightMouseButtonClicked())
        {
            OnRightMouseButtonClick.Throw(GetMouseButtonData());
        }
    }

    private static bool IsLeftMouseButtonClicked()
    {
        return Input.GetMouseButtonUp(0);
    }

    private static bool IsRightMouseButtonClicked()
    {
        return Input.GetMouseButtonUp(1);
    }
    private bool IsAvailableToPublishData()
    {
        return _isOnGrid && !_isOnUI;
    }
    private bool IsMousePosInGridArea()
    {
        return _mousePosition.x >= _xEdge.x &&
               _mousePosition.x <= _xEdge.y &&
               _mousePosition.y >= _yEdge.x && 
               _mousePosition.y <= _yEdge.y;
    }

}

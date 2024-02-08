using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour , IPointerDownHandler ,IPointerUpHandler
{
    public Camera MainCamera;
    private Vector2 mousepos;
    private bool isOnUI = false;
    private bool clicked = false;
    public bool isOnGrid = false;
    public static Action<Vector2> OnMouseClick , OnMouseRelease;
    public Vector2Int XEdge, YEdge;
    private void Update()
    {
        mousepos =MainCamera.ScreenToWorldPoint(Input.mousePosition);
        isOnUI = EventSystem.current.IsPointerOverGameObject();
        isOnGrid = isMousePosInGridArea();
    }

    private bool isMousePosInGridArea()
    {
        return mousepos.x >= XEdge.x &&
               mousepos.x <= XEdge.y &&
               mousepos.y >= YEdge.x && 
               mousepos.y <= YEdge.y;
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        clicked = true;
        OnMouseClick?.Invoke(mousepos);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (clicked)
        {
            clicked = false;
            OnMouseRelease?.Invoke(mousepos);
        }
    }
}

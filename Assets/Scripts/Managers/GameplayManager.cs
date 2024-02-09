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
    public void Initialize()
    {
        EventCatcher<Vector2Int>.Catch(InputHandler.Instance.OnLeftMouseButtonClick , LeftMouseClicked);
        EventCatcher<Vector2Int>.Catch(InputHandler.Instance.OnRightMouseButtonClick , RightMouseClicked);
    }
    private void LeftMouseClicked(Vector2Int coordinate)
    {
        Debug.Log(coordinate.ToString());
    }
    private void RightMouseClicked(Vector2Int coordinate)
    {
        Debug.Log(coordinate.ToString());
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    protected override void Awake()
    {
        base.Awake();
    }
    
    private void Start()
    {
        InputHandler.Instance.Initialize();
        MapGenerateManager.Instance.Initialize();
        GameplayManager.Instance.Initialize();
        UIManager.Instance.Initialize();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    protected override void Awake()
    {
        base.Awake();
    }

    [SerializeField] private ProductionUI productionUI;
    [SerializeField] private InformationUI informationUI;

    public void Initialize()
    {
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstantsVariables;
public class UnitUISettings : MonoBehaviour
{
    [SerializeField] protected Sprite unitIcon;
    public Sprite UnitIcon
    {
        get
        {
            return unitIcon;
        }
    }
    protected SpriteRenderer SpriteRenderer;

    [SerializeField] private string unitDescription;
    public string UnitDescription
    {
        get
        {
            return unitDescription;
        }
    }
    [SerializeField] private string unitTitle;
    public string UnitTitle
    {
        get
        {
            return unitTitle;
        }
    }
    public void SetDefault()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer.color = GameConsts.UnitInitialColor;
    }
    public void GetDamageEffect()
    {
        StartCoroutine(DamageEffect());
    }
    IEnumerator DamageEffect(float delay = .05f)
    {
        SpriteRenderer.color = GameConsts.DamagedUnitColor;
        yield return new WaitForSeconds(delay);
        SpriteRenderer.color = GameConsts.UnitInitialColor;
    }
}

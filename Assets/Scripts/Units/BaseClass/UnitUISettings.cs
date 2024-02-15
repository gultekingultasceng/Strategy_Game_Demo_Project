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
    protected SpriteRenderer spriteRenderer;
    public SpriteRenderer _SpriteRenderer
    {
        get
        {
            return spriteRenderer;
        }
        set
        {
            spriteRenderer = value;
        }
    }
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
    private Color damagedColor, initialColor;

    public void SetDefault()
    {
        _SpriteRenderer = GetComponent<SpriteRenderer>();
        damagedColor = GameConsts.DamagedUnitColor;
        initialColor = GameConsts.UnitInitialColor;
        _SpriteRenderer.color = initialColor;
    }
    public void GetDamageEffect()
    {
        StartCoroutine(DamageEffect());
    }
    IEnumerator DamageEffect(float delay = .05f)
    {
        _SpriteRenderer.color = damagedColor;
        yield return new WaitForSeconds(delay);
        _SpriteRenderer.color = initialColor;
    }
}

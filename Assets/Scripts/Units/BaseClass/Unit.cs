using SGD.Core.Configs;
using SGD.Core.EventHandler;
using UnityEngine;

namespace SGD.Core.Base
{ 
    [RequireComponent(typeof(UnitUISettings))]
    public abstract class Unit : MonoBehaviour
    {   
    [SerializeField] private int uniqueIDForUnitType = -1;
    public int UniqueIDForType
    {
        get => uniqueIDForUnitType;
    }
    [SerializeField] public UnitConfig unitConfig;
    private UnitUISettings _unitUISettings;
    public readonly EventThrower<Unit> OnDestroy = new EventThrower<Unit>();
    private bool _isDestroyed = false;
    public bool IsDestroyed
    {
        get => _isDestroyed;
        private set => _isDestroyed = value;
    }
    public UnitUISettings UnitUISettings
    {
        get
        {
            if (_unitUISettings != null)
            {
                return _unitUISettings;
            }
            else
            {
                _unitUISettings = GetComponent<UnitUISettings>();
                return _unitUISettings;
            }
            
        }
    }
    
    [SerializeField] protected int width; // Set these manually from inspector to make it more suitable for various situations
                                         // But we can set it with Sprite resolution divided by pixel per unit Count (cell size)
    public int Width 
    {
        get
        {
            return width;
        }
    } // Read-only
    [SerializeField] protected int height;
    public int Height 
    {
        get
        {
            return height;
        }
    }

    protected int InitialHealth;
    protected int CurrentHealth;
   
    protected Vector2Int myPosition;
    public Vector2Int MyPosition
    {
        get
        {
            return myPosition;
        }
        set
        {
            myPosition = value;
            MoveToPosition(myPosition);
        }
    }
    
    private void Awake()
    {
        _unitUISettings = GetComponent<UnitUISettings>();
    }

    private void OnEnable()
    {
        IsDestroyed = false;
    }
    
    private void MoveToPosition(Vector2Int coordinate)
    {
        transform.position = new Vector3(coordinate.x, coordinate.y, 0f);
    }
    public void UnderAttack(int damagePoint)
    {
        CurrentHealth -= damagePoint;
        if (CurrentHealth > 0)
        {
            _unitUISettings.GetDamageEffect();
        }
        else
        {
            DestroyMe();
        }
    }

    public void DestroyMe()
    {
        _isDestroyed = true;
        OnDestroy.Throw(this);
    }
}

}

using SGD.Core.Base;
using SGD.Core.ObjectPooling;
using UnityEngine;
using UnityEngine.UI;

namespace SGD.Core.UI
{
    public class SoldierCreateButton : MonoBehaviour,IEnableDisable
    {
        [SerializeField] private Image soldierICon;
        private Button _myButton;
        private Building _myBuilding;
        private Soldier _mySoldier;
        public void SetTheCreateButton(Building building,Soldier soldier)
        {
            soldierICon.sprite = soldier.SoldierUISettings.UnitIcon;
            if (_myButton == null)
            {
                _myButton = GetComponent<Button>();
            }
            _myButton.onClick.AddListener(OnCreateSoldierButtonClicked);
            _myBuilding = building;
            _mySoldier = soldier;
        }
        public void OnCreateSoldierButtonClicked()
        {
            _myBuilding.CreateSoldier(_mySoldier);
        }
        public void PerformOnEnable()
        {
        
        }
        public void PerformOnDisable()
        {
            _myButton.onClick.RemoveListener(OnCreateSoldierButtonClicked);
            _myBuilding = null;
            _mySoldier = null;
            this.gameObject.SetActive(false);
        }
    }
}


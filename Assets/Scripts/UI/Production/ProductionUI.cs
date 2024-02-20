using UnityEngine;
using SGD.Core.Base;
namespace SGD.Core.UI
{
    public class ProductionUI : MonoBehaviour
    {
        [SerializeField] private UnitCreateButton unitCreateButtonPrefab;
        [SerializeField] private Unit[] produceableUnitList;
        [SerializeField] private Transform contentProductsParent;
        [SerializeField] private UnitCreateButtonPool unitCreateButtonPool;
        [SerializeField] private InfiniteScrollGeneric infiniteScrollView;
        public void Initialize()
        {
            FillProductsMenu();
        }

        private void FillUnitCreateButtonsFromPool(GameObject unitCreateButton , Transform parent , bool isSiblingOrderFirst)
        {
            UnitCreateButton pulled = unitCreateButtonPool.GetObject(unitCreateButton,parent);
            pulled.SetTheCreateButton(unitCreateButton.GetComponent<UnitCreateButton>().ProduceableUnit); 
            if (isSiblingOrderFirst)
            {
                pulled.GetComponent<RectTransform>().SetAsFirstSibling();
            }
            else
            {
                pulled.GetComponent<RectTransform>().SetAsLastSibling();
            }
        }

        private UnitCreateButton GetUnitCreateButtonsFromPool(GameObject unitCreateButton, Transform parent)
        {
            return unitCreateButtonPool.GetObject(unitCreateButton,parent);
        }
   
        private void FillProductsMenu()
        {
            for (int i = 0; i < produceableUnitList.Length; i++)
            {
                UnitCreateButton button = GetUnitCreateButtonsFromPool(unitCreateButtonPrefab.gameObject, contentProductsParent);
                button.SetTheCreateButton(produceableUnitList[i]);
            }
            infiniteScrollView.Initialize(FillUnitCreateButtonsFromPool);
            
        }
    }
}


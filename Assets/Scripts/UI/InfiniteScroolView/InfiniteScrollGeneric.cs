using System;
using System.Collections;
using System.Collections.Generic;
using SGD.Core.ObjectPooling;
using UnityEngine;
using UnityEngine.UI;

namespace SGD.Core.UI
{
   public class InfiniteScrollGeneric : MonoBehaviour
{
   [SerializeField] private ScrollRect scrollRect;
   private RectTransform _viewPortTransform;
   private RectTransform _contentPanelTransform;
   private GridLayoutGroup _gridLayoutGroup;
   private Vector2 _oldVelocity = Vector2.zero;
   private bool _isUpdated = false;
   private bool _isVertical = false;
   private bool _isHorizontal = false;
   private RectTransform[] _itemList;
   public void Initialize(Action<GameObject , Transform , bool> InstantiateAction)
   {
      _viewPortTransform = scrollRect.viewport;
      _contentPanelTransform = scrollRect.content;
      _gridLayoutGroup = _contentPanelTransform.GetComponent<GridLayoutGroup>();
      scrollRect.movementType = ScrollRect.MovementType.Unrestricted;
      scrollRect.inertia = true;
      scrollRect.decelerationRate = .5f;
      int itemsToAdd = 0;
      _isVertical = scrollRect.vertical;
      _isHorizontal = scrollRect.horizontal;
      _itemList = new RectTransform[_contentPanelTransform.transform.childCount];
      for (int i = 0; i < _contentPanelTransform.transform.childCount; i++)
      {
         _itemList[i] = _contentPanelTransform.transform.GetChild(i).GetComponent<RectTransform>();
      }
      if (_isHorizontal)
      {
         itemsToAdd = Mathf.CeilToInt(_viewPortTransform.rect.width / _gridLayoutGroup.cellSize.x + _gridLayoutGroup.spacing.x);
      }
      if (_isVertical)
      {
         itemsToAdd = Mathf.CeilToInt(_viewPortTransform.rect.height / _gridLayoutGroup.cellSize.y + _gridLayoutGroup.spacing.y);
      }
      for (int i = 0; i < itemsToAdd; i++)
      {
         InstantiateAction?.Invoke(_itemList[i % _itemList.Length].gameObject , _contentPanelTransform , false);
      }
      for (int i = 0; i < itemsToAdd; i++)
      {
         int num = _itemList.Length - i - 1;
         while (num < 0)
         {
            num += _itemList.Length;
         }
         InstantiateAction?.Invoke(_itemList[num].gameObject , _contentPanelTransform , true);
      }

      var contentPanelLocalPosition = _contentPanelTransform.localPosition;
      if (_isHorizontal)
      {
         contentPanelLocalPosition = new Vector3((0 - (_gridLayoutGroup.cellSize.x + _gridLayoutGroup.spacing.x) * itemsToAdd),
            contentPanelLocalPosition.y,
            contentPanelLocalPosition.z);
      }

      if (_isVertical)
      {
         contentPanelLocalPosition = new Vector3(contentPanelLocalPosition.x,
            (0 - (_gridLayoutGroup.cellSize.y + _gridLayoutGroup.spacing.y) * itemsToAdd),
            contentPanelLocalPosition.z);
      }

      _contentPanelTransform.localPosition = contentPanelLocalPosition;
   }

   private void Update()
   {
      if (_isUpdated)
      {
         _isUpdated = false;
         scrollRect.velocity = _oldVelocity;
      }

      if (_isHorizontal)
      {
         if (_contentPanelTransform.localPosition.x < 0)
         {
            Canvas.ForceUpdateCanvases();
            _oldVelocity = scrollRect.velocity;
            _contentPanelTransform.localPosition += new Vector3(_itemList.Length * (_gridLayoutGroup.cellSize.x + _gridLayoutGroup.spacing.x),
               0, 0);
            _isUpdated = true;
         }

         if (_contentPanelTransform.localPosition.x > (_itemList.Length * (_gridLayoutGroup.cellSize.x + _gridLayoutGroup.spacing.x)) )
         {
            Canvas.ForceUpdateCanvases();
            _oldVelocity = scrollRect.velocity;
            _contentPanelTransform.localPosition -= new Vector3(_itemList.Length * (_gridLayoutGroup.cellSize.x + _gridLayoutGroup.spacing.x),
               0, 0);
            _isUpdated = true;
         }
      }

      if (_isVertical)
      {
         if (_contentPanelTransform.localPosition.y < 0)
         {
            Canvas.ForceUpdateCanvases();
            _oldVelocity = scrollRect.velocity;
            _contentPanelTransform.localPosition += new Vector3(0,
               _itemList.Length * (_gridLayoutGroup.cellSize.y + _gridLayoutGroup.spacing.y), 0);
            _isUpdated = true;
         }

         if (_contentPanelTransform.localPosition.y > (_itemList.Length * (_gridLayoutGroup.cellSize.y + _gridLayoutGroup.spacing.y)) )
         {
            Canvas.ForceUpdateCanvases();
            _oldVelocity = scrollRect.velocity;
            _contentPanelTransform.localPosition -= new Vector3(0,
               _itemList.Length * (_gridLayoutGroup.cellSize.y + _gridLayoutGroup.spacing.y), 0);
            _isUpdated = true;
         }
      }
   
   }
}
}


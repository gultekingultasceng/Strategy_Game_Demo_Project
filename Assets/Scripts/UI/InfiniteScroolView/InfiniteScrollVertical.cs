using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InfiniteScrollVertical : MonoBehaviour, IBeginDragHandler, IDragHandler, IScrollHandler
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private GridLayoutGroup gridLayout;

    private RectTransform _rect;
    private Vector2 _lastDragPosition;
    private bool _positiveDrag;
    private float _size;
   
    [System.Serializable]
    private enum ScrollType
    {
        Vertical,
        Horizontal
    }
    [SerializeField] private ScrollType scrollType;
    public void OnEnable()
    {
        scrollRect.movementType = ScrollRect.MovementType.Unrestricted;
        scrollRect.onValueChanged.AddListener(OnViewScroll);
        SetSize();
        scrollType = scrollRect.vertical ? ScrollType.Vertical : ScrollType.Horizontal;
    }
    private void SetSize()
    {
        _rect = GetComponent<RectTransform>();
        _size = scrollType == ScrollType.Vertical ? _rect.rect.height : _rect.rect.width;
        _size = _rect.rect.height;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _lastDragPosition = eventData.position;
    }
    public int GetRequiredItemCount()
    {
        return Mathf.CeilToInt((scrollRect.viewport.rect.height * 1.5f) / (gridLayout.spacing.y + gridLayout.cellSize.y));
    }
    public void OnDrag(PointerEventData eventData)
    {
        _positiveDrag = scrollType == ScrollType.Vertical ? eventData.position.y > _lastDragPosition.y : eventData.position.x > _lastDragPosition.x;
        _lastDragPosition = eventData.position;
    }

    public void OnScroll(PointerEventData eventData)
    {
        _positiveDrag = scrollType == ScrollType.Vertical ? eventData.scrollDelta.y > 0 : eventData.scrollDelta.x > 0;
    }

    public void OnViewScroll(Vector2 _)
    {
        int leftItemIndex = _positiveDrag ? scrollRect.content.childCount - 1 : 0;
        var leftItem = scrollRect.content.GetChild(leftItemIndex);

        int rightItemIndex = _positiveDrag ? scrollRect.content.childCount - 2 : 1;
        var rightItem = scrollRect.content.GetChild(rightItemIndex);
        if (!ReachedThreshold(rightItem) && !ReachedThreshold(leftItem))
        {
            return;
        }
        Vector2 newPos = scrollRect.content.anchoredPosition;
        if (_positiveDrag)
        {
            if (scrollType == ScrollType.Vertical)
            {
                newPos.y = newPos.y - ( gridLayout.spacing.y + gridLayout.cellSize.y );
            }
            else
            {
                newPos.x = newPos.x - ( gridLayout.spacing.x + gridLayout.cellSize.x );
            }
        }
        else
        {
            if (scrollType == ScrollType.Vertical)
            {
                newPos.y = newPos.y + (gridLayout.spacing.y + gridLayout.cellSize.y);
            }
            else
            {
                newPos.x = newPos.x + (gridLayout.spacing.x + gridLayout.cellSize.x);
            }
        }

        int newIndex = _positiveDrag ? 0 : scrollRect.content.childCount - 1;
        rightItem.SetSiblingIndex(newIndex);
        leftItem.SetSiblingIndex(_positiveDrag ? newIndex + 1 : newIndex - 1);
        scrollRect.content.anchoredPosition = newPos;
    }
    private bool ReachedThreshold(Transform item)
    {
        bool isReached = false;
        float position = scrollType == ScrollType.Vertical ? transform.position.y : transform.position.x;
        float offset = _size * .5f;
        float posThreshold = position - offset;
        float negThreshold = position + offset;
        if (scrollType == ScrollType.Vertical)
        {
            isReached = _positiveDrag ? item.position.y - gridLayout.cellSize.y > posThreshold :
                item.position.y + gridLayout.cellSize.y < negThreshold;
        }
        else
        {
            isReached = _positiveDrag ? item.position.x - gridLayout.cellSize.x > posThreshold :
                item.position.x + gridLayout.cellSize.x < negThreshold;
        }
        return isReached;
    }
}

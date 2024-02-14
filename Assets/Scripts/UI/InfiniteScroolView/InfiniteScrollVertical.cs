using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InfiniteScrollVertical : MonoBehaviour, IBeginDragHandler, IDragHandler, IScrollHandler
{
  [SerializeField] ScrollRect scrollRect;
    [SerializeField] GridLayoutGroup gridLayout;

    private RectTransform rect;
    private Vector2 lastDragPosition;
    private bool positiveDrag;
    private float size;
   
    [System.Serializable]
    enum ScrollType
    {
        vertical,
        horizontal
    }
    [SerializeField] private ScrollType scrolltype;
    public void Initialize()
    {
        scrollRect.movementType = ScrollRect.MovementType.Unrestricted;
        scrollRect.onValueChanged.AddListener(OnViewScroll);
        CalculateHeight();
        scrolltype = scrollRect.vertical ? ScrollType.vertical : ScrollType.horizontal;
    }
    private void CalculateHeight()
    {
        rect = GetComponent<RectTransform>();
        size = scrolltype == ScrollType.vertical ? rect.rect.height : rect.rect.width;
        size = rect.rect.height;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        lastDragPosition = eventData.position;
    }
    public int GetRequiredItemCount()
    {
        return Mathf.CeilToInt((scrollRect.viewport.rect.height * 1.5f) / (gridLayout.spacing.y + gridLayout.cellSize.y));
    }
    public void OnDrag(PointerEventData eventData)
    {
        positiveDrag = scrolltype == ScrollType.vertical ? eventData.position.y > lastDragPosition.y : eventData.position.x > lastDragPosition.x;
        lastDragPosition = eventData.position;
    }

    public void OnScroll(PointerEventData eventData)
    {
        positiveDrag = scrolltype == ScrollType.vertical ? eventData.scrollDelta.y > 0 : eventData.scrollDelta.x > 0;
    }

    public void OnViewScroll(Vector2 _)
    {
        int leftItemIndex = positiveDrag ? scrollRect.content.childCount - 1 : 0;
        var lItem = scrollRect.content.GetChild(leftItemIndex);

        int rItemIndex = positiveDrag ? scrollRect.content.childCount - 2 : 1;
       var rItem = scrollRect.content.GetChild(rItemIndex);
      /*
        List<int> itemsIndexesInRow = new List<int>();
        List<Transform> itemList = new List<Transform>();
        if (positiveDrag)
        {
            for (int i = 0; i < gridLayout.constraintCount ; i++)
            {
                int index = scrollRect.content.childCount - 1 - i;
                itemsIndexesInRow.Add(index);
                itemList.Add(scrollRect.content.GetChild(index));
            }
        }
        else
        {
            for (int i = 0; i < gridLayout.constraintCount ; i++)
            {
                int index = i;
                itemsIndexesInRow.Add(index);
                itemList.Add(scrollRect.content.GetChild(index));
            }
        }
        */
        /*
        for (int i = 0; i < itemList.Count; i++)
        {
            if (ReachedThreshold(itemList[i]))
            {
                return;
            }
        }
        */
        
        if (!ReachedThreshold(rItem) && !ReachedThreshold(lItem))
        {
            return;
        }
        
        Vector2 newPos = scrollRect.content.anchoredPosition;

        if (positiveDrag)
        {
            if (scrolltype == ScrollType.vertical)
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
            if (scrolltype == ScrollType.vertical)
            {
                newPos.y = newPos.y + (gridLayout.spacing.y + gridLayout.cellSize.y);
            }
            else
            {
                newPos.x = newPos.x + (gridLayout.spacing.x + gridLayout.cellSize.x);
            }
            
        }

        int nIndex = positiveDrag ? 0 : scrollRect.content.childCount - 1;

        /*
        if (positiveDrag)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                itemList[i].SetSiblingIndex(nIndex + i);
            }
        }
        else
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                itemList[i].SetSiblingIndex(nIndex - i);
            }
        }
        */
        rItem.SetSiblingIndex(nIndex);
        lItem.SetSiblingIndex(positiveDrag ? nIndex + 1 : nIndex - 1);
        scrollRect.content.anchoredPosition = newPos;
    }

    private bool ReachedThreshold(Transform item)
    {
        bool isReached = false;
        float position = scrolltype == ScrollType.vertical ? transform.position.y : transform.position.x;
        float offset = size * .5f;
        float posThreshold = position - offset;
        float negThreshold = position + offset;
        if (scrolltype == ScrollType.vertical)
        {
            isReached = positiveDrag ? item.position.y - gridLayout.cellSize.y > posThreshold :
                item.position.y + gridLayout.cellSize.y < negThreshold;
        }
        else
        {
            isReached = positiveDrag ? item.position.x - gridLayout.cellSize.x > posThreshold :
                item.position.x + gridLayout.cellSize.x < negThreshold;
        }
        return isReached;
    }
}

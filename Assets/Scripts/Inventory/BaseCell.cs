using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class BaseCell : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Action<BaseCell> Injecting;

    private int _cellIndex;
    private Transform _inventoryParent;
    private Transform _cellParent;
    private Transform _draggingParent;
    private Image _cellImage;

    public int CellIndex => _cellIndex;
    public Transform InventoryParent => _inventoryParent;
    public Transform CellParent => _cellParent;
    public Transform DraggingParent => _draggingParent;
    public Image CellImage => _cellImage;

    public abstract void Clear();

    public void Init(int cellIndex, Transform inventoryParent, Transform cellParent, Transform draggingParent)
    {
        _cellIndex = cellIndex;
        _inventoryParent = inventoryParent;
        _cellParent = cellParent;
        _draggingParent = draggingParent;
        _cellImage = gameObject.GetComponent<Image>();
        SetColorAlpha(0f);
    }

    protected void SetColorAlpha(float alpha)
    {
        var color = _cellImage.color;
        color.a = alpha;
        _cellImage.color = color;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_cellImage != default)
            transform.SetParent(_draggingParent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_cellImage != default)
            transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (In((RectTransform)_inventoryParent))
            InsertCell();
        else
            Injecting?.Invoke(this);
    }

    protected bool In(RectTransform originalParent)
    {
        return originalParent.rect.Contains(transform.localPosition);
    }

    protected void InsertCell()
    {
        int newIndex = 0;
        var childCount = _inventoryParent.childCount;
        for (int cellIndex = 0; cellIndex < childCount; cellIndex++)
        {
            var newIndexDistance = Vector3.Distance(transform.position, _inventoryParent.GetChild(cellIndex).position);
            var oldIndexDistance = Vector3.Distance(transform.position, _inventoryParent.GetChild(newIndex).position);
            if (newIndexDistance < oldIndexDistance && newIndexDistance <= 100f)
            {
                newIndex = cellIndex;
            }
        }

        transform.SetParent(_cellParent);
        transform.position = _cellParent.position;

        var oldItemIndex = _cellParent.GetSiblingIndex();
        _inventoryParent.GetChild(newIndex).SetSiblingIndex(oldItemIndex);
        _cellParent.SetSiblingIndex(newIndex);
    }
}

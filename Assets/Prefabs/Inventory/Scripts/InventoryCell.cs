using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Action<InventoryCell> Injecting;

    public int _cellIndex;
    private Transform _inventoryParent;
    private Transform _cellParent;
    private Transform _draggingParent;
    private Image _cellImage;
    private ItemSO _item;

    public int CellIndex => _cellIndex;
    public ItemSO Item => _item;
    public Transform InventoryParent => _inventoryParent;
    public Transform CellParent => _cellParent;
    public Transform DraggingParent => _draggingParent;

    public void OnEndDrag(PointerEventData eventData)
    {
        if (In((RectTransform)InventoryParent))
            InsertCell();
        else
            Injecting?.Invoke(this);
    }

    public void Init(Transform inventoryParent, Transform cellParent, Transform draggingParent)
    {
        _inventoryParent = inventoryParent;
        _cellParent = cellParent;
        _draggingParent = draggingParent;
        _cellImage = gameObject.GetComponent<Image>();
        SetColorAlpha(0f);
    }

    public void SetItem(int cellIndex, ItemSO item)
    {
        _cellIndex = cellIndex;
        _item = item;
        _cellImage.sprite = _item.InventoryIcon;
        SetColorAlpha(1f);
    }

    public void Clear()
    {
        _item = null;
        _cellImage.sprite = null;
        transform.SetParent(_cellParent);
        transform.position = _cellParent.position;
        SetColorAlpha(0f);
    }

    private void SetColorAlpha(float alpha)
    {
        var color = _cellImage.color;
        color.a = alpha;
        _cellImage.color = color;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_item != null)
            transform.SetParent(_draggingParent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_item != null)
            transform.position = eventData.position;
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

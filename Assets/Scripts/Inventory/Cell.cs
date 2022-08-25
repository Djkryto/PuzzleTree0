using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Action<Cell> Injecting;

    private Transform _inventoryParent;
    private Transform _cellParent;
    private Transform _draggingParent;
    private Image _cellImage;
    private InteractiveItem _itemInWorld;
    private ItemSO _item;

    public Transform InventoryParent => _inventoryParent;
    public Transform CellParent => _cellParent;
    public Transform DraggingParent => _draggingParent;
    public Image CellImage => _cellImage;
    public ItemSO Item => _item;
    public InteractiveItem ItemInWorld => _itemInWorld;

    public Cell SetItem(InteractiveItem itemInWorld)
    {
        Clear();
        _itemInWorld = itemInWorld;
        _item = itemInWorld.Takeable.ItemData;
        CellImage.sprite = _item.InventoryIcon;
        SetColorAlpha(1f);
        return this;
    }

    public void Clear()
    {
        _item = default;
        _itemInWorld = default;
        CellImage.sprite = default;
        transform.SetParent(CellParent);
        transform.position = CellParent.position;
        SetColorAlpha(0f);
    }

    public void Init(Transform inventoryParent, Transform cellParent, Transform draggingParent)
    {
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
        if (_item != null)
            transform.SetParent(_draggingParent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_item != null)
            transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (ContainedIn((RectTransform)_inventoryParent))
            InsertCell();
        else
            Injecting?.Invoke(this);
    }

    protected bool ContainedIn(RectTransform originalParent)
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
            if (newIndexDistance < oldIndexDistance && newIndexDistance <= 200f)
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

    public void ResetCell()
    {
        transform.SetParent(_cellParent);
        transform.position = _cellParent.position;
    }
}

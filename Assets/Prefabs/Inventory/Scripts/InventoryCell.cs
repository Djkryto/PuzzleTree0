using UnityEngine;

public class InventoryCell : BaseCell
{
    private Transform _objectInWorld;
    private ItemSO _item;

    public ItemSO Item => _item;
    public Transform ObjectInWorld => _objectInWorld;

    public InventoryCell SetItem(Transform objectInWorld, ItemSO item)
    {
        _objectInWorld = objectInWorld;
        _item = item;
        CellImage.sprite = _item.InventoryIcon;
        SetColorAlpha(1f);
        return this;
    }

    public override void Clear()
    {
        _item = default;
        CellImage.sprite = default;
        transform.SetParent(CellParent);
        transform.position = CellParent.position;
        SetColorAlpha(0f);
    }
}

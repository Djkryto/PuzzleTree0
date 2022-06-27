using UnityEngine;

public class InventoryCell : BaseCell
{
    private Transform _itemInWorld;
    private ItemSO _item;

    public ItemSO Item => _item;
    public Transform ItemInWorld => _itemInWorld;

    public InventoryCell SetItem(Transform itemInWorld, ItemSO item)
    {
        _itemInWorld = itemInWorld;
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

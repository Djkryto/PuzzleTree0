using UnityEngine;

public class InventoryCell : BaseCell
{
    private InteractiveItem _itemInWorld;
    private ItemSO _item;

    public ItemSO Item => _item;
    public InteractiveItem ItemInWorld => _itemInWorld;

    public InventoryCell SetItem(InteractiveItem itemInWorld, ItemSO item)
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
        _itemInWorld = default;
        CellImage.sprite = default;
        transform.SetParent(CellParent);
        transform.position = CellParent.position;
        SetColorAlpha(0f);
    }
}

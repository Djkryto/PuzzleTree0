using System;
using System.Collections.Generic;
using System.Linq;

public class Inventory
{
    public Action<InteractiveItem> OnAddingItem;

    private List<InteractiveItem> _items;
    private int _inventorySize;

    public IReadOnlyList<InteractiveItem> Items => _items;
    public int Size => _inventorySize;

    public Inventory(int inventorySize)
    {
        _inventorySize = inventorySize;
        _items = new List<InteractiveItem>();
    }

    public Inventory(int inventorySize, List<InteractiveItem> items) : this(inventorySize)
    {
        if (items.Count > _items.Count)
            throw new Exception("An incorrect list of items was passed!");

        for(int i = 0; i < items.Count; i++)
        {
            _items[i] = items[i];
        }
    }

    public bool TryAddItem(InteractiveItem item)
    {
        if(_items.Count >= _inventorySize)
            return false;

        _items.Add(item);
        OnAddingItem?.Invoke(item);

        return true;
    }

    public bool TryRemoveItem(InteractiveItem item)
    {
        int removeableItemIndex = _items.IndexOf(item);

        if(removeableItemIndex == -1)
            return false;

        _items.RemoveAt(removeableItemIndex);
        return true;
    }
}

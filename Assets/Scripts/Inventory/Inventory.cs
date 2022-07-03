using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : BaseCellContainer
{
    public UnityEvent InventoryShowing;
    public Action<InventoryCell> AddedItem;
    public Action<InventoryCell> DeletedItem;

    private bool _isOpen = false;

    public bool IsOpen => _isOpen;

    public void InventoryActive()
    {
        gameObject.SetActive(!_isOpen);
        InventoryShowing.Invoke();
        _isOpen = !_isOpen;
    }

    public InventoryCell AddItem(InteractiveItem itemInWorld, ItemSO item)
    {
        try
        {
            var cell = (InventoryCell)CellPool.FirstOrDefault(cell => ((InventoryCell)cell).Item == default);
            cell.SetItem(itemInWorld, item);
            AddedItem?.Invoke(cell);
            return cell;
        }
        catch(Exception exc)
        {
            Debug.LogException(exc);
            return null;
        }
    }

    public override void DropItem(BaseCell dropedCell)
    {
        try
        {
            DeletedItem?.Invoke((InventoryCell)dropedCell);
            var cell = CellPool.FirstOrDefault(cell => cell == dropedCell);
            cell.Clear();
        }
        catch (Exception exc)
        {
            Debug.LogException(exc);
        }
    }
}

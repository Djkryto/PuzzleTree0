using System;
using System.Linq;
using UnityEngine;

public class Inventory : BaseCellContainer
{
    public Action<InventoryCell> AddedItem;
    public Action<InventoryCell> DeletedItem;

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

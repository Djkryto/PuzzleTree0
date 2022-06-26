using System;
using System.Linq;
using UnityEngine;

public class HotBar : Inventory
{
    public void AddItem(int cellIndex, ItemSO item)
    {
        try
        {
            var cell = CellPool.FirstOrDefault(cell => cell.Item == null);
            cell.SetItem(cellIndex, item);
        }
        catch(Exception exc)
        {
            Debug.Log(exc);
        }
    }

    public override void DropItem(InventoryCell dropCell)
    {
        try
        {
            ItemDeleting?.Invoke(dropCell);
            var cell = CellPool.FirstOrDefault(cell => cell.CellIndex == dropCell.CellIndex);
            cell.Clear();
        }
        catch (Exception exc)
        {
            Debug.LogException(exc);
        }
    }
}

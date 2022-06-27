using System;
using System.Linq;
using UnityEngine;

public class HotBar : BaseCellContainer
{
    [SerializeField] private Inventory _inventory;

    private void Awake()
    {
        _inventory.AddedItem += AddItem;
        _inventory.DeletedItem += DropItem;
    }

    public void AddItem(InventoryCell inventoryCell)
    {
        try
        {
            var cell = (HotbarCell)CellPool.FirstOrDefault(cell => ((HotbarCell)cell).SourceCell == default);
            cell.SetItem(inventoryCell);
        }
        catch(Exception exc)
        {
            Debug.LogWarning(exc);
        }
    }

    public override void DropItem(BaseCell dropedCell)
    {
        try
        {
            InventoryCell inventoryCell = (InventoryCell)dropedCell;
            var hotBarCell = CellPool.FirstOrDefault(cell => ((HotbarCell)cell).SourceCell == inventoryCell);
            hotBarCell.Clear();
        }
        catch (Exception exc)
        {
            Debug.LogWarning(exc);
        }
    }
}

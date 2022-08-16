using System;
using System.Linq;
using UnityEngine;

public class HotbarView : BaseInventoryView
{
    [SerializeField] private int _hotbarSize;

    public override int ContainerSize => _hotbarSize;

    private void Start()
    {
        Player.Inventory.OnAddingItem += AddToEmptySpace;
    }

    public bool TryAddOtherCellOnHotbar(Cell otherCell)
    {
        var oldParent = otherCell.transform.parent;
        otherCell.transform.SetParent(ItemContainer);
        var entersTheHotbar = ((RectTransform)ItemContainer).rect.Contains(otherCell.transform.localPosition);
        otherCell.transform.SetParent(oldParent);
        if (!entersTheHotbar)
            return false;
        InsertCell(otherCell);
        otherCell.ResetCell();
        return true;
    }

    protected void InsertCell(Cell otherCell)
    {
        int newIndex = 0;
        var cellsCount = ItemContainer.childCount;
        for (int cellIndex = 0; cellIndex < cellsCount; cellIndex++)
        {
            var newIndexDistance = Vector3.Distance(otherCell.transform.position, ItemContainer.GetChild(cellIndex).position);
            var oldIndexDistance = Vector3.Distance(otherCell.transform.position, ItemContainer.GetChild(newIndex).position);
            if (newIndexDistance < oldIndexDistance)
            {
                newIndex = cellIndex;
            }
        }

        var oldHotbarCell = CellPool.FirstOrDefault(cell => cell.ItemInWorld == otherCell.ItemInWorld);
        oldHotbarCell?.Clear();

        var newHotbarCell = CellPool[newIndex];
        newHotbarCell.SetItem(otherCell.ItemInWorld);
    }

    public void AddToEmptySpace(InteractiveItem interactiveItem)
    {
        try
        {
            var hotbarCell = CellPool.FirstOrDefault(cell => cell.ItemInWorld == null);
            hotbarCell.SetItem(interactiveItem);
        }
        catch (Exception exc)
        {
            Debug.LogWarning(exc);
        }
    }

    public override void DropItem(Cell dropedCell)
    {
        try
        {
            var hotbarCell = CellPool.FirstOrDefault(cell => cell.ItemInWorld == dropedCell.ItemInWorld);
            hotbarCell.Clear();
        }
        catch (Exception exc)
        {
            Debug.LogWarning(exc);
        }
    }
}

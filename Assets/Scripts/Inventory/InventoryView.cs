using System;
using UnityEngine;
using UnityEngine.Events;

public class InventoryView : BaseInventoryView
{
    public UnityEvent InventoryShowing;

    [SerializeField] private HotbarView _hotbar;
    private bool _isOpen = false;

    public bool IsOpen => _isOpen;
    public override int ContainerSize => Player.Inventory.Size;

    public void InventoryDisplaySwitch()
    {
        _isOpen = !_isOpen;
        gameObject.SetActive(_isOpen);
        Cursor.lockState = (_isOpen) ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = _isOpen;
        InventoryShowing.Invoke();
    }

    public override void DropItem(Cell dropedCell)
    {
        try
        {
            if(!_hotbar.TryAddOtherCellOnHotbar(dropedCell))
            {
                Player.DropItem(dropedCell.ItemInWorld);
                _hotbar.DropItem(dropedCell);
                dropedCell.Clear();
            }
        }
        catch (Exception exc)
        {
            Debug.LogException(exc);
        }
    }

    public override void Render()
    {
        base.Render();
        RebaseCell();
    }

    private void RebaseCell()
    {
        var items = Inventory.Items;
        for (int i = 0; i < items.Count; i++)
        {
            var cell = CellPool[i];
            if (cell.ItemInWorld != items[i])
            {
                cell.SetItem(items[i]);
            }
        }
    }
}

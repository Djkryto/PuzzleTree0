using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryView : Window
{
    [SerializeField] private HotbarView _hotbar;
    [SerializeField] private CellsFactory _cellFactory;
    [SerializeField] private Transform _itemsContainer;
    [SerializeField] private Transform _backgroundContainer;
    [SerializeField] private Transform _parentContainer;
    [SerializeField] private Player _player;
    private List<Cell> _cellPool;

    public Inventory Inventory => _player.Inventory;

    public int ContainerSize => Inventory.Size;

    private void Awake()
    {
        var inventorySize = _player.Inventory.Size;
        _cellFactory.Init(_itemsContainer, _backgroundContainer, _parentContainer);
        _cellPool = _cellFactory.CreateCells(inventorySize);
        _cellPool.ForEach(cell => cell.Injecting += DropItemFromInventory);
    }

    public void DropItemFromInventory(Cell dropedCell)
    {
        try
        {
            if (!_hotbar.TryAddOtherCellOnHotbar(dropedCell))
            {
                _player.DropItem(dropedCell.ItemInWorld);
                _hotbar.DropItem(dropedCell);
                dropedCell.Clear();
            }
        }
        catch (Exception exc)
        {
            GameLogger.WriteToLog(exc);
        }
    }

    private void OnEnable()
    {
        RebaseCell();
    }

    private void RebaseCell()
    {
        var items = Inventory.Items;
        for (int i = 0; i < items.Count; i++)
        {
            var cell = _cellPool[i];
            if (cell.ItemInWorld != items[i])
            {
                cell.SetItem(items[i]);
            }
        }
    }

    public override void Open()
    {
        _player.Camera.LockCamera();
        SetActiveWindow(true);
        base.Open();
    }

    public override void Close()
    {
        _player.Camera.UnlockCamera();
        SetActiveWindow(false);
        base.Close();
    }

    public void SetActiveWindow(bool active)
    {
        _isOpen = active;
        Cursor.lockState = (_isOpen) ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = _isOpen;
    }
}

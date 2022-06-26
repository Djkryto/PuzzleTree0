using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public UnityEvent<int, ItemSO> ItemAdding;
    public UnityEvent<InventoryCell> ItemDeleting;

    [SerializeField] private int _inventorySize = 9;
    [SerializeField] private InventoryCell _cellTemplate;
    [SerializeField] private Transform _inventoryContainer;
    [SerializeField] private Transform _draggingParent;
    [SerializeField] private Transform _backgroundCellTemplate;
    [SerializeField] private Transform _inventoryBackgroundContainer;
    private List<InventoryCell> _cellPool;
    private bool _isOpen = false;

    public bool IsOpen => _isOpen;
    public List<InventoryCell> CellPool => _cellPool;

    public void OpenCloseInventory()
    { 
        gameObject.SetActive(!_isOpen);
        _isOpen = !_isOpen;
    }

    private void OnEnable()
    {
        Render();
    }

    private void Render()
    {
        if(_cellPool == null)
        {
            _cellPool = new List<InventoryCell>();
            for (int cellIndex = 0; cellIndex < _inventorySize; cellIndex++)
            {
                Instantiate(_backgroundCellTemplate, _inventoryBackgroundContainer);
                var cell = new GameObject("Cell_" + cellIndex);
                cell.transform.SetParent(_inventoryContainer, false);
                cell.AddComponent<RectTransform>();
                var Cell = Instantiate(_cellTemplate, cell.transform);
                Cell.Init(_inventoryContainer, cell.transform, _draggingParent);
                Cell.Injecting += DropItem;
                _cellPool.Add(Cell);
            }
        }
    }

    public void AddItem(ItemSO item)
    {
        try
        {
            var cellIndex = _cellPool.FindIndex(cell => cell.Item == null);
            var cell = _cellPool[cellIndex];
            cell.SetItem(cellIndex, item);
            ItemAdding?.Invoke(cellIndex, item);
        }
        catch(Exception exc)
        {
            Debug.LogException(exc);
        }
    }

    public virtual void DropItem(InventoryCell dropCell)
    {
        try
        {
            ItemDeleting?.Invoke(dropCell);
            var cell = _cellPool.FirstOrDefault(cell => cell == dropCell);
            cell.Clear();
        }
        catch (Exception exc)
        {
            Debug.LogException(exc);
        }
    }
}

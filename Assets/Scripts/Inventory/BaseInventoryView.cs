using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInventoryView : MonoBehaviour
{
    [SerializeField] private Cell _cellTemplate;
    [SerializeField] private Transform _itemContainer;
    [SerializeField] private Transform _backgroundContainer;
    [SerializeField] private Transform _draggingParent;
    [SerializeField] private Transform _backgroundCellTemplate;
    [SerializeField] private Player _player;
    private List<Cell> _cellPool;

    public List<Cell> CellPool => _cellPool;
    public Transform ItemContainer => _itemContainer;
    public Transform BackgroundContainer => _backgroundContainer;
    public Transform DraggingParent => _draggingParent;
    public Transform BackgroundCellTemplate => _backgroundCellTemplate;
    public Cell CellTemplate => _cellTemplate;
    public Inventory Inventory => _player.Inventory;
    public Player Player => _player;
    public abstract int ContainerSize { get;}

    private void OnEnable()
    {
        Render();
    }

    public abstract void DropItem(Cell cell);

    public virtual void Render()
    {
        if (CellPool == null)
            CreateCells(ContainerSize);
    }

    protected void CreateCells(int containerSize)
    {
        _cellPool = new List<Cell>();
        for (int cellIndex = 0; cellIndex < containerSize; cellIndex++)
        {
            Instantiate(_backgroundCellTemplate, _backgroundContainer);
            var cell = new GameObject("Cell_" + cellIndex);
            cell.transform.SetParent(_itemContainer, false);
            cell.AddComponent<RectTransform>();
            var Cell = Instantiate(CellTemplate, cell.transform);
            Cell.Init(_itemContainer, cell.transform, _draggingParent);
            Cell.Injecting += DropItem;
            CellPool.Add(Cell);
        }
    }
}

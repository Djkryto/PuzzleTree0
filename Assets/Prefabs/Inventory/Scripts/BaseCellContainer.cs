using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseCellContainer : MonoBehaviour
{

    [SerializeField] private int _inventorySize = 9;
    [SerializeField] private BaseCell _cellTemplate;
    [SerializeField] private Transform _inventoryContainer;
    [SerializeField] private Transform _draggingParent;
    [SerializeField] private Transform _backgroundCellTemplate;
    [SerializeField] private Transform _inventoryBackgroundContainer;
    private List<BaseCell> _cellPool;

    public List<BaseCell> CellPool => _cellPool;

    public abstract void DropItem(BaseCell dropedCell);

    private void OnEnable()
    {
        Render();
    }

    private void Render()
    {
        if (_cellPool == null)
        {
            _cellPool = new List<BaseCell>();
            for (int cellIndex = 0; cellIndex < _inventorySize; cellIndex++)
            {
                Instantiate(_backgroundCellTemplate, _inventoryBackgroundContainer);
                var cell = new GameObject("Cell_" + cellIndex);
                cell.transform.SetParent(_inventoryContainer, false);
                cell.AddComponent<RectTransform>();
                var Cell = Instantiate(_cellTemplate, cell.transform);
                Cell.Init(cellIndex, _inventoryContainer, cell.transform, _draggingParent);
                Cell.Injecting += DropItem;
                _cellPool.Add(Cell);
            }
        }
    }
}

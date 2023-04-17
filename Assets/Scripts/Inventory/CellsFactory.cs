using System.Collections.Generic;
using UnityEngine;

public class CellsFactory : MonoBehaviour
{
    [SerializeField] private Cell _cellTemplate;
    [SerializeField] private Transform _backgroundCellTemplate;
    [SerializeField] private Transform _itemsContainer;
    [SerializeField] private Transform _backgroundContainer;
    [SerializeField] private Transform _parentContainer;

    public void Init(Transform itemsContainer, Transform backgroundContainer, Transform parentContainer)
    {
        _itemsContainer = itemsContainer;
        _backgroundContainer = backgroundContainer;
        _parentContainer = parentContainer;
    }

    public List<Cell> CreateCells(int numberOfCells)
    {
        var cellPool = new List<Cell>();
        for (int cellIndex = 0; cellIndex < numberOfCells; cellIndex++)
        {
            Instantiate(_backgroundCellTemplate, _backgroundContainer);
            var cell = new GameObject("Cell_" + cellIndex);
            cell.transform.SetParent(_itemsContainer, false);
            cell.AddComponent<RectTransform>();
            var Cell = Instantiate(_cellTemplate, cell.transform);
            Cell.Init(_backgroundContainer, cell.transform, _parentContainer);
            cellPool.Add(Cell);
        }

        return cellPool;
    }
}

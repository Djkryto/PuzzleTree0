using UnityEngine;
using UnityEngine.EventSystems;

public class HotbarCell : BaseCell, IPointerEnterHandler
{
    private InventoryCell _sourceCell;

    public InventoryCell SourceCell => _sourceCell;

    public void SetItem(InventoryCell sourceCell)
    {
        CellImage.sprite = sourceCell.Item.InventoryIcon;
        _sourceCell = sourceCell;
        SetColorAlpha(1f);
    }

    public override void Clear()
    {
        CellImage.sprite = default;
        _sourceCell = default;
        transform.SetParent(CellParent);
        transform.position = CellParent.position;
        SetColorAlpha(0f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var ray = eventData.pointerDrag;
        Debug.Log(ray.gameObject.name);
    }
}

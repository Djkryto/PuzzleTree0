using System.Collections.Generic;
using UnityEngine;

public class ItemInspectorUI : MonoBehaviour
{
    [SerializeField] private ItemInspectorControl _itemInspectorControl;

    private void Awake()
    {
        var itemsControl = new List<ItemControl>(FindObjectsOfType<ItemControl>());
        itemsControl.ForEach(itemControl => itemControl.InspectEvent += OpenItemInspector);
    }
    public void OpenItemInspector(IInspectable item)
    {
        _itemInspectorControl.gameObject.SetActive(true);
        _itemInspectorControl.OpenInspector(item);
    }
}

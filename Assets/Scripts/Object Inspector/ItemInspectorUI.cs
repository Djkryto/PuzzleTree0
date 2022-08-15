using System.Collections.Generic;
using UnityEngine;

public class ItemInspectorUI : MonoBehaviour
{
    [SerializeField] private ItemInspectorControl _itemInspectorControl;

    public void OpenItemInspector(IInspectable item)
    {
        _itemInspectorControl.gameObject.SetActive(true);
        _itemInspectorControl.OpenInspector(item);
    }
}

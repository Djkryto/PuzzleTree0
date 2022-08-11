using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tablet : InteractiveItem,IUseable
{
    [SerializeField] private ItemSO _itemData;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Camera _cameraMain;
    [SerializeField] private Camera _cameraTable;    
    [SerializeField] private Light _lightGreen;
    private ITakeable _takeable;
    public override IPortable Portable => null;

    public override ITakeable Takeable => _takeable;

    public override IInspectable Inspectable => null;

    public override ILearn Learn => null;

    public override IReading Reading => null;

    public override IUseable Useable => this;
    public ItemSO ItemData => _itemData;

    private void Awake()
    {
        _takeable = new TakeableItem(gameObject, _rigidbody, _itemData);
    }
    public void Use()
    {
        _cameraMain.enabled = !_cameraMain.enabled;
        _cameraTable.enabled = !_cameraTable.enabled;
        _lightGreen.enabled = !_lightGreen.enabled;
    }
}

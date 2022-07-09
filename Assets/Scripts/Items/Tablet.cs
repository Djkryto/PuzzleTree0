using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tablet : InteractiveItem,IUseable
{
    [SerializeField] private ItemSO _itemData;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Camera _cameraMain;
    [SerializeField] private Camera _cameraTable;    
    private ITakeable _takeable;
    public override IPortable Portable => throw new System.NotImplementedException();

    public override ITakeable Takeable => _takeable;

    public override IInspectable Inspectable => throw new System.NotImplementedException();

    public override ILearn Learn => throw new System.NotImplementedException();

    public override IReading Reading => throw new System.NotImplementedException();

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
    }
}

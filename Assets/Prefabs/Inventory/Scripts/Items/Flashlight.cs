using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : InteractiveItem, IInspectable, ITakeable, IUseable
{
    [SerializeField] ItemSO _itemData;
    [SerializeField] Transform _lightTransform;

    public Transform ItemTransform => transform;
    public ItemSO ItemData => _itemData;
    public override IPortable Portable => throw new System.NotImplementedException();
    public override ITakeable Takeable => this;
    public override IInspectable Inspectable => this;
    public override IUseable Useable => this;

    public Transform TakeItem()
    {
        gameObject.SetActive(false);
        return transform;
    }

    public void Use()
    {
        var onOffFlashlight = !_lightTransform.gameObject.activeInHierarchy;
        _lightTransform.gameObject.SetActive(onOffFlashlight);
    }
}

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class Flashlight : InteractiveItem, IInspectable, ITakeable, IUseable
{
    [SerializeField] ItemSO _itemData;
    [SerializeField] Transform _lightTransform;
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] AudioSource _audioSource;

    public Transform ItemTransform => transform;
    public ItemSO ItemData => _itemData;
    public override IPortable Portable => throw new System.NotImplementedException();
    public override ITakeable Takeable => this;
    public override IInspectable Inspectable => this;
    public override IUseable Useable => this;

    public Transform TakeItem()
    {
        _rigidbody.isKinematic = true;
        gameObject.SetActive(false);
        return transform;
    }

    public Transform DropItem()
    {
        _rigidbody.isKinematic = false;
        gameObject.SetActive(true);
        return transform;
    }

    public void Use()
    {
        _audioSource.Play();
        var onOffFlashlight = !_lightTransform.gameObject.activeInHierarchy;
        _lightTransform.gameObject.SetActive(onOffFlashlight);
    }
}

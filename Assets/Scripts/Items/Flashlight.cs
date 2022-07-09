using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class Flashlight : InteractiveItem, IInspectable, IUseable
{
    [SerializeField] private ItemSO _itemData;
    [SerializeField] private Transform _lightTransform;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private AudioSource _audioSource;
    private ITakeable _takeable;

    public Transform ItemTransform => transform;
    public ItemSO ItemData => _itemData;
    public override IPortable Portable => throw new System.NotImplementedException();
    public override ITakeable Takeable => _takeable;
    public override IInspectable Inspectable => this;
    public override IUseable Useable => this;

    private void Awake()
    {
        _takeable = new TakeableItem(gameObject, _rigidbody, _itemData);
    }

    public void Use()
    {
        _audioSource.Play();
        var onOffFlashlight = !_lightTransform.gameObject.activeInHierarchy;
        _lightTransform.gameObject.SetActive(onOffFlashlight);
    }
}
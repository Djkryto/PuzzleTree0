using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PortableItem : InteractiveItem, IPortable
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Collider _collider;
    [SerializeField] private float _rotateSpeed;

    public override IPortable Portable => this;
    public override ITakeable Takeable => null;
    public override IInspectable Inspectable => null;
    public override IUseable Useable => null;
    public override IReadable Readable => null;
    public Transform ItemTransform => null;

    public void Rotate(Vector2 rotationVector)
    {
        transform.Rotate(rotationVector * _rotateSpeed * Time.deltaTime);
    }

    public Transform DragItem(Transform parent)
    {
        _rigidbody.isKinematic = true;
        transform.SetParent(parent);
        _collider.enabled = false;
        return transform;
    }

    public Transform DropItem()
    {
        _rigidbody.isKinematic = false;
        transform.SetParent(null);
        _collider.enabled = true;
        return transform;
    }
}

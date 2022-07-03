using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PortableItem : InteractiveItem, IPortable
{
    [SerializeField] private Rigidbody _rigidbody;

    public override IPortable Portable => this;
    public override ITakeable Takeable => throw new System.NotImplementedException();
    public override IInspectable Inspectable => throw new System.NotImplementedException();
    public override IUseable Useable => throw new System.NotImplementedException();

    public Transform ItemTransform => throw new System.NotImplementedException();

    public Transform DragItem(Transform parent)
    {
        _rigidbody.isKinematic = true;
        transform.SetParent(parent);
        return transform;
    }

    public Transform DropItem()
    {
        _rigidbody.isKinematic = false;
        transform.SetParent(null);
        return transform;
    }
}

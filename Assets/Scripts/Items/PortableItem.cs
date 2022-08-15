using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PortableItem : InteractiveItem, IPortable
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Vector3 _rotateAxis;
    [SerializeField] private float _rotateSpeed;

    public override IPortable Portable => this;
    public override ITakeable Takeable => null;
    public override IInspectable Inspectable => null;
    public override IUseable Useable => null;
    public override ILearn Learn => null;
    public override IReading Reading => null;
    public Transform ItemTransform => null;


    public void Rotate(float scrollValue)
    {
        transform.Rotate(scrollValue * _rotateAxis * _rotateSpeed, Space.World);
    }

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

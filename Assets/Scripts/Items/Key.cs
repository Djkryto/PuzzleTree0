using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Key : InteractiveItem, IUseable
{
    [SerializeField] private ItemSO _itemData;
    [SerializeField] private Rigidbody _rigidbody;

    private ITakeable _takeable;

    public override IPortable Portable => null;
    public override ITakeable Takeable => _takeable;
    public override IInspectable Inspectable => null;
    public override IUseable Useable => this;
    public override IReadable Readable => null;
    public ItemSO ItemData => _itemData;

    private void Awake()
    {
        _takeable = new TakeableItem(gameObject, _rigidbody, _itemData);
    }

    public void Use()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if(hit.collider.name == "Door")
            {
                hit.collider.GetComponent<Animator>().enabled = true;
            }
        }
    }
}

using UnityEngine;

public class TakeableItem : ITakeable
{
    public ItemSO ItemData => _itemData;

    private ItemSO _itemData;
    private GameObject _takeableItem;
    private Rigidbody _rigidbody;

    public TakeableItem(GameObject takeableItem, Rigidbody rigidbody, ItemSO itemData)
    {
        _takeableItem = takeableItem;
        _rigidbody = rigidbody;
        _itemData = itemData;
    }

    public Transform TakeItem()
    {
        _rigidbody.isKinematic = true;
        _takeableItem.SetActive(false);
        return _takeableItem.transform;
    }

    public Transform DropItem()
    {
        _rigidbody.isKinematic = false;
        _takeableItem.SetActive(true);
        return _takeableItem.transform;
    }
}

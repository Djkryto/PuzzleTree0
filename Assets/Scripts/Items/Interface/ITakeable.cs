using UnityEngine;

public interface ITakeable
{
    public ItemSO ItemData { get; }
    public Transform TakeItem();
    public Transform DropItem();
}

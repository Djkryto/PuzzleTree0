using UnityEngine;

public interface ITakeable
{
    public ItemSO Item { get; }
    public Transform Take();
}

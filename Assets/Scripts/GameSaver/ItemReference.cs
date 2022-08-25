using UnityEngine.AddressableAssets;

[System.Serializable]
public class ItemReference : AssetReferenceT<ItemSO>
{
    public ItemReference(string guid) : base(guid)
    {
    }
}
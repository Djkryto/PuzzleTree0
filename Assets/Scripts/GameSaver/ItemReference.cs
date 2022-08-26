using UnityEngine.AddressableAssets;

[System.Serializable]
public class ItemReference : AssetReferenceT<SceneSO>
{
    public ItemReference(string guid) : base(guid)
    {
    }
}
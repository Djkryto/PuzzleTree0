using UnityEngine;

[CreateAssetMenu(menuName ="Create Item")]
public class ItemSO : ScriptableObject
{
    [SerializeField] private int _index;
    [SerializeField] private string _name;
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private Sprite _inventoryIcon;

    public int Index => _index;
    public string Name => _name;
    public GameObject ItemPrefab => _itemPrefab;
    public Sprite InventoryIcon => _inventoryIcon;
}

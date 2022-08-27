using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "Puzzle/GameData")]
[System.Serializable]
public class GameData : ScriptableObject
{
    public Dictionary<string, IData> Data;  

    [SerializeField] private List<ItemReference> ItemReference;
    public string MainMenuID => ItemReference[0].AssetGUID;

    public void SaveData()
    {
    }

    // public Dictionary<string, IData> LoadData()
    // {

    // }
}
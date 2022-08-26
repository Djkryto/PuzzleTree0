using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Puzzle/GameData")]
[System.Serializable]
public class GameData : ScriptableObject
{
    public Dictionary<string, IData> Data;  

    [SerializeField] private List<ItemReference> ItemReference;
    // public void SaveData()
    // {

    // }

    // public Dictionary<string, IData> LoadData()
    // {

    // }
}
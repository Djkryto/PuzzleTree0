using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SceneSaver
{
    private string _filePath;
    private SceneSession _sceneSession;

    [Serializable]
    public struct Data
    {
        public string Name;
        public Position Position;

        public Data(string name, Position position)
        {
            Name = name;
            Position = position;
        }
    }

    public SceneSaver(string sceneName, SceneSession sceneSession)
    {
        _filePath = Application.persistentDataPath + "/" + sceneName + ".gamesave";
        _sceneSession = sceneSession;
    }

    

    public void Load(ref List<ItemTransformData> itemsData)
    {
        if (!File.Exists(_filePath))
            return;

        FileStream fileStream = new FileStream(_filePath, FileMode.Open);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        List<Data> items = binaryFormatter.Deserialize(fileStream) as List<Data>;
        Debug.Log(items);
        fileStream.Close();

        foreach (var item in items)
        {
            var oldItemData = itemsData.First(itemInCollection => itemInCollection.Item.name == item.Name);
            var newItemData = new ItemTransformData(oldItemData.Item, item.Position);
            Debug.LogWarning($"{newItemData.Item.name}: {newItemData.ItemPosition.X}, {newItemData.ItemPosition.Y}, {newItemData.ItemPosition.Z}");
            itemsData.Remove(oldItemData);
            itemsData.Add(newItemData);
        }
    }

    public void Save()
    {
        FileStream fileStream = new FileStream(_filePath, FileMode.Create);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        List<Data> items = new List<Data>();
        foreach(var item in _sceneSession.InteractiveItems)
        {
            Data itemData = new Data(item.Item.name, item.ItemPosition);
            items.Add(itemData);
        }
        binaryFormatter.Serialize(fileStream, items);
        fileStream.Close();
    }
}
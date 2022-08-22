using System;

public class PlayerDataSaver
{
    // public Action<SceneData> Saved;

    // private string _filePath;
    // private SceneSession _sceneSession;

    // public PlayerDataSaver(string sceneName, SceneSession sceneSession)
    // {
    //     _filePath = Application.persistentDataPath + "/" + sceneName + ".gamesave";
    //     _sceneSession = sceneSession;
    // }

    // public SceneData Load(ref List<ItemTransformData> itemsData)
    // {
    //     if (!File.Exists(_filePath))
    //         return null;

    //     FileStream fileStream = new FileStream(_filePath, FileMode.Open);
    //     BinaryFormatter binaryFormatter = new BinaryFormatter();
    //     SceneData sceneData = binaryFormatter.Deserialize(fileStream) as SceneData;
    //     fileStream.Close();

    //     return sceneData;
    // }

    // public SceneData Save()
    // {
    //     SceneData sceneData = new SceneData();
    //     foreach (var item in _sceneSession.InteractiveItems)
    //     {
    //         ItemTransformData itemData = new ItemTransformData(item.Item.name, item.ItemPosition);
    //         sceneData.Items.Add(itemData);
    //     }

    //     return sceneData;
    // }
}
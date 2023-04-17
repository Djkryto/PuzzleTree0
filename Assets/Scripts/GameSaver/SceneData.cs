using System.Collections.Generic;

[System.Serializable]
public class SceneData : IData
{
    public string SceneId;
    public List<Riddle> Riddles;
    public List<ItemTransformData> Items;
    
    public string DataId => SceneId;
}
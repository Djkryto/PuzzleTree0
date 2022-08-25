using UnityEngine;

internal class PlayerData : IData
{
    public string PlayerId;
    public Position Position;

    public string DataId => PlayerId;
}
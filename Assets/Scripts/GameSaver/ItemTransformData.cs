using System;
using UnityEngine;

[Serializable]
public struct ItemTransformData
{
    public string Name;
    public Position Position;

    public ItemTransformData(InteractiveItem item, Vector3 itemPosition)
    {
        Name = item.name;
        Position = new Position(itemPosition);
    }

    public ItemTransformData(InteractiveItem item, Position itemPosition)
    {
        Name = item.name;
        Position = itemPosition;
        item.transform.position = itemPosition.ToVector3();
    }
}
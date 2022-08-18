using System;
using UnityEngine;

[Serializable]
public struct ItemTransformData
{
    public InteractiveItem Item;
    public Position ItemPosition;

    public ItemTransformData(InteractiveItem item, Vector3 itemPosition)
    {
        Item = item;
        ItemPosition = new Position(itemPosition);
    }

    public ItemTransformData(InteractiveItem item, Position itemPosition)
    {
        Item = item;
        ItemPosition = itemPosition;
        item.transform.position = itemPosition.ToVector3();
    }
}
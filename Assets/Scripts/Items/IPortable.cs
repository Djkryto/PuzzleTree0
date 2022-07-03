using UnityEngine;

public interface IPortable
{
    public Transform ItemTransform { get; }
    public Transform DragItem(Transform parent);
    public Transform DropItem();
}

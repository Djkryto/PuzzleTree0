using UnityEngine;

[RequireComponent(typeof(ItemControl))]
public abstract class InteractiveItem : MonoBehaviour
{
    [SerializeField] private ItemControl _itemControl;

    public ItemControl ItemControl => _itemControl;
    public abstract IPortable Portable { get; }
    public abstract ITakeable Takeable { get; }
    public abstract IInspectable Inspectable { get; }
    public abstract ILearn Learn { get; }
    public abstract IReading Reading { get; }
    public abstract IUseable Useable { get; }
}
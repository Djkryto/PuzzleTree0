using UnityEngine;

public class InventoryControl : InputControl
{
    [SerializeField] private InventoryView _inventoryView;
    [SerializeField] private Player _player;

    public override void Init(UserInput input)
    {
        base.Init(input);

    }

    public override void Enable()
    {
    }

    public override void Disable()
    {
    }
}

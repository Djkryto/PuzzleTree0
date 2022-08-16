public class InventoryControlState : ControlState
{
    private InventoryView _inventoryView;

    public InventoryControlState(Player player, InventoryView inventoryView) : base(player)
    {
        _inventoryView = inventoryView;
        _inventoryView.InventoryDisplaySwitch();
        Input.Player.Escape.performed += context => CloseInventory();
        Input.Player.Inventory.performed += context => CloseInventory();
    }

    public void CloseInventory()
    {
        _inventoryView.InventoryDisplaySwitch();
        OnExitState.Invoke();
    }
}
public class ReadableControlState : ControlLockState
{
    private IReadable _readableItem;

    public ReadableControlState(Player player) : base(player)
    {
        Input.Player.Escape.performed += context => CloseText();
        _readableItem = player.Vision.InteractiveItem.Readable;
        _readableItem.ReadText();
    }

    public void CloseText()
    {
        _readableItem.CloseText();
        ControlUnlock();
    }
}
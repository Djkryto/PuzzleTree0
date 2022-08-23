public class ControlLockState : ControlState
{
    public ControlLockState(Player player) : base(player)
    {
        Input.Player.Escape.performed += context => ControlUnlock();
    }

    public void ControlUnlock()
    {
        OnExitState?.Invoke();
    }
}
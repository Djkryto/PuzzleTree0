public class ControlLockState : ControlState
{
    public ControlLockState(Player player) : base(player)
    {
        Input.Player.Escape.performed += context => ControlUnclock();
    }

    public void ControlUnclock()
    {
        OnChangeState?.Invoke();
    }
}
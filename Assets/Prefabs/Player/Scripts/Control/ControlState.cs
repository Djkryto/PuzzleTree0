using System;

public abstract class ControlState
{
    public Action OnChangeState;

    private Player _player;
    private UserInput _input;

    public Player Player => _player;
    public UserInput Input => _input;

    public ControlState(Player player)
    {
        _player = player;
        _input = new UserInput();
    }

    public void EnableControl()
    {
        _input.Enable();
    }

    public void DisableControl()
    {
        _input.Disable();
    }
}
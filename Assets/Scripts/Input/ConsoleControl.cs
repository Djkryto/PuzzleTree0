using GameConsole.UI;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class ConsoleControl : InputControl
{
    [SerializeField] private ConsoleUI _consoleUI;

    public override void Enable()
    {
        _input.Player.Console.performed += OpenOrClose;
        _consoleUI.Opened += InputManager.DisableInput<PlayerControl>;
        _consoleUI.Closed += InputManager.EnableInput<PlayerControl>;
    }

    public override void Disable()
    {
        _input.Player.Console.performed -= OpenOrClose;
        _consoleUI.Opened -= InputManager.DisableInput<PlayerControl>;
        _consoleUI.Closed -= InputManager.EnableInput<PlayerControl>;
    }

    private void OpenOrClose(CallbackContext context)
    {
        _consoleUI.OpenOrClose();
        if(_consoleUI.IsOpen)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;

    }
}

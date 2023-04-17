using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    [SerializeField] private List<InputControl> _inputControls;

    private UserInput _input;

    private void Awake()
    {
        _input = new UserInput();
        _inputControls.ForEach(input => input.Init(_input));
        Cursor.lockState = CursorLockMode.Locked;
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        _input.Enable();
        _inputControls.ForEach(input => input.Enable());
    }

    private void OnDisable()
    {
        _input.Disable();
        _inputControls.ForEach(input => input.Disable());
    }

    public static void DisableInput<InputType>() where InputType : InputControl
    {
        var input = GetInputControl<InputType>();
        if (input == null)
            return;
        input.Disable();
    }

    public static void EnableInput<InputType>() where InputType : InputControl
    {
        var input = GetInputControl<InputType>();
        if (input == null)
            return;
        input.Enable();
    }

    public static InputControl GetInputControl<InputType>() where InputType : InputControl
    {
        return Instance._inputControls.FirstOrDefault(inputControl => inputControl is InputType);
    }
}

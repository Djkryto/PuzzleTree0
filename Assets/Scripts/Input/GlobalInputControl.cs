using UnityEngine.InputSystem.Interactions;

public class GlobalInputControl : InputControl
{
    public override void Enable()
    {
        _input.Player.Escape.performed += context =>
        {
            if(context.interaction is PressInteraction)
                UIManager.CloseLastWindow();
            if(context.interaction is HoldInteraction)
                UIManager.CloseAllWindows();
        };
    }

    public override void Disable()
    {
        _input.Player.Escape.performed -= context =>
        {
            if (context.interaction is PressInteraction)
                UIManager.CloseLastWindow();
            if (context.interaction is HoldInteraction)
                UIManager.CloseAllWindows();
        };
    }
}

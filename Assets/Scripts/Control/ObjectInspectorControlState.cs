using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class ObjectInspectorControlState : ControlState
{
    private ObjectInspector _objectInspector;

    public ObjectInspectorControlState(Player player, ObjectInspector objectInspector) : base(player)
    {
        _objectInspector = objectInspector;
        Input.Player.Escape.performed += context => CloseInspector();
        Input.Player.Scroll.performed += context => ObjectZoom(context);
        Input.Player.MouseLook.performed += context => ObjectRotate();
        var inspectableItem = player.Vision.InteractiveItem.Inspectable;
        OpenInspector(inspectableItem);
    }

    public void OpenInspector(IInspectable item)
    {
        _objectInspector.gameObject.SetActive(true);
        _objectInspector.TryOpenInspector();
        _objectInspector.CreateObjectInInspector(item);
    }

    private void CloseInspector()
    {
        _objectInspector.TryCloseInspector();
        OnExitState.Invoke();
    }

    private void ObjectZoom(CallbackContext context)
    {
        var scroll = context.ReadValue<Vector2>();
        _objectInspector.ObjectZoom(scroll.y);
    }

    private void ObjectRotate()
    {
        if (Input.Player.LMB.IsPressed())
        {
            var mouseVector = Input.Player.MouseLook.ReadValue<Vector2>();
            _objectInspector.RotateObject(mouseVector);
        }
    }
}
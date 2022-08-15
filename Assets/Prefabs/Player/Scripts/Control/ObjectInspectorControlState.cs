using System.Threading.Tasks;
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
    }

    public void OpenInspector(IInspectable item)
    {
        _objectInspector.gameObject.SetActive(true);
        _objectInspector.CreateObjectInInspector(item);
        _objectInspector.TryOpenInspector();
    }

    private void CloseInspector()
    {
        _objectInspector.TryCloseInspector();
        OnChangeState.Invoke();
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
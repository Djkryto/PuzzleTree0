using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class InputObjectInspector : MonoBehaviour
{
    [SerializeField] private Transform _inspectorRotatorTransform;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private ObjectInspector _objectInspector;
    private UserInput _input;

    private void Awake()
    {
        _input = new UserInput();
        _playerInput.InspectEvent += OpenInspector;
        _input.Player.Escape.performed += context => CloseInspector();
        _input.Player.Scroll.performed += context => ObjectZoom(context);
    }

    private void OpenInspector(IInspectable item)
    {
        _objectInspector.gameObject.SetActive(true);
        _objectInspector.CreateObjectInInspector(item);
        _objectInspector.TryOpenInspector();
    }

    private void CloseInspector()
    {
        _objectInspector.TryCloseInspector();
        _playerInput.ControlLock();
    }

    private void ObjectZoom(CallbackContext context)
    {
        var scroll = context.ReadValue<Vector2>();
        _objectInspector.ObjectZoom(scroll.y);
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    void Update()
    {
        ObjectRotate();
    }

    private void ObjectRotate()
    {
        if (_input.Player.LMB.IsPressed())
        {
            var mouseVector = _input.Player.MouseLook.ReadValue<Vector2>();
            _objectInspector.RotateObject(mouseVector);
        }
    }

    private void OnDisable()
    {
        _input.Disable();
    }
}

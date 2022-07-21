using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.InputSystem.InputAction;

public class ItemInspectorControl : MonoBehaviour
{
    public UnityEvent InspectorOpened;
    public UnityEvent InspectorClosed;

    [SerializeField] private Transform _inspectorRotatorTransform;
    [SerializeField] private PlayerControl _playerInput;
    [SerializeField] private ItemInspector _objectInspector;
    private UserInput _input;

    private void Awake()
    {
        _input = new UserInput();
        _input.Player.Escape.performed += context => CloseInspector();
        _input.Player.Scroll.performed += context => ObjectZoom(context);
    }

    public void OpenInspector(IInspectable item)
    {
        _objectInspector.gameObject.SetActive(true);
        _objectInspector.CreateObjectInInspector(item);
        _objectInspector.TryOpenInspector();
        InspectorOpened?.Invoke();
    }

    private void CloseInspector()
    {
        _objectInspector.TryCloseInspector();
        _playerInput.ControlLock();
        InspectorClosed?.Invoke();
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

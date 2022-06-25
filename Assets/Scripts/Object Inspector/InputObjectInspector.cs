using Cinemachine;
using UnityEngine;

public class InputObjectInspector : MonoBehaviour
{
    [SerializeField] private Transform _inspectorRotatorTransform;
    [SerializeField] private PlayerControl _playerControl;
    [SerializeField] private ObjectInspector _objectInspector;
    [SerializeField] private float _minZoom = 2.0f;
    [SerializeField] private float _maxZoom = 10.0f;
    [SerializeField] private float _zoomMultiplier = 0.2f;
    [SerializeField] private float _rotateSpeedPerSecond = 100f;
    private PlayerInput _input;

    private void Awake()
    {
        _input = new PlayerInput();
    }

    void OnEnable()
    {
        _inspectorRotatorTransform.rotation = Quaternion.identity;
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    void Update()
    {
        ObjectRotate();
        ObjectZoom();
        Close();
    }

    private void ObjectRotate()
    {
        if (_input.Player.LMB.IsPressed())
        {
            var mouseVector = _input.Player.MouseLook.ReadValue<Vector2>();
            var rotationvector = new Vector3(mouseVector.y, -mouseVector.x, 0f);
            _inspectorRotatorTransform.Rotate(rotationvector * _rotateSpeedPerSecond * Time.deltaTime, Space.World);
        }
    }

    private void ObjectZoom()
    {
        var scroll = _input.Player.Scroll.ReadValue<Vector2>();
        if (scroll.y > 0f)
        {
            var newPositionZ = _inspectorRotatorTransform.localPosition.z + _zoomMultiplier;
            newPositionZ = Mathf.Clamp(newPositionZ, _minZoom, _maxZoom);
            _inspectorRotatorTransform.localPosition = new Vector3(0f, 0f, newPositionZ);
        }
        else if (scroll.y < 0f)
        {
            var newPositionZ = _inspectorRotatorTransform.localPosition.z - _zoomMultiplier;
            newPositionZ = Mathf.Clamp(newPositionZ, _minZoom, _maxZoom);
            _inspectorRotatorTransform.localPosition = new Vector3(0f, 0f, newPositionZ);
        }
    }

    private void Close()
    {
        if(_input.Player.Escape.IsPressed())
        {
            _objectInspector.TryCloseInspector();
            _playerControl.ControlUnlock();
        }
    }
}

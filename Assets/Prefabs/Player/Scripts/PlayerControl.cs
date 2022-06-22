using UnityEngine;
using UnityEngine.Events;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private LayerMask _layerInteractiveObject;
    [SerializeField] private  UnityEvent<IInspectable> _inspectableEvent;
    private PlayerInput _input;

    private void Awake()
    {
        _input = new PlayerInput();
    }

    private void OnEnable()
    {
        _input?.Enable();
    }

    private void OnDisable()
    {
        _input?.Disable();
    }

    private void Update()
    {
        LookAt();
        Use();
    }

    private void LookAt()
    {
        var mouseMoved = _input.Player.MouseLook.ReadValue<Vector2>() != Vector2.zero;
        if (mouseMoved)
        {
            var rayCenterCamera = GetRayFromCameraCenter();
            _player.LookAt(rayCenterCamera);
        }
    }

    private void Use()
    {
        var usePressed = _input.Player.Use.IsPressed();
        if(usePressed)
        {
            var rayCenterCamera = GetRayFromCameraCenter();
            if (Physics.Raycast(rayCenterCamera, out RaycastHit hitInfo, Mathf.Infinity, _layerInteractiveObject))
            {
                if(hitInfo.transform.TryGetComponent(out IInspectable inspectableObject))
                {
                    _inspectableEvent.Invoke(inspectableObject);
                }
            }
            
        }
    }

    private Ray GetRayFromCameraCenter()
    {
        var cameraCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        return _playerCamera.ScreenPointToRay(cameraCenter);
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        var movementVector = _input.Player.Move.ReadValue<Vector2>();
        if (movementVector != Vector2.zero)
        {
            if (_input.Player.Run.IsPressed())
            {
                _player.Run(movementVector);
            }
            else
            {
                _player.Walk(movementVector);
            }
        }
        else
        {
            _player.Decceleration(movementVector);
        }

        _player.Rotate(_playerCamera.transform.eulerAngles.y);
    }
}
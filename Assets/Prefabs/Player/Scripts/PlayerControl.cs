using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private LayerMask _layerInteractiveObject;
    [SerializeField] private  UnityEvent<IInspectable> _inspectableEvent;
    [SerializeField] private  float _useDistance = 5f;
    [SerializeField] private CinemachineInputProvider _cinemachineInputProvider;
    private PlayerInput _input;
    private bool _controlIsLock = false;

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
        if(!_controlIsLock)
        {
            var rayCenterCamera = GetRayFromCameraCenter();
            LookAt(rayCenterCamera);
            Use(rayCenterCamera);
        }
    }

    private Ray GetRayFromCameraCenter()
    {
        var cameraCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        return _playerCamera.ScreenPointToRay(cameraCenter);
    }

    private void LookAt(Ray rayCenterCamera)
    {
        var mouseMoved = _input.Player.MouseLook.ReadValue<Vector2>() != Vector2.zero;
        if (mouseMoved)
        {
            _player.LookAt(rayCenterCamera);
        }
    }

    private void Use(Ray rayCenterCamera)
    {
        var usePressed = _input.Player.Use.IsPressed();
        if(usePressed)
        {
            if (Physics.Raycast(rayCenterCamera, out RaycastHit hitInfo, _useDistance, _layerInteractiveObject))
            {
                if(hitInfo.transform.TryGetComponent(out IInspectable inspectableObject))
                {
                    _controlIsLock = true;
                    _cinemachineInputProvider.enabled = false;
                    _inspectableEvent.Invoke(inspectableObject);
                }
            }
            
        }
    }

    private void FixedUpdate()
    {
        if (!_controlIsLock)
        {
            PlayerMove();
        }
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

    public void ControlUnlock()
    {
        _controlIsLock = false;
        _cinemachineInputProvider.enabled = true;
    }
}
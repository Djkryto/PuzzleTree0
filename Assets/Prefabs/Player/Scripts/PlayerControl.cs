using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private UnityEvent<IInspectable> _inspectEvent;
    [SerializeField] private UnityEvent<ITakeable> _takeEvent;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private HotBar _hotbar;
    [SerializeField] private CinemachineInputProvider _cinemachineInputProvider;
    private PlayerInput _input;
    private bool _controlIsLock = false;

    private void Awake()
    {
        _input = new PlayerInput();
        _input.Player.Inventory.performed += context => OpenInventory();
        _input.Player.Use.canceled += context => _player.TakeObject();
        _input.Player.Use.performed += context => InspectObject();
        _input.Player.One.performed += context => SetItemInHead(0);
        _input.Player.Two.performed += context => SetItemInHead(1);
        _input.Player.Three.performed += context => SetItemInHead(2);
    }

    private void OnEnable()
    {
        _input?.Enable();
    }

    private void OnDisable()
    {
        _input?.Disable();
    }

    private void OpenInventory()
    {
        _inventory.InventoryActive();
    }

    private void Update()
    {
        if(!_controlIsLock)
        {
            var rayCenterCamera = GetRayFromCameraCenter();
            LookAt(rayCenterCamera);
            _player.ScanObjectInFront(rayCenterCamera);
        }
    }

    private Ray GetRayFromCameraCenter()
    {
        var cameraCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        return _playerCamera.ScreenPointToRay(cameraCenter);
    }

    private void SetItemInHead(int index)
    {
        try
        {
            HotbarCell cellItem = (HotbarCell)_hotbar.CellPool[index];
            _player.SetItemInHand(cellItem.SourceCell);
        }
        catch(Exception exception)
        {
            Debug.LogException(exception);
        }
    }

    private void LookAt(Ray rayCenterCamera)
    {
        var mouseMoved = _input.Player.MouseLook.ReadValue<Vector2>() != Vector2.zero;
        if (mouseMoved)
        {
            _player.LookAt(rayCenterCamera);
        }
    }

    private void InspectObject()
    {
        ControlLock();
        IInspectable inspectableObject = _player.Vision.InspectableObject;
        if(inspectableObject != null)
            _inspectEvent?.Invoke(inspectableObject);
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

    public void ControlLock()
    {
        _controlIsLock = !_controlIsLock;
        Cursor.lockState = (_controlIsLock) ? CursorLockMode.None : CursorLockMode.Locked;
        _cinemachineInputProvider.enabled = !_cinemachineInputProvider.enabled;
    }
}
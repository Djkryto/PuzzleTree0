using Cinemachine;
using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static UserInput Input;

    [SerializeField] private Player _player;
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private InventoryUI _inventoryUI;
    [SerializeField] private HotBar _hotbar;
    [SerializeField] private CinemachineInputProvider _cinemachineInputProvider;
    private bool _controlIsLock = false;

    public bool ControlIsLock => _controlIsLock;

    private void Awake()
    {   
        Input = new UserInput();
        Input.Player.Inventory.performed += context => OpenInventory();
        Input.Player.One.performed += context => SetItemInHead(0);
        Input.Player.Two.performed += context => SetItemInHead(1);
        Input.Player.Three.performed += context => SetItemInHead(2);
    }

    private void OpenInventory()
    {
        _inventoryUI.InventoryActive();
        ControlLock();
    }

    private void SetItemInHead(int index)
    {
        try
        {
            HotbarCell cellItem = (HotbarCell)_hotbar.CellPool[index];
            _player.SetItemInHand(cellItem.SourceCell);
        }
        catch (Exception exception)
        {
            Debug.LogException(exception);
        }
    }

    private void OnEnable()
    {
        Input?.Enable();
    }

    private void Update()
    {
        if(!_controlIsLock)
        {
            var rayCenterCamera = GetRayFromCameraCenter();
            LookAt(rayCenterCamera);
        }
    }

    private Ray GetRayFromCameraCenter()
    {
        var cameraCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        return _playerCamera.ScreenPointToRay(cameraCenter);
    }

    private void LookAt(Ray rayCenterCamera)
    {
        var mouseMoved = Input.Player.MouseLook.ReadValue<Vector2>() != Vector2.zero;
        if (mouseMoved)
        {
            _player.LookAt(rayCenterCamera);
        }
    }

    private void FixedUpdate()
    {
        if (!_controlIsLock)
        {
            PlayerMove();
            var rayCenterCamera = GetRayFromCameraCenter();
            _player.ScanObjectInFront(rayCenterCamera);
        }

    }

    private void PlayerMove()
    {
        var movementVector = Input.Player.Move.ReadValue<Vector2>();
        if (movementVector != Vector2.zero)
        {
            if (Input.Player.Run.IsPressed())
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
            _player.Decceleration();
        }

        _player.Rotate(_playerCamera.transform.eulerAngles.y);
    }

    private void OnDisable()
    {
        Input?.Disable();
    }

    public void ControlLock()
    {
        _controlIsLock = !_controlIsLock;
        _cinemachineInputProvider.enabled = !_cinemachineInputProvider.enabled;
    }
}
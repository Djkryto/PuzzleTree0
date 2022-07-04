using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.Interactions;

public class PlayerControl : MonoBehaviour
{
    public static PlayerInput Input;

    [SerializeField] private Player _player;
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private UnityEvent<IInspectable> _inspectEvent;
    [SerializeField] private UnityEvent<ITakeable> _takeEvent;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private HotBar _hotbar;
    [SerializeField] private CinemachineInputProvider _cinemachineInputProvider;
    private bool _controlIsLock = false;
    private bool _holdLMB = false;

    private void Awake()
    {   
        Input = new PlayerInput();
        Input.Player.Inventory.performed += context => OpenInventory();
        Input.Player.LMB.started += context => { _holdLMB = true; };
        Input.Player.LMB.canceled += context => { _holdLMB = false; };
        Input.Player.LMB.performed += context => 
        {
            if (context.interaction is PressInteraction) 
                UseItem();
        };

        Input.Player.Use.performed += context =>
        {
            if (context.interaction is HoldInteraction)
                InspectObject();
            if (context.interaction is PressInteraction)
                TakeItem();

        };
        Input.Player.LMB.performed += context => DragItem();
        Input.Player.One.performed += context => SetItemInHead(0);
        Input.Player.Two.performed += context => SetItemInHead(1);
        Input.Player.Three.performed += context => SetItemInHead(2);
    }

    private void OnEnable()
    {
        Input?.Enable();
    }

    private void OnDisable()
    {
        Input?.Disable();
    }

    private void OpenInventory()
    {
        _inventory.InventoryActive();
    }

    private void TakeItem()
    {
        if (!_controlIsLock)
            _player.TakeObject();
    }

    private void Update()
    {
        if(!_controlIsLock)
        {
            var rayCenterCamera = GetRayFromCameraCenter();
            LookAt(rayCenterCamera);
            DragItem();
        }
    }

    private void UseItem()
    {
        try
        {
            _player.CurrentItemInHand.Useable.Use();
        }
        catch { }
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

    private void DragItem()
    {
        try
        {
            if (_holdLMB)
            {
                _player.DragItem();
            }
            else
            {
                _player.DropPortableItem();
            }
        }
        catch { }
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

    private void InspectObject()
    {
        IInspectable inspectableObject = _player.Vision.InteractiveItem.Inspectable;
        if(inspectableObject != null && !_controlIsLock)
        {
            ControlLock();
            _inspectEvent?.Invoke(inspectableObject);
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
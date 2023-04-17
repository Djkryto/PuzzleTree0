using Cinemachine;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class PlayerControl : InputControl
{
    public Action ItemDragStarted;
    public Action ItemDragFinished;

    [SerializeField] private Player _player;
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private InventoryView _inventoryView;
    [SerializeField] private ObjectInspector _objectInspector;
    [SerializeField] private HotbarView _hotbar;
    private ControlState _controlState;
    private Coroutine _movingItemProcessEnumerator;

    public ControlState ControlState => _controlState;
    public HotbarView HotbarView => _hotbar;

    public override void Enable()
    {
        _input.Player.One.performed += context => SetItemWithIndexInHand(0);
        _input.Player.Two.performed += context => SetItemWithIndexInHand(1);
        _input.Player.Three.performed += context => SetItemWithIndexInHand(2);
        _input.Player.LMB.started += context => StartMovingItem();
        _input.Player.LMB.canceled += context => EndMovingItem();
        _input.Player.LMB.performed += context => UseItemInHand();
        _input.Player.Inventory.performed += context => OpenOrCloseInventory();
        _input.Player.Use.performed += context =>
        {
            if (context.interaction is PressInteraction)
                UseOrTakeItem();
            if (context.interaction is HoldInteraction)
                InspectItem();
        };
    }

    public override void Disable()
    {
        _input.Player.One.performed -= context => SetItemWithIndexInHand(0);
        _input.Player.Two.performed -= context => SetItemWithIndexInHand(1);
        _input.Player.Three.performed -= context => SetItemWithIndexInHand(2);
        _input.Player.LMB.started -= context => StartMovingItem();
        _input.Player.LMB.canceled -= context => EndMovingItem();
        _input.Player.LMB.performed -= context => UseItemInHand();
        _input.Player.Inventory.performed -= context => OpenOrCloseInventory();
        _input.Player.Use.performed -= context =>
        {
            if (context.interaction is PressInteraction)
                UseOrTakeItem();
            if (context.interaction is HoldInteraction)
                InspectItem();
        };
    }

    private void SetItemWithIndexInHand(int index)
    {
        try
        {
            Cell cell = _hotbar.CellPool[index];
            _player.SetItemInHand(cell.ItemInWorld);
        }
        catch (Exception exception)
        {
            GameLogger.WriteToLog(exception);
        }
    }

    private void StartMovingItem()
    {
        if (_inventoryView.IsOpen)
            return;
        _movingItemProcessEnumerator = StartCoroutine(MovingItemProcess());
        _player.DragItem();
        ItemDragStarted?.Invoke();
    }

    private void EndMovingItem()
    {
        StopCoroutine(_movingItemProcessEnumerator);
        _player.DropPortableItem();
        if (!_inventoryView.IsOpen)
            _player.Camera.UnlockCamera();
        ItemDragFinished?.Invoke();
    }

    private IEnumerator MovingItemProcess()
    {
        while(true)
        {
            var rmbPressed = _input.Player.RMB.IsPressed();
            if (rmbPressed)
            {
                var mouseVector = _input.Player.MouseLook.ReadValue<Vector2>();
                var rotationVector = new Vector3(mouseVector.y, -mouseVector.x, 0f);
                _player.RotateObject(rotationVector);
                _player.Camera.LockCamera();
            }
            else if(!_inventoryView.IsOpen)
            {
                _player.Camera.UnlockCamera();
            }
            yield return null;
        }
    }

    private void UseItemInHand()
    {
        try
        {
            _player.CurrentItemInHand.Useable.Use();
        }
        catch (Exception exception)
        {
            GameLogger.WriteToLog(exception);
        }
    }

    public void OpenOrCloseInventory()
    {
        if (_inventoryView.IsOpen)
            _inventoryView.Close();
        else
            _inventoryView.Open();
    }

    private void UseOrTakeItem()
    {
        _player.TakeObject();
    }

    private void InspectItem()
    {

    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    public void PlayerMove()
    {
        _player.Rotate(_playerCamera.transform.eulerAngles.y);
        var movementVector = _input.Player.Move.ReadValue<Vector2>();
        if(movementVector == Vector2.zero)
        {
            _player.Decceleration();
            return;
        }

        if (_input.Player.Run.IsPressed())
        {
            _player.Run(movementVector);
        }
        else
        {
            _player.Walk(movementVector);
        }
    }
}
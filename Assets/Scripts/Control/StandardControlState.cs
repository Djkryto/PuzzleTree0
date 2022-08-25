using System;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class StandardControlState : ControlState
{
    public Action OnNotepadOpen;
    public Action OnInventoryOpen;

    private Camera _playerCamera;
    private HotbarView _hotbar;

    public StandardControlState(Player player, Camera playerCamera, HotbarView hotbar) : base(player)
    {
        _playerCamera = playerCamera;
        _hotbar = hotbar;
        Input.Player.Escape.performed += context => OpenMenu();
        Input.Player.Inventory.performed += context => OpenInventory();
        Input.Player.One.performed += context => SetItemInHead(0);
        Input.Player.Two.performed += context => SetItemInHead(1);
        Input.Player.Three.performed += context => SetItemInHead(2);
        Input.Player.LMB.performed += context =>
        {
            if (context.interaction is PressInteraction)
                UseItem();
        };
    }

    private void OpenInventory()
    {
        OnInventoryOpen?.Invoke();
    }

    private void OpenMenu()
    {
        OnExitState?.Invoke();
    }

    private void SetItemInHead(int index)
    {
        try
        {
            Cell cell = _hotbar.CellPool[index];
            Player.SetItemInHand(cell.ItemInWorld);
        }
        catch (Exception exception)
        {
            Debug.LogException(exception);
        }
    }

    public void ScanObjectInFront()
    {
        var rayCenterCamera = GetRayFromCameraCenter();
        Player.ScanObjectInFront(rayCenterCamera);
    }

    private Ray GetRayFromCameraCenter()
    {
        var cameraCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        return _playerCamera.ScreenPointToRay(cameraCenter);
    }

    public void TiltPlayerTorso()
    {
        var mouseLook = Input.Player.MouseLook.ReadValue<Vector2>();
        TiltPlayerTorso(mouseLook);
    }

    public void TiltPlayerTorso(Vector2 mouseLook)
    {
        var rayCenterCamera = GetRayFromCameraCenter();
        var mouseMoved = mouseLook != Vector2.zero;
        if (mouseMoved)
        {
            Player.LookAt(rayCenterCamera);
        }
    }

    public void PlayerMove()
    {
        var movementVector = Input.Player.Move.ReadValue<Vector2>();
        if (movementVector != Vector2.zero)
        {
            if (Input.Player.Run.IsPressed())
            {
                Player.Run(movementVector);
            }
            else
            {
                Player.Walk(movementVector);
            }
        }
        else
        {
            Player.Decceleration();
        }

        Player.Rotate(_playerCamera.transform.eulerAngles.y);
    }

    private void UseItem()
    {
        try
        {
            Player.CurrentItemInHand.Useable.Use();
        }
        catch (Exception exception)
        {
            Debug.LogWarning(exception);
        }
    }
}
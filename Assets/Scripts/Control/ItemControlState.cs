using System;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class ItemControlState : StandardControlState
{
    public Action<IInspectable> InspectEvent;

    private bool _dragItem = false;

    public ItemControlState(Player player, Camera playerCamera, HotbarView hotbar) : base(player, playerCamera, hotbar)
    {
        SetPortableControl();
        SetTakeControl();
        SetInspectControl();
        SetRotateObject();
    }

    private void SetRotateObject()
    {
        Input.Player.Scroll.performed += context => RotateObject();
    }

    private void RotateObject()
    {
        try
        {
            var scrollValue = Input.Player.Scroll.ReadValue<Vector2>();
            Player.RotateObject(scrollValue.y);
        }
        catch (Exception exception)
        {
            Debug.LogWarning(exception);
        }
    }

    private void SetPortableControl()
    {
        Input.Player.LMB.started += context => { _dragItem = true; DragItem(); };
        Input.Player.LMB.canceled += context => { _dragItem = false; DragItem(); };
        Input.Player.MouseLook.performed += context => { DragItem(); };
    }

    private void SetTakeControl()
    {
        Input.Player.Use.performed += context =>
        {
            if (context.interaction is PressInteraction)
                TakeItem();
        };
    }

    private void TakeItem()
    {
        try
        {
            Player.TakeObject();
        }
        catch (Exception exception)
        {
            Debug.LogWarning(exception);
        }
    }

    private void SetInspectControl()
    {
        Input.Player.Use.performed += context =>
        {
            if (context.interaction is HoldInteraction)
                InspectObject();
        };
    }

    private void InspectObject()
    {
        try
        {
            IInspectable inspectableObject = Player.Vision.InteractiveItem.Inspectable;
            if (inspectableObject != null)
            {
                //PlayerInput.ControlLock();
                InspectEvent?.Invoke(inspectableObject);
            }
        }
        catch (Exception exception)
        {
            Debug.LogWarning(exception);
        }
    }

    private void SetLearnControl()
    {
        //Здесь необходимо добавить описание привязки логики к клавишам
        //Необходимо отлавливать ошибки try-catch-finally
    }

    private void SetReadControl()
    {
        //Здесь необходимо добавить описание привязки логики к клавишам 
        //Необходимо отлавливать ошибки try-catch-finally
    }

    private void DragItem()
    {
        if (_dragItem)
        {
            Player.DragItem();
        }
        else
        {
            Player.DropPortableItem();
        }
    }
}
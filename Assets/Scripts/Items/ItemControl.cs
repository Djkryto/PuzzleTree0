using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class ItemControl : MonoBehaviour
{
    public Action<IInspectable> InspectEvent;

    private UserInput _input;
    private bool _dragItem = false;
    private Player _player;
    private PlayerControl _playerInput;

    private void Awake()
    {
        _input = new UserInput();
        _player = FindObjectOfType<Player>();
        _playerInput = FindObjectOfType<PlayerControl>();
    }

    private void Start()
    {
        SetPortableControl();
        SetTakeControl();
        SetInspectControl();
        SetUseControl();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void SetPortableControl()
    {
        _input.Player.LMB.started += context => { _dragItem = true; StartCoroutine(DragItem()); };
        _input.Player.LMB.canceled += context => { _dragItem = false; };
    }

    private void SetTakeControl()
    {
        _input.Player.Use.performed += context =>
        {
            if (context.interaction is PressInteraction)
                TakeItem();
        };
    }

    private void TakeItem()
    {
        try
        {
            if (!_playerInput.ControlIsLock)
                _player.TakeObject();
        }
        catch (Exception exception)
        {
            Debug.LogWarning(exception);
        }
    }

    private void SetInspectControl()
    {
        _input.Player.Use.performed += context =>
        {
            if (context.interaction is HoldInteraction)
                InspectObject();
        };
    }

    private void InspectObject()
    {
        try
        {
            IInspectable inspectableObject = _player.Vision.InteractiveItem.Inspectable;
            if (inspectableObject != null && !_playerInput.ControlIsLock)
            {
                _playerInput.ControlLock();
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

    private void SetUseControl()
    {
        _input.Player.LMB.performed += context =>
        {
            if (context.interaction is PressInteraction)
                UseItem();
        };
    }

    private void UseItem()
    {
        try
        {
            _player.CurrentItemInHand.Useable.Use();
        }
        catch (Exception exception)
        {
            Debug.LogWarning(exception);
        }
    }

    private void OnDisable()
    {
        _input.Disable();
    }
    private IEnumerator DragItem()
    {
        while (_dragItem)
        {
            _player.DragItem();
            yield return null;
        }
        _player.DropPortableItem();
    }
}
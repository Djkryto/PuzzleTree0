using System;
using UnityEngine;

public class Window : MonoBehaviour
{
    public Action Opened;
    public Action Closed;

    protected bool _isOpen = false;

    public bool IsOpen => _isOpen;

    public virtual void Open()
    {
        UIManager.AddOpenWindow(this);
        gameObject.SetActive(true);
        _isOpen = true;
        Opened?.Invoke();
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
        _isOpen = false;
        Closed?.Invoke();
    }

    public void OpenOrClose()
    {
        if(_isOpen)
            Close();
        else
            Open();
    }
}

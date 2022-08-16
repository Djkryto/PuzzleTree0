using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private MenuWindow _mainWindow;
    private Stack<MenuWindow> _windows;
    private UserInput _input; 

    private void Awake()
    {
        _windows = new Stack<MenuWindow>();
        _windows.Push(_mainWindow);
        _input = new UserInput();
        _input.Player.Escape.performed += context => CloseWindow();
    }

    public void CloseWindow()
    {
        if (_windows.Count > 1)
        {
            var currentWindow = _windows.Pop();
            currentWindow.gameObject.SetActive(false);
            var previousWindow = _windows.Peek();
            previousWindow.gameObject.SetActive(true);
        }
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    public void OpenWindow(MenuWindow window)
    {
        var currentWindow = _windows.Peek();
        currentWindow.gameObject.SetActive(false);
        window.gameObject.SetActive(true);
        _windows.Push(window);
    }
}

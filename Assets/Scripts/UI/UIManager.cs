using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;


    private Stack<Window> _windowsStack;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance.gameObject);
        }
        Instance._windowsStack = new Stack<Window>();
    }

    public static void AddOpenWindow(Window window)
    {
        Instance._windowsStack.Push(window);
    }

    public static void CloseLastWindow()
    {
        if(Instance._windowsStack.Count > 0)
        {
            var window = Instance._windowsStack.Pop();
            window.Close();
        }
    }

    public static void CloseAllWindows()
    {
        while(Instance._windowsStack.Count > 0)
        {
            var window = Instance._windowsStack.Pop();
            window.Close();
        }
    }
}

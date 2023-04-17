using UnityEngine;

public class Window : MonoBehaviour
{
    public virtual void Open()
    {
        UIManager.AddOpenWindow(this);
        gameObject.SetActive(true);
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
    }
}

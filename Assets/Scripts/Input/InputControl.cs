using UnityEngine;

public abstract class InputControl : MonoBehaviour
{
    protected UserInput _input;

    public virtual void Init(UserInput input)
    {
        _input = input;
    }

    public abstract void Enable();
    public abstract void Disable();
}

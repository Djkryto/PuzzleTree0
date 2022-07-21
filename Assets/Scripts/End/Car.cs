using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private Animator _endingAnimator;
    [SerializeField] private List<GameObject> _disabledObjects;
    private UserInput _input;
    private bool _endingUnlock = false;

    private void Start()
    {
        _input = PlayerControl.Input;
    }

    private void Ending()
    {
        if (_endingUnlock)
        {
            _disabledObjects.ForEach(obj => obj.SetActive(false));
            _endingAnimator.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Wheel wheel))
        {
            _endingUnlock = true;
        }

        if(other.gameObject.TryGetComponent(out Player player))
        {
            _input.Player.Use.performed += context => Ending();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Wheel wheel))
        {
            _endingUnlock = false;
        }

        if (other.gameObject.TryGetComponent(out Player player))
        {
            _input.Player.Use.performed -= context => Ending();
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private PlayerControl _playerControl;
    [SerializeField] private Animator _endingAnimator;
    [SerializeField] private List<GameObject> _disabledObjects;
    [SerializeField] private LayerMask _carLayer;
    private Camera _camera;
    private bool _endingUnlock = false;

    private void Awake()
    {
        _camera = Camera.main;
        _playerControl = FindObjectOfType<PlayerControl>();
    }

    public void Ending()
    {
        if (_endingUnlock)
        {
            _disabledObjects.ForEach(obj => obj.SetActive(false));
            _endingAnimator.enabled = true;
            _playerControl.SetControlLockState();
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
            var endingControlState = new CarEndingControlState(player, _carLayer, _camera, _playerControl.HotbarView);
            _playerControl.SetCustomState(endingControlState);
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
            _playerControl.ResetControl();
        }
    }
}
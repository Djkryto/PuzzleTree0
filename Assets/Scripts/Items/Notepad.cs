using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notepad : MonoBehaviour,IReading
{
    [SerializeField] private GameObject _textCanvas;
    [SerializeField] private PlayerControl _playerControl;
    private PlayerInput _playerInput;
    private Camera _camera;
    [SerializeField] private LayerMask _layerMask;
    private bool _isRead;
    [SerializeField] private bool isAddUseContext;
    private void Start()
    {
        if(isAddUseContext)
        {
            _playerInput = PlayerControl.Input;
            _playerInput.Player.Use.performed += context => CheckNotepad();
        }
        _camera = Camera.main;
    }

    public void CheckNotepad()
    {
        Ray rayCamera = new Ray(_camera.transform.position, _camera.transform.forward);

        if (Physics.Raycast(rayCamera, out RaycastHit hit, _layerMask))
        {
            if (hit.collider.TryGetComponent(out Notepad notepad) )
                if(notepad.enabled)
                    notepad.UseNotepad();
        }
    }

    public void UseNotepad()
    {
        _isRead = !_isRead;
        _textCanvas.SetActive(_isRead);
        _playerControl.ControlLock();
    }
   
}

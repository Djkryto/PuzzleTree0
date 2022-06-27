using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody _playerRigidbody;
    [SerializeField] private Transform _aimTarget;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private float _visionDistance;
    [SerializeField] private LayerMask _layerOfVision;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _accelerationTime;
    [SerializeField] private Inventory _inventory;
    private List<InventoryCell> _playerInventory;
    private PlayerMovement _playerMovement;
    private PlayerVision _playerVision;

    public PlayerVision Vision => _playerVision;
    public Inventory Inventory => _inventory;

    private void Awake()
    {
        _playerInventory = new List<InventoryCell>();
        _playerMovement = new PlayerMovement(_playerRigidbody, _walkSpeed, _runSpeed, _accelerationTime);
        _playerVision = new PlayerVision(_aimTarget, _visionDistance, _layerOfVision);
    }

    public void TakeObject()
    {
        var takeableObject = _playerVision.TakeableObject;
        try
        {
            var objectTransform = takeableObject.Take();
            var inventoryCell = _inventory.AddItem(objectTransform, takeableObject.Item);
            _playerInventory.Add(inventoryCell);
        }
        catch(Exception exception)
        {
            Debug.LogException(exception);
        }
    }

    public void LookAt(Ray desiredTargetRay)
    {
        _playerVision.LookAt(desiredTargetRay);
    }

    public RaycastHit ScanObjectInFront(Ray rayCenterCamera)
    {
        return _playerVision.ScanObjectInFront(rayCenterCamera);
    }

    public void Rotate(float targetYEulerAngle)
    {
        _playerMovement.Rotate(transform.localEulerAngles, targetYEulerAngle);
    }

    public void Walk(Vector2 direction)
    {
        _playerMovement.Walk(direction, Time.deltaTime);
        _playerAnimator.SetFloat("Speed", (_playerMovement.CurrentMovementSpeed / _playerMovement.WalkSpeed) * 0.5f);
    }

    public void Run(Vector2 direction)
    {
        _playerMovement.Run(direction, Time.deltaTime);
        _playerAnimator.SetFloat("Speed", _playerMovement.CurrentMovementSpeed / _playerMovement.RunSpeed);
    }

    public void Decceleration(Vector2 direction)
    {
        _playerMovement.Decceleration(direction, Time.deltaTime);
        _playerAnimator.SetFloat("Speed", _playerMovement.CurrentMovementSpeed / _playerMovement.RunSpeed);
    }
}

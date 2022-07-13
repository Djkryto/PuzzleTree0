using System;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _accelerationTime;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Transform _hand;
    private InteractiveItem _currentItemInHand;
    private List<InventoryCell> _playerItems;
    private PlayerMovement _playerMovement;
    private PlayerRotator _playerRotator;
    private PlayerVision _playerVision;

    public PlayerVision Vision => _playerVision;
    public Inventory Inventory => _inventory;
    public InteractiveItem CurrentItemInHand => _currentItemInHand;

    private void Awake()
    {
        _playerItems = new List<InventoryCell>();
        _playerMovement = new PlayerMovement(_playerRigidbody, _walkSpeed, _runSpeed, _accelerationTime);
        _playerRotator = new PlayerRotator(transform, _rotationSpeed);
        _playerVision = new PlayerVision(_aimTarget, _visionDistance, _layerOfVision);
        _inventory.DeletedItem += DropItem;
    }

    private void DropItem(InventoryCell  inventoryItem)
    {
        if(_currentItemInHand == inventoryItem.ItemInWorld)
        {
            _currentItemInHand.transform.SetParent(null);
            _currentItemInHand.Takeable.DropItem();
            _currentItemInHand = null;
        }
        else
        {
            inventoryItem.ItemInWorld.gameObject.SetActive(true);
            inventoryItem.ItemInWorld.Takeable.DropItem();
            inventoryItem.ItemInWorld.transform.SetParent(null);
        }
    }

    public void TakeObject()
    {
        try
        {
            var takeableObject = _playerVision.InteractiveItem.Takeable;
            var itemTransform = takeableObject.TakeItem();
            itemTransform.SetParent(_hand);
            var itemInWorld = itemTransform.GetComponent<InteractiveItem>();
            var inventoryCell = _inventory.AddItem(itemInWorld, takeableObject.ItemData);
            _playerItems.Add(inventoryCell);
        }
        catch(Exception exception)
        {
           // Debug.LogException(exception);
        }
    }

    public void DragItem()
    {
        try
        {
            var interactiveItem = _playerVision.InteractiveItem;
            interactiveItem.Portable.DragItem(_hand);
        }
        catch(Exception exception)
        {
            Debug.LogWarning(exception);
        }
    }

    public void DropPortableItem()
    {
        try
        {
            var interactiveItem = _playerVision.InteractiveItem;
            interactiveItem.Portable.DropItem();
        }
        catch (Exception exception)
        {
            Debug.LogWarning(exception);
        }
    }

    public void SetItemInHand(InventoryCell currentCell)
    {
        try
        {
            _currentItemInHand?.gameObject.SetActive(false);
            var item = _playerItems.FirstOrDefault(cell => cell == currentCell && cell.CellImage != default);
            _currentItemInHand = item.ItemInWorld;
            _currentItemInHand.gameObject.SetActive(true);
            _currentItemInHand.transform.rotation = _hand.rotation;
            _currentItemInHand.transform.position = _hand.position;
        }
        catch (Exception exception)
        {
            _currentItemInHand?.gameObject.SetActive(false);
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
        _playerRotator.Rotate(targetYEulerAngle, Time.deltaTime);
    }

    public void Walk(Vector2 direction)
    {
        _playerMovement.Walk(direction, Time.deltaTime);
        _playerAnimator.SetFloat("Speed", (_playerMovement.CurrentMovementSpeed / _playerMovement.MaxMovementSpeed) * 0.5f);
    }

    public void Run(Vector2 direction)
    {
        _playerMovement.Run(direction, Time.deltaTime);
        _playerAnimator.SetFloat("Speed", _playerMovement.CurrentMovementSpeed / _playerMovement.MaxMovementSpeed);
    }

    public void Decceleration()
    {
        _playerMovement.Decceleration(Time.deltaTime);
        _playerAnimator.SetFloat("Speed", _playerMovement.CurrentMovementSpeed / _playerMovement.RunSpeed);
    }
}
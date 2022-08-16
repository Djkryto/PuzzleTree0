using System;
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
    [SerializeField] private int _inventorySize;
    [SerializeField] private Transform _hand;
    [SerializeField] private AudioSource _takeSound;
    private InteractiveItem _currentItemInHand;
    private PlayerMovement _playerMovement;
    private PlayerRotator _playerRotator;
    private PlayerVision _playerVision;

    public PlayerVision Vision => _playerVision;
    public Inventory Inventory => _inventory;
    public InteractiveItem CurrentItemInHand => _currentItemInHand;

    private void Awake()
    {
        _inventory = new Inventory(_inventorySize);
        _playerMovement = new PlayerMovement(_playerRigidbody, _walkSpeed, _runSpeed, _accelerationTime);
        _playerRotator = new PlayerRotator(transform, _rotationSpeed);
        _playerVision = new PlayerVision(_aimTarget, _visionDistance, _layerOfVision);
    }

    public void DropItem(InteractiveItem itemInWorld)
    {
        if (_currentItemInHand == itemInWorld)
            _currentItemInHand = default;

        itemInWorld.transform.SetParent(null);
        itemInWorld.gameObject.SetActive(true);
        itemInWorld.Takeable.DropItem();
        _inventory.TryRemoveItem(itemInWorld);
        _takeSound.Play();
    }

    public void TakeObject()
    {
        try
        {
            var interactiveItem = _playerVision.InteractiveItem;
            var itemTransform = interactiveItem.Takeable.TakeItem();

            if (!_inventory.TryAddItem(interactiveItem))
                throw new Exception("Failed to add an item!");

            itemTransform.SetParent(_hand);
            itemTransform.rotation = _hand.rotation;
            itemTransform.position = _hand.position;

            if (_currentItemInHand == null)
            {
                _currentItemInHand = interactiveItem;
                _currentItemInHand.gameObject.SetActive(true);
            }

            _takeSound.Play();
        }
        catch(Exception exception)
        {
           Debug.LogWarning(exception);
        }
    }

    public void RotateObject(float scrollValue)
    {
        try
        {
            var interactiveItem = _playerVision.InteractiveItem;
            interactiveItem.Portable.Rotate(scrollValue);
        }
        catch (Exception exception)
        {
            Debug.LogWarning(exception);
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

    public void SetItemInHand(InteractiveItem itemInWorld)
    {
        try
        {
            _currentItemInHand?.gameObject.SetActive(false);
            _currentItemInHand = itemInWorld;
            _currentItemInHand.gameObject.SetActive(true);
            _currentItemInHand.transform.rotation = _hand.rotation;
            _currentItemInHand.transform.position = _hand.position;
        }
        catch (Exception exception)
        {
            _currentItemInHand?.gameObject.SetActive(false);
            Debug.LogWarning(exception);
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
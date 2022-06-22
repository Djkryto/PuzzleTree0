using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody _playerRigidbody;
    [SerializeField] private Transform _aimTarget;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _accelerationTime;
    private PlayerMovement _playerMovement;
    private PlayerVision _playerVision;

    private void Awake()
    {
        _playerMovement = new PlayerMovement(_playerRigidbody, _walkSpeed, _runSpeed, _accelerationTime);
        _playerVision = new PlayerVision(_aimTarget);
    }

    public void LookAt(Ray desiredTargetRay)
    {
        _playerVision.LookAt(desiredTargetRay);
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

using UnityEngine;

public class PlayerMovement
{
    private float _walkSpeed;
    private float _runSpeed;
    private float _accelerationTime;
    private float _currentSpeed;
    private float _currentMaxSpeed;
    private float _currentAcceleration;
    private Vector3 _lastDirection;
    private Rigidbody _playerRigidbody;
    private Transform _playerTransform;

    public float CurrentMovementSpeed => _currentSpeed;
    public float MaxMovementSpeed => _currentSpeed;
    public float WalkSpeed => _walkSpeed;
    public float RunSpeed => _runSpeed;

    public PlayerMovement(Rigidbody playerRigidbody, float walkSpeed, float runSpeed, float accelerationTime)
    {
        _playerRigidbody = playerRigidbody;
        _playerTransform = playerRigidbody.transform;
        _currentSpeed = 0f;
        _walkSpeed = walkSpeed;
        _runSpeed = runSpeed;
        _accelerationTime = accelerationTime;
    }

    public void Walk(Vector2 direction, float deltaTime)
    {
        _currentMaxSpeed = _walkSpeed;
        Move(direction, deltaTime);
    }

    public void Run(Vector2 direction, float deltaTime)
    {
        _currentMaxSpeed = _runSpeed;
        Move(direction, deltaTime);
    }

    private void Move(Vector2 direction, float deltaTime)
    {
        _currentAcceleration = _currentMaxSpeed / _accelerationTime;
        var movementVector = direction.y * _playerTransform.forward + direction.x * _playerTransform.right;
        _lastDirection = movementVector;
        Acceleration(_currentAcceleration, deltaTime);
        SetVelocityVector(movementVector, _currentSpeed);
    }

    public void Decceleration(float deltaTime)
    {
        Acceleration(-_currentAcceleration, deltaTime);
        SetVelocityVector(_lastDirection, _currentSpeed);
    }

    private void Acceleration(float currentAcceleration, float deltaTime)
    {
        _currentSpeed += currentAcceleration * deltaTime;
        _currentSpeed = Mathf.Clamp(_currentSpeed, 0f, _currentMaxSpeed);
    }

    private void SetVelocityVector(Vector3 movementVector, float currentSpeed)
    {
        var xVelocity = movementVector.x * currentSpeed;
        var zVelocity = movementVector.z * currentSpeed;
        var velocityVector = new Vector3(xVelocity, _playerRigidbody.velocity.y, zVelocity);
        _playerRigidbody.velocity = velocityVector;
    }
}

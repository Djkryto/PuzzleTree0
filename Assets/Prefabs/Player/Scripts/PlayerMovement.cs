using UnityEngine;

public class PlayerMovement
{
    private float _walkSpeed;
    private float _runSpeed;
    private float _accelerationTime;
    private float _currentSpeed;
    private float _currentMaxSpeed;
    private Rigidbody _playerRigidbody;
    private Transform _playerTransform;

    public float CurrentMovementSpeed => _currentSpeed;
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
        _currentSpeed += Acceleration(_currentMaxSpeed, deltaTime);
        SetVelocityVector(direction, _currentSpeed);
    }

    public void Run(Vector2 direction, float deltaTime)
    {
        _currentMaxSpeed = _runSpeed;
        _currentSpeed += Acceleration(_currentMaxSpeed, deltaTime);
        SetVelocityVector(direction, _currentSpeed);
    }

    public void Decceleration(Vector2 direction, float deltaTime)
    {
        _currentSpeed -= _currentMaxSpeed / _accelerationTime * deltaTime;
        _currentSpeed = Mathf.Clamp(_currentSpeed, 0f, _currentMaxSpeed);
        var velocityVector = _playerTransform.forward * _currentSpeed;
        velocityVector.y = _playerRigidbody.velocity.y;
        _playerRigidbody.velocity = velocityVector;
    }

    private float Acceleration(float maxSpeed, float deltaTime)
    {
        var speedDifference = maxSpeed - _currentSpeed;
        var acceleration = speedDifference / _accelerationTime;
        return acceleration * deltaTime;
    }

    private void SetVelocityVector(Vector2 direction, float currentSpeed)
    {
        var movementVector = direction.y * _playerTransform.forward + direction.x * _playerTransform.right;
        var xVelocity = movementVector.x * currentSpeed;
        var zVelocity = movementVector.z * currentSpeed;
        var velocityVector = new Vector3(xVelocity, _playerRigidbody.velocity.y, zVelocity);
        _playerRigidbody.velocity = velocityVector;
    }

    public void Rotate(Vector3 sourceEulerAngles, float targetYEulerAngle)
    {
        var parentRotation = sourceEulerAngles;
        parentRotation.y = targetYEulerAngle;
        var newPlayerRotation = Quaternion.Euler(parentRotation);

        _playerRigidbody.transform.rotation = Quaternion.Lerp(_playerRigidbody.transform.rotation, newPlayerRotation, 20f * Time.deltaTime);
    }
}

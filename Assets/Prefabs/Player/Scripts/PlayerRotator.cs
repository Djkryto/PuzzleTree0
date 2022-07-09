using UnityEngine;

public class PlayerRotator
{
    private Transform _characterTransform;
    private float _rotationSpeed;

    public PlayerRotator(Transform characterTransform, float rotationSpeed)
    {
        _characterTransform = characterTransform;
        _rotationSpeed = rotationSpeed;
    }

    public void Rotate(float targetYEulerAngle, float deltaTime)
    {
        var rotationSpeed = _rotationSpeed * deltaTime;
        var parentRotation = _characterTransform.localEulerAngles;
        parentRotation.y = targetYEulerAngle;
        var newPlayerRotation = Quaternion.Euler(parentRotation);
        var newRotation = Quaternion.Lerp(_characterTransform.rotation, newPlayerRotation, rotationSpeed);
        _characterTransform.rotation = newRotation;
    }
}
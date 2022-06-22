using UnityEngine;

public class PlayerVision
{
    private Transform _aimTarget;

    public PlayerVision(Transform aimTarget)
    {
        _aimTarget = aimTarget;
    }

    public void LookAt(Ray desiredTargetRay)
    {
        Vector3 desiredTargetPosition = desiredTargetRay.origin + desiredTargetRay.direction;
        _aimTarget.position = desiredTargetPosition;
    }
}

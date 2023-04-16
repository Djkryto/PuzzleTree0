using UnityEngine;

[System.Serializable]
public struct LaserRay
{
    public float RayAngle;
    public float RayLength;
    public LineRenderer RayRenderer;
    public IRefractable HitObject;

    public void ResetRay()
    {
        HitObject = default;
    }
}

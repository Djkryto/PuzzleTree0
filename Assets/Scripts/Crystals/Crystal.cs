using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Crystal : MonoBehaviour, IRefractable
{
    [SerializeField] private List<LaserRay> _generatedRays;
    private List<IRefractable> _hitObjects;

    private void Awake()
    {
        _hitObjects = new List<IRefractable>();
    }

    public void Refract(Vector3 hitPoint, Vector3 sourceLaserDirection)
    {
        var newLaseDirection = hitPoint - sourceLaserDirection;
        foreach(var laserRay in _generatedRays)
        {
            var startPosition = transform.InverseTransformPoint(hitPoint);
            laserRay.RayRenderer.gameObject.SetActive(true);
            laserRay.RayRenderer.SetPosition(0, startPosition);

            var endPosition = (startPosition - transform.InverseTransformPoint(newLaseDirection)) * laserRay.RayLength;
            endPosition.x = endPosition.x * Mathf.Cos(laserRay.RayAngle * Mathf.Deg2Rad) + endPosition.z * Mathf.Sin(laserRay.RayAngle * Mathf.Deg2Rad);
            endPosition.y = startPosition.y;
            endPosition.z = -endPosition.x * Mathf.Sin(laserRay.RayAngle * Mathf.Deg2Rad) + endPosition.z * Mathf.Cos(laserRay.RayAngle * Mathf.Deg2Rad);

            endPosition = CheckHitOtherCrystal(startPosition, endPosition, laserRay.RayLength);

            laserRay.RayRenderer.SetPosition(1, endPosition);
        }
    }

    private Vector3 CheckHitOtherCrystal(Vector3 startPosition, Vector3 endPosition, float rayDistance)
    {
        var rayDirection = endPosition - startPosition;
        Ray ray = new Ray(transform.position + startPosition, rayDirection);
        var refractedObjects = Physics.RaycastAll(ray, rayDistance);
        if (refractedObjects.Length > 0)
        {
            var hit = refractedObjects.FirstOrDefault(refractedObject => refractedObject.collider.gameObject != this);
            if (hit.collider != default && hit.transform.TryGetComponent(out IRefractable refractedObj))
            {
                _hitObjects.Add(refractedObj);
                endPosition = transform.InverseTransformPoint(hit.point);
                refractedObj.Refract(hit.point, rayDirection.normalized);
            }
        }
        return endPosition;
    }

    public void ClearRefraction()
    {
        foreach (var laserRay in _generatedRays)
        {
            laserRay.RayRenderer.gameObject.SetActive(false);
        }
        foreach (var refractedObject in _hitObjects)
        {
            refractedObject.ClearRefraction();
        }
    }

    private void OnDrawGizmos()
    {
        foreach(var ray in _generatedRays)
        {
            var startPosition = ray.RayRenderer.GetPosition(0);
            var endPosition = ray.RayRenderer.GetPosition(1);
            var rayDirection = endPosition - startPosition;
            Ray newRay = new Ray(transform.position - startPosition, rayDirection.normalized);
            Gizmos.DrawRay(startPosition, rayDirection);
        }
    }
}

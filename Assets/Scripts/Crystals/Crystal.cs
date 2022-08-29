using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Crystal : MonoBehaviour, IRefracted
{
    [SerializeField] private List<RayRefraction> _generatedRays;
    private List<IRefracted> _hitObjects;

    private void Awake()
    {
        _hitObjects = new List<IRefracted>();
    }

    public void Refract(Vector3 hitPoint, Vector3 direction)
    {
        var rayLength = hitPoint - direction;
        foreach(var laserRay in _generatedRays)
        {
            var startPosition = transform.InverseTransformPoint(hitPoint);
            laserRay.Line.gameObject.SetActive(true);
            laserRay.Line.SetPosition(0, startPosition);

            var endPosition = (startPosition - transform.InverseTransformPoint(rayLength)) * laserRay.LineLength;
            endPosition.x = endPosition.x * Mathf.Cos(laserRay.AngleOfRefraction * Mathf.Deg2Rad) + endPosition.z * Mathf.Sin(laserRay.AngleOfRefraction * Mathf.Deg2Rad);
            endPosition.y = startPosition.y;
            endPosition.z = -endPosition.x * Mathf.Sin(laserRay.AngleOfRefraction * Mathf.Deg2Rad) + endPosition.z * Mathf.Cos(laserRay.AngleOfRefraction * Mathf.Deg2Rad);

            endPosition = CheckHitOtherCrystal(startPosition, endPosition, laserRay.LineLength);

            laserRay.Line.SetPosition(1, endPosition);
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
            if (hit.collider != default && hit.transform.TryGetComponent(out IRefracted refractedObj))
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
            laserRay.Line.gameObject.SetActive(false);
        }
        foreach (var refractedObject in _hitObjects)
        {
            refractedObject.ClearRefraction();
        }
    }
}

using System;
using UnityEngine;

public class PlayerVision
{
    public Action Detected;
    public Action Undetected;
    private float _visionDistance = 5f;
    private LayerMask _layerOfVision;
    private Transform _aimTarget;
    private ITakeable _visibleTakeableObject;
    private IInspectable _visibleInspectableObject;
    private IPortable _visiblePortableItem;

    public ITakeable TakeableObject => _visibleTakeableObject;
    public IInspectable InspectableObject => _visibleInspectableObject;
    public IPortable PortableItem => _visiblePortableItem;

    public PlayerVision(Transform aimTarget, float visionDistance, LayerMask layerOfVision)
    {
        _aimTarget = aimTarget;
        _visionDistance = visionDistance;
        _layerOfVision = layerOfVision;
    }

    public RaycastHit ScanObjectInFront(Ray rayCenterCamera)
    {
        var detected = Physics.Raycast(rayCenterCamera, out RaycastHit hitObject, _visionDistance, _layerOfVision);
        if (detected)
        {
            _visibleInspectableObject = CheckComponent<IInspectable>(hitObject);
            _visibleTakeableObject = CheckComponent<ITakeable>(hitObject);
            _visiblePortableItem = CheckComponent<IPortable>(hitObject);
            Detected?.Invoke();
        }
        else
        {
            _visibleInspectableObject = default;
            _visibleTakeableObject = default;
            _visiblePortableItem = default;
            Undetected?.Invoke();
        }
        return hitObject;
    }

    private T CheckComponent<T>(RaycastHit hitObject)
    {
        T componentObject = default;
        var isComponent = hitObject.transform.TryGetComponent(out T component);
        if (isComponent)
        {
            componentObject = component;
        }
        return componentObject;
    }

    public void LookAt(Ray desiredTargetRay)
    {
        Vector3 desiredTargetPosition = desiredTargetRay.origin + desiredTargetRay.direction;
        _aimTarget.position = desiredTargetPosition;
    }
}

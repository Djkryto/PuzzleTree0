using System;
using UnityEngine;

public class PlayerVision
{
    public Action Detected;
    public Action Undetected;
    private float _visionDistance = 5f;
    private LayerMask _layerOfVision;
    private Transform _aimTarget;
    private InteractiveItem _interactiveItem;

    public InteractiveItem InteractiveItem => _interactiveItem;

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
            _interactiveItem = CheckComponent<InteractiveItem>(hitObject);
            Detected?.Invoke();
        }
        else
        {
            _interactiveItem = default;
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

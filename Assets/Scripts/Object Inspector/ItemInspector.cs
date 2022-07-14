using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ItemInspectorControl))]
public class ItemInspector : MonoBehaviour
{
    [SerializeField] private Transform _inspectorRotator;
    [SerializeField] private float _inspectorDetectionDistance = Mathf.Infinity;
    [SerializeField] private LayerMask _inspectableLayer;
    [SerializeField] private float _rotateSpeedPerSecond = 100f;
    [SerializeField] private float _minZoom = 2.0f;
    [SerializeField] private float _maxZoom = 10.0f;
    [SerializeField] private float _zoomMultiplier = 0.2f;
    private List<IInspectable> _inspectableObjectPool = new List<IInspectable>();
    private IInspectable _currentInspectableObject;

    public void CreateObjectInInspector(IInspectable inspectableObject)
    {
        var copyInspectableObject = Instantiate(inspectableObject.ItemTransform, _inspectorRotator.position, Quaternion.identity, _inspectorRotator);
        var isRigidbbody = copyInspectableObject.TryGetComponent(out Rigidbody rigidbody);
        if(isRigidbbody)
        {
            rigidbody.isKinematic = true;
        }
        _currentInspectableObject = copyInspectableObject.GetComponent<IInspectable>();
        _inspectableObjectPool.Add(_currentInspectableObject);
    }

    public void RotateObject(Vector2 mouseVector)
    {
        var rotationvector = new Vector3(mouseVector.y, -mouseVector.x, 0f);
        _inspectorRotator.Rotate(rotationvector * _rotateSpeedPerSecond * Time.deltaTime, Space.World);
    }

    public void ObjectZoom(float scrollValue)
    {
        var newPositionZ = _inspectorRotator.localPosition.z + scrollValue * _zoomMultiplier;
        newPositionZ = Mathf.Clamp(newPositionZ, _minZoom, _maxZoom);
        _inspectorRotator.localPosition = new Vector3(0f, 0f, newPositionZ);
    }

    public void TryOpenInspector()
    {
        try
        {
            transform.gameObject.SetActive(true);
            _inspectorRotator.rotation = Quaternion.identity;
            _currentInspectableObject.ItemTransform.gameObject.SetActive(true);
        }
        catch (Exception exp)
        {
            Debug.LogWarning(exp);
        }
    }

    public void TryCloseInspector()
    {
        try
        {
            _currentInspectableObject.ItemTransform.gameObject.SetActive(false);
            transform.gameObject.SetActive(false);
        }
        catch (Exception exp)
        {
            Debug.LogException(exp);
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInspector : MonoBehaviour
{
    [SerializeField] private Transform _inspectorRotator;
    [SerializeField] private float _inspectorDetectionDistance = Mathf.Infinity;
    [SerializeField] private LayerMask _inspectableLayer;
    private List<IInspectable> _inspectableObjectPool = new List<IInspectable>();
    private IInspectable _currentInspectableObject;

    public void CreateObjectInInspector(IInspectable inspectableObject)
    {
        var copyInspectableObject = Instantiate(inspectableObject.ItemTransform, _inspectorRotator.position, Quaternion.identity, _inspectorRotator);
        _currentInspectableObject = copyInspectableObject.GetComponent<IInspectable>();
        _inspectableObjectPool.Add(_currentInspectableObject);
    }

    public void TryOpenInspector()
    {
        try
        {
            transform.gameObject.SetActive(true);
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
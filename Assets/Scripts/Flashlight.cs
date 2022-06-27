using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour, IInspectable, ITakeable
{
    [SerializeField] ItemSO _itemData;

    public Transform ObjectTransform => transform;

    public ItemSO Item => _itemData;

    public Transform Take()
    {
        gameObject.SetActive(false);
        return transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

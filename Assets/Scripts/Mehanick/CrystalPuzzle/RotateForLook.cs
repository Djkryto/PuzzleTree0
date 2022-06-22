using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateForLook : MonoBehaviour
{
    public Transform look;

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x,look.eulerAngles.y, transform.eulerAngles.z);
    }
}

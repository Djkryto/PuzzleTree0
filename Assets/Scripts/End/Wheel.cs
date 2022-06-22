using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    public CarName carName;
    public Rigidbody rb;
    public Collider collider;
    public GameObject WheelCar;
    public float Timer;
    public IEnumerator ChangePosition()
    {
        yield return new WaitForSeconds(2f);
        collider.enabled = false;
        transform.position = WheelCar.transform.position;
        Destroy(rb);
        transform.rotation = new Quaternion(0, 0, 0, 0);
        carName.isReady = true;
        WheelCar.SetActive(false);
    }
}

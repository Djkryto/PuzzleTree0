using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarName : MonoBehaviour
{
    public Transform Wheel;
    public int ChangeName;
    public bool isReady;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float Dist = Vector3.Distance(transform.position,Wheel.position);

        if(Dist < ChangeName)
        {
            if (isReady)
            {
                gameObject.name = "CarReady";
            }
            else
            {
                gameObject.name = "CarWait";
            }
        }
    }
}

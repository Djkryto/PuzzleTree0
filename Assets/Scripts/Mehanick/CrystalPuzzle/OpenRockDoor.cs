using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenRockDoor : MonoBehaviour
{
    public int Y;
    public bool isActive;
    public Vector3 Target;
    // Start is called before the first frame update
    void Start()
    {
        Target = new Vector3(transform.position.x, transform.position.y - Y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target,Time.deltaTime * 2);
            if(transform.position == Target)
            {
                isActive = false;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{
    public GameObject RayEnd;
    public LineRenderer LighteLine;
    public Transform direction;
    public bool isLighte;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isLighte)
        {
            Ray ray = new Ray(direction.position, direction.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                RayEnd.transform.position = hit.point;
                LighteLine.SetPosition(0, transform.position);
                LighteLine.SetPosition(1, hit.point);
                LighteLine.enabled = true;
            }
        }
        else
        {
            LighteLine.enabled = false;
        }
    }
    public void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "LightBox")
        {
            isLighte = true;
        }
    }
    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "LightBox")
        {
            isLighte = false;
        }
    }
}

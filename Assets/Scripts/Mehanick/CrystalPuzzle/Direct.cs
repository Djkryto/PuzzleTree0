using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direct : MonoBehaviour
{
    public GameObject LaserPoint;
    public LineRenderer lineRenderer;
    public bool isActive;
    public bool isRotate;
    // Start is called before the first frame update
    void Start()
    {
        LaserPoint.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (isRotate)
            {
                LaserPoint.SetActive(true);
                lineRenderer.enabled = true;

                Ray ray = new Ray(transform.position, transform.forward);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    LaserPoint.transform.position = hit.point;
                    lineRenderer.SetPosition(0, transform.position);
                    lineRenderer.SetPosition(1, LaserPoint.transform.position);
                }
            }
        }
        else
        {
            //Vector3.MoveTowards(LaserPoint.transform.position, new Vector3(), 3* Time.deltaTime);
            LaserPoint.SetActive(false);
            lineRenderer.enabled = false;
        }
    }
}

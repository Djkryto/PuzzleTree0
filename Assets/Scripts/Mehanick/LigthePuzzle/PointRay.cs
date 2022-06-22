using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointRay : MonoBehaviour
{
    public GameObject[] points;
    public LineRenderer LighteLine;
    public string NameObject;
    public int Index;
    public int NextPoint;
    public bool isLighte;

    public void Start()
    {
        for(int i = 0; i < points.Length; i++)
        {
            if(points[i] == gameObject)
            {
                Index = i;
                NextPoint = i + 1;
            }
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name == "45" || collision.gameObject.name == "minus90" || collision.gameObject.name == "15" || collision.gameObject.name == "CrystalChange")
        {
            LighteLine.enabled = true;
            RayDirection(collision.gameObject.name);
        }
    }
    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "45" || collision.gameObject.name == "minus90" || collision.gameObject.name == "15" || collision.gameObject.name == "CrystalChange")
        {
            LighteLine.enabled = false;
            for(int i = Index; i < points.Length; i++)
            {
                if(points[i] != null)
                {
                    points[i].transform.position = Vector3.zero;
                    points[i].GetComponent<LineRenderer>().enabled = false;
                }
            }
        }
    }

    public void RayDirection(string value)
    {
        if (value == "CrystalChange")
        {
            if(Index == 4)
            {
                Ray ray = new Ray(transform.position, new Vector3(0.5f, -0.5f, 0));
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (points[NextPoint] != null)
                    {
                        points[NextPoint].transform.position = hit.point;
                    }
                    LighteLine.SetPosition(0, transform.position);
                    LighteLine.SetPosition(1, hit.point);
                }
            }
        }
        if (value == "45")
        {
            Ray ray = new Ray(transform.position, new Vector3(0.5f, -0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (points[NextPoint] != null)
                {
                    points[NextPoint].transform.position = hit.point;
                }
                LighteLine.SetPosition(0, transform.position);
                LighteLine.SetPosition(1, hit.point);
            }
        }
        if (value == "minus90")
        {
            Ray ray = new Ray(transform.position, new Vector3(0, 1, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (points[NextPoint] != null)
                {
                    points[NextPoint].transform.position = hit.point;
                }
                LighteLine.SetPosition(0, transform.position);
                LighteLine.SetPosition(1, hit.point);
            }
        }
        if (value == "15")
        {
            Ray ray = new Ray(transform.position, new Vector3(-1.75f, -0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (points[NextPoint] != null)
                {
                    points[NextPoint].transform.position = hit.point;
                }
                LighteLine.SetPosition(0, transform.position);
                LighteLine.SetPosition(1, hit.point);
            }
        }
    }
}

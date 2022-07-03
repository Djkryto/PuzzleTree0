using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLighte : MonoBehaviour
{
    public GameObject pointLigthe;
    public Transform Laser;
    public LineRenderer Line;
    public Transform Hand;
    public bool isLookCrystal;

    public GameObject Crystal;
    public bool isLaser;

    void Update()
    {
        if (isLaser)
        {
            pointLigthe.SetActive(true);
            Line.enabled = true;
            Ray ray = new Ray(transform.position, transform.right);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Line.SetPosition(0,Laser.position);
                pointLigthe.transform.position = hit.point;
                Line.SetPosition(1,hit.point);
            }
        }
        else
        {
            Line.enabled = false;
            pointLigthe.transform.position = Vector3.zero;
            pointLigthe.SetActive(false);
        }
    }

    public IEnumerator ChangePosition()
    {
        transform.localEulerAngles = new Vector3(0, 0, 103);
        Debug.Log("Work");
        yield return null;
    }

}

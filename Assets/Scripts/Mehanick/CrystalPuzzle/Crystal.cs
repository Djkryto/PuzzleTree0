using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    public CheckAllCrystal allCrystal;
    public Direct direct;
    public Direct directTwo;
    public bool isPoint;
    // Start is called before the first frame update
    void Start()
    {
        allCrystal = GameObject.Find("CrystalAll").GetComponent<CheckAllCrystal>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "PointLightLaser" || collision.gameObject.name == "LaserPoint")
        {
            allCrystal.CheckAllCrystals();
            isPoint = true; 
            direct.isActive = true;
            if (directTwo != null)
            {
                directTwo.isActive = true;
            }
        }
    }
  
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "PointLightLaser" || collision.gameObject.name == "LaserPoint")
        {
            isPoint = false;
            direct.isActive = false;
            if (directTwo != null)
            {
                directTwo.isActive = false;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAllCrystal : MonoBehaviour
{
    public GameObject[] AllCrystal;
    public Crystal[] Crystals;
    public bool isCheck;
    public OpenRockDoor Door;

    // Start is called before the first frame update
    void Start()
    {
        AllCrystal = GameObject.FindGameObjectsWithTag("Crystal");
        for (int i = 0; i < Crystals.Length; i++)
        {
            Crystals[i] = AllCrystal[i].GetComponent<Crystal>();
        }
    }

    public void CheckAllCrystals()
    {
        if (Crystals[0].isPoint && Crystals[1].isPoint && Crystals[2].isPoint && Crystals[3].isPoint && Crystals[4].isPoint
            && Crystals[5].isPoint && Crystals[6].isPoint)
            isCheck = true;

        if (isCheck)
        {
            Door.isActive = true;

            for (int i = 0; i < AllCrystal.Length; i++)
            {
                Crystals[i].transform.GetChild(0).GetComponent<LookPlayer>().enabled = false;
            }
        }
    }
}

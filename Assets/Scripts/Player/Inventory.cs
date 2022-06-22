using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject light;
    public GameObject cameraLook;

    public GameObject[] Items;

    public Camera cameraMain;

    public AudioSource AudioFlashlighte;
    public AudioSource AudioLaser;

    public LaserLighte laserLighte;

    public GameObject flashlighte;
    public GameObject laser;
    public GameObject Hand;

    public bool isTablet;
    public bool EnableItems;

    public GameObject HandItems;
    public AudioSource HandItemsAudio;
    // Update is called once per frame
    void Update()
    {
        

        ChangeItem();
        CheckItems();
        if (isTablet)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                cameraLook.active = !cameraLook.active;
                cameraMain.enabled = !cameraMain.enabled;
            }
        }
    }

    public void AddItems(GameObject item)
    {
        for(int i = 0; i < Items.Length; i++)
        {
            if(Items[i] == null)
            {
                Items[i] = item;
                Items[i].transform.position = Hand.transform.position;
                Items[i].transform.rotation = Hand.transform.rotation;
                Items[i].transform.SetParent(Hand.transform);
                if(HandItems != null)
                {
                    Items[i].gameObject.SetActive(false);
                }
                else
                {
                    Items[i].gameObject.SetActive(true);
                    HandItems = Items[i];
                    if (HandItems.GetComponent<AudioSource>() != null)
                    {
                        HandItemsAudio = HandItems.GetComponent<AudioSource>();
                    }
                }

                break;
            }
        }
    }

    public void ChangeItem()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (Items[0] != null)
            {
                for (int i = 0; i < Items.Length; i++)
                {
                    if (Items[i] != null)
                        Items[i].SetActive(false);
                }
                Items[0].SetActive(true);
                HandItems = Items[0];
                if (HandItems.GetComponent<AudioSource>() != null)
                {
                    HandItemsAudio = HandItems.GetComponent<AudioSource>();
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(Items[1] != null)
            {
                for (int i = 0; i < Items.Length; i++)
                {
                    if (Items[i] != null)
                        Items[i].SetActive(false);
                }
                Items[1].SetActive(true);
                HandItems = Items[1];
                if (HandItems.GetComponent<AudioSource>() != null)
                {
                    HandItemsAudio = HandItems.GetComponent<AudioSource>();
                }
            }
        }
       
    }

    public void CheckItems()
    {
        if(HandItems != null)
        {
            if (!EnableItems)
            {
                if (HandItems.name == "Laser")
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        HandItemsAudio.Play();
                        laserLighte.isLaser = !laserLighte.isLaser;
                    }
                }
                else if (HandItems.name == "Flashlight")
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        HandItemsAudio.Play();
                        light.active = !light.active;
                    }
                }
            }
        }
    }
}
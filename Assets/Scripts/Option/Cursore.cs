using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursore : MonoBehaviour
{
   
    public void CursorCenter(bool state)
    {
        if (state)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class ButtonCasket : MonoBehaviour
{
    public int Nomber;
    public Action<int> OnPress;
    [SerializeField] private GameObject _pressIndicator;
    public void Press()
    {
        OnPress.Invoke(Nomber);
        _pressIndicator.SetActive(false);
    }

    public void ResetButton()
    {
        _pressIndicator.SetActive(true);
    }
    //void OnMouseDown()
    //{
    //    if (Nomber == 1)
    //    {
    //        casket.One = !casket.One;
    //        casket.Two = !casket.Two;
    //    }

    //    if (Nomber == 2)
    //    {
    //        casket.One = !casket.One;
    //        casket.Three = !casket.Three;
    //        if(casket.One && casket.Three)
    //        {
    //            casket.Two = false;
    //        }
    //        else
    //        {
    //            casket.Two = true;
    //        }
    //    }

    //    if (Nomber == 3)
    //    {
    //        casket.Two = !casket.Two;
    //        casket.Three = !casket.Three;
    //    }
    //    casket.CheckCasket();
    //}
}

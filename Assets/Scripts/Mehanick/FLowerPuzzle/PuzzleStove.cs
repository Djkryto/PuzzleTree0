using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleStove : MonoBehaviour
{
    [SerializeField] private Stove[] AllStoves;

    [SerializeField] private Animator Britch;

    private bool checkEnd;
    private bool checkIsActive;
    private bool end;
    private float Timer;
    public void Check(bool value)
    {
        checkEnd = value;
        if (value)
        {
            for (int i = 0; i < AllStoves.Length; i++)
            {
                if (AllStoves[i].isActive)
                {
                    checkIsActive = true;
                }
                else
                {
                    checkEnd = false;
                    checkIsActive = false;
                }
            }

            if (checkIsActive)
            {
               
                end = true;
                checkEnd = false;
                
            }
            else
            {
                checkEnd = false;
                checkIsActive = false;
                Timer = 0;
            }
        }
        else
        {
            Timer = 0;
        }

        if (end)
        {
            Britch.enabled = true;
        }

    }
    
}

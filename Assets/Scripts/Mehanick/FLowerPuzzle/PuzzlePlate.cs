using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePlate : MonoBehaviour
{
    public Plate[] All;
    public Plate[] PlateEven;
    public Plate[] PlateNoEven;
    public Plate Special;
    public Plate SpecialChange;

    public Animator Britch;

    private bool unlockEven;
    private bool unlockNoEven;

    public bool checkEnd;
    public bool checkIsActive;
    public bool end;
    private float Timer;
    public void Update()
    {
        if (checkEnd)
        {
            for(int i = 0;i < All.Length; i++)
            {
                if(All[i].isActive)
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
                Timer += Time.deltaTime;
                if (Timer > 2)
                {
                    end = true;
                    checkEnd = false;
                }
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
    public void LockEven()
    {
        for (int i = 0; i < PlateEven.Length; i++)
        {
            PlateEven[i].isActive = true;
        }
        checkEnd = true;
    }

    public void LockNoEven()
    {
        for (int i = 0; i < PlateNoEven.Length; i++)
        {
            PlateNoEven[i].isActive = true;
        }
        checkEnd = true;
    }

    public void LockSpecial()
    {
        for(int i = 0;i < All.Length; i++)
        {
            if(All[i] != gameObject.GetComponent<Plate>())
            {
                if(!All[i].isRock && !All[i].isPlayer)
                {
                    All[i].isActive = false;
                    Special.isActive = true;
                    SpecialChange = All[i];
                    break;
                }
            }
        }
        checkEnd = true;
    }

    public void UnlockEven()
    {
        unlockEven = true;
        for (int i = 0; i < PlateEven.Length; i++)
        {
            if(PlateEven[i].isPlayer || PlateEven[i].isRock)
            {
                unlockEven = false;
            }
        }
        if (unlockEven)
        {
            for (int i = 0; i < PlateEven.Length; i++)
            {
                PlateEven[i].isActive = false;
            }
        }
    }

    public void UnlockNoEven()
    {
        unlockNoEven = true;
        for (int i = 0; i < PlateNoEven.Length; i++)
        {
            if (PlateNoEven[i].isPlayer || PlateNoEven[i].isRock)
            {
                unlockNoEven = false;
            }
        }
        if (unlockNoEven)
        {
            for (int i = 0; i < PlateNoEven.Length; i++)
            {
                PlateNoEven[i].isActive = false;
            }
        }
    }
    public void UnlockSpecial()
    {
        if(!SpecialChange.isRock && !SpecialChange.isPlayer)
        {
            SpecialChange.isActive = true;
            Special.isActive = false;
        }
    }
}
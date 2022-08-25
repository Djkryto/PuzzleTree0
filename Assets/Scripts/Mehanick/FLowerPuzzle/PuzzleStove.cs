using UnityEngine;

public class PuzzleStove : MonoBehaviour
{
    [SerializeField] private Stove[] AllStoves;

    [SerializeField] private Animator Britch;

    private bool checkEnd;
    private bool checkIsActive;
    private bool end;
    [SerializeField]private bool _startValue;
    public void Check(bool value)
    {
        checkEnd = value;
        if (checkEnd)
        {
            for (int i = 0; i < AllStoves.Length; i++)
            {
                if (AllStoves[i].isActive)
                {
                    _startValue = true;
                }
                else
                {
                    _startValue = false;
                    checkEnd = false;
                }
                if (!_startValue)
                    break;
            }

            if (_startValue)
            {
               
                end = true;
                checkEnd = false;
                
            }
            else
            {
                checkEnd = false;
                checkIsActive = false;
            }
        }

        if (end)
        {
            Britch.enabled = true;
        }

    }
    
}

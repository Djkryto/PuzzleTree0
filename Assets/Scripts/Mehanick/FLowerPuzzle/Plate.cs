using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    public GameObject[] Plates;
    public GameObject[] Changes;
    public GameObject Change;
    public PuzzlePlate puzzlePlate;
    public Vector3 TargetPosition;
    private int count;
    private float timerState;//Таймер проверки ухода объекта с блина.
    public bool CloseTimer;
    public bool isPlayer;
    public bool isRock;
    public bool isActive;
    public bool isLock;

    // Start is called before the first frame update
    void Start()
    {
        TargetPosition = new Vector3(transform.position.x, 4.98f, transform.position.z);
        Plates = GameObject.FindGameObjectsWithTag("Plate");
        if (gameObject.name == "PlitaTree" || gameObject.name == "PlitaSeven" || gameObject.name == "PlitaOne")
        {
            for (int i = 0; i < Plates.Length; i++)
            {
                if (Plates[i].name == "PlitaTree" || Plates[i].name == "PlitaSeven" || Plates[i].name == "PlitaOne")
                {
                    Changes[count] = Plates[i];
                    count++;
                }
            }
        }

        if (gameObject.name == "PlitaTwo" || gameObject.name == "PlitaSix" || gameObject.name == "PlitaFour")
        {
            for (int i = 0; i < Plates.Length; i++)
            {
                if (Plates[i].name == "PlitaTwo" || Plates[i].name == "PlitaSix" || Plates[i].name == "PlitaFour")
                {
                    Changes[count] = Plates[i];
                    count++;
                }
            }
        }

        if(Changes[0] == gameObject)
        {
            GameObject changeIntext = Changes[2];
            Changes[0] = changeIntext;
            Changes[2] = gameObject;
        }
        if(Changes[1] == gameObject)
        {
            GameObject changeIntext = Changes[2];
            Changes[0] = changeIntext;
            Changes[2] = gameObject;
        }
    }

    public void Update()
    {
        if(isActive)
        {
            timerState = 0.5f;
            CloseTimer = false;
            TargetPosition = new Vector3(transform.position.x, 4.875f, transform.position.z);
            if (TargetPosition.y < transform.position.y)
            {
                transform.position -= transform.up * Time.deltaTime;
            }
        }
        else
        {
            if (!CloseTimer)
            {
                timerState -= Time.deltaTime;
            }
            
            if(timerState < 0)
            {
                TargetPosition = new Vector3(transform.position.x, 4.98f, transform.position.z);
                if (TargetPosition.y > transform.localPosition.y)
                {
                    transform.position += transform.up * Time.deltaTime;
                    CloseTimer = true;
                }

            }
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "RockBig")
        {
            isRock = true;
            if (gameObject.name == "PlitaTwo" || gameObject.name == "PlitaSix" || gameObject.name == "PlitaFour")
            {
                puzzlePlate.LockEven();
            }
            if (gameObject.name == "PlitaTree" || gameObject.name == "PlitaSeven" || gameObject.name == "PlitaOne")
            {
                puzzlePlate.LockNoEven();
            }
            if(gameObject.name == "PlitaFive")
            {
                puzzlePlate.LockSpecial();
            }
        }
        if(collision.gameObject.tag == "Player")
        {
            isPlayer = true;
            if (gameObject.name == "PlitaTwo" || gameObject.name == "PlitaSix" || gameObject.name == "PlitaFour")
            {
                puzzlePlate.LockEven();
            }
            isPlayer = true;
            if (gameObject.name == "PlitaTree" || gameObject.name == "PlitaSeven" || gameObject.name == "PlitaOne")
            {
                puzzlePlate.LockNoEven();
            }
            if (gameObject.name == "PlitaFive")
            {
                puzzlePlate.LockSpecial();
            }
        }
        //if (!isLock)
        //{
        //    if (collision.gameObject.tag == "Rock")
        //    {
        //        isRock = true;
        //        isActive = true;
        //        isPlayer = false;

            //        if (gameObject.name != "PlitaFive")
            //        {
            //            for (int i = 0; i < Changes.Length; i++)
            //            {
            //                if (!Changes[i].GetComponent<Plate>().isRock && !Changes[i].GetComponent<Plate>().isPlayer)
            //                {
            //                    Changes[i].GetComponent<Plate>().isActive = true;
            //                    Changes[i].GetComponent<Plate>().isLock = true;
            //                }
            //            }
            //        }
            //        if (gameObject.name == "PlitaFive")
            //        {
            //            for (int i = 0; i < Plates.Length; i++)
            //            {
            //                Plate plate = Plates[i].GetComponent<Plate>();
            //                if (plate.isActive && !plate.isRock && !plate.isPlayer)
            //                {
            //                    Change = Plates[i];
            //                    break;
            //                }
            //            }
            //            Change.GetComponent<Plate>().isActive = false;
            //        }
            //        isLock = true;
            //    }
            //    ////////////////////////////////////////
            //    if (collision.gameObject.tag == "Player")
            //    {
            //        isRock = false;
            //        isActive = true;
            //        isPlayer = true;
            //        if (gameObject.name != "PlitaFive")
            //        {
            //            for (int i = 0; i < Changes.Length; i++)
            //            {
            //                if (!Changes[i].GetComponent<Plate>().isRock && !Changes[i].GetComponent<Plate>().isPlayer)
            //                {
            //                    Changes[i].GetComponent<Plate>().isActive = true;
            //                    Changes[i].GetComponent<Plate>().isLock = true;
            //                }
            //            }
            //        }
            //        if (gameObject.name == "PlitaFive")
            //        {
            //            for (int i = 0; i < Plates.Length; i++)
            //            {
            //                Plate plate = Plates[i].GetComponent<Plate>();
            //                if (plate.isActive && !plate.isRock && !plate.isPlayer)
            //                {
            //                    Change = Plates[i];
            //                    plate.isActive = false;
            //                    plate.isLock = true;
            //                }
            //            }
            //            Change.GetComponent<Plate>().isActive = false;

            //        }
            //    }
            //}
    }
    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "RockBig")
        {
            isRock = false;
            if (gameObject.name == "PlitaTwo" || gameObject.name == "PlitaSix" || gameObject.name == "PlitaFour")
            {
                puzzlePlate.UnlockEven();
            }
            if (gameObject.name == "PlitaTree" || gameObject.name == "PlitaSeven" || gameObject.name == "PlitaOne")
            {
                puzzlePlate.UnlockNoEven();
            }
            if (gameObject.name == "PlitaFive")
            {
                puzzlePlate.UnlockSpecial();
            }
        }
        if (collision.gameObject.tag == "Player")
        {
            isPlayer = false;
            if (gameObject.name == "PlitaTwo" || gameObject.name == "PlitaSix" || gameObject.name == "PlitaFour")
            {
                puzzlePlate.UnlockEven();
            }
            if (gameObject.name == "PlitaTree" || gameObject.name == "PlitaSeven" || gameObject.name == "PlitaOne")
            {
                puzzlePlate.UnlockNoEven();
            }
            if (gameObject.name == "PlitaFive")
            {
                puzzlePlate.UnlockSpecial();
            }
        }
        //if (isLock)
        //{
        //    if (collision.gameObject.tag == "Rock")
        //    {
        //        isActive = false;
        //        isRock = false;
        //        if (gameObject.name != "PlitaFive")
        //        {
        //            for (int i = 0; i < Changes.Length; i++)
        //            {
        //                if (!Changes[i].GetComponent<Plate>().isRock && !Changes[i].GetComponent<Plate>().isPlayer)
        //                {
        //                    Debug.Log(i);
        //                    Changes[i].GetComponent<Plate>().isActive = false;
        //                    Changes[i].GetComponent<Plate>().isLock = false;
        //                }
        //            }
        //        }

        //    }
        //    /////////////////////////////////////////
        //    if (collision.gameObject.tag == "Player")
        //    {
        //            isActive = false;
        //            isPlayer = false;
        //            if (gameObject.name != "PlitaFive")
        //            {
        //                for (int i = 0; i < Changes.Length; i++)
        //                {
        //                    if (!Changes[i].GetComponent<Plate>().isRock && !Changes[i].GetComponent<Plate>().isPlayer)
        //                    {
        //                        Changes[i].GetComponent<Plate>().isActive = false;
        //                        Changes[i].GetComponent<Plate>().isLock = false;
        //                    }
        //                }
        //            }
        //            if (gameObject.name == "PlitaFive")
        //            {
        //                Change.GetComponent<Plate>().isActive = false;
        //            Change.GetComponent<Plate>().isLock = false;
        //        }

        //    }
        //}
    }
}

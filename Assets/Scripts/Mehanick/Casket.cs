using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casket : MonoBehaviour
{
    public CameraRotate cameraRotate;
    public Inventory inventory;
    public Animator animator;
    public Move move;
    public Cursore cursore;
    public GameObject OneLine;
    public GameObject TwoLine;
    public GameObject TreeLine;

    public bool One;
    public bool Two;
    public bool Three;
    // Update is called once per frame
    void Update()
    {
        if (One && Two && Three)
        {
            animator.enabled = true;
            gameObject.tag = "List";
            move.isMove = true;
            cursore.CursorCenter(true);
            cameraRotate.isRotate = true;
            inventory.EnableItems = false;
            Destroy(GetComponent<Casket>());
        }

        OneLine.SetActive(One);
        TwoLine.SetActive(Two);
        TreeLine.SetActive(Three);
    }
}

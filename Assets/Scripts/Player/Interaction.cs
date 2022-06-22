using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public Inventory inventory;

    public Animator blackout;
    public Animator FastEnd;

    public Cursore cursore;

    public CameraRotate CameraRotate;

    public Move move;

    public bool BringBitObject;
    public bool BringObject;

    public Transform PointBringBigObject;
    private Rigidbody RbBigObject;

    public GameObject BigObject;
    public GameObject BringObjectLite;
    public GameObject ListTwo;
    public GameObject ListOne;
    public GameObject ListTree;
    public GameObject ButtonActive;
    public GameObject Text;
    public Text UIText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() 
    {
        if (BringObject)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                RbBigObject.useGravity = true;
                RbBigObject.constraints = RigidbodyConstraints.None;
                BringObjectLite.layer = 0;
                BringObject = false;
            }
            else
            {
                BringObjectLite.transform.eulerAngles = new Vector3(BringObjectLite.transform.eulerAngles.x, BringObjectLite.transform.eulerAngles.y + (Input.mouseScrollDelta.y * 7), BringObjectLite.transform.eulerAngles.z);
                BringObjectLite.transform.position = PointBringBigObject.position;
            }
        }
        if (BringBitObject)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                RbBigObject.useGravity = true;
                RbBigObject.constraints = RigidbodyConstraints.None;
                BigObject.layer = 0;
                move.SetDefaultSpeed();
                BigObject = null;
                BringBitObject = false;
            }
            else
            {
                BigObject.transform.position = PointBringBigObject.position;
            }
        }

        Ray ray = new Ray(transform.position,transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit,3))
        {
            if (hit.collider.tag == "Flashlight" )
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    inventory.AddItems(hit.collider.gameObject);
                    hit.collider.enabled = false;
                }
                ButtonActive.SetActive(true);
                Text.SetActive(true);
                UIText.text = "Взять";
            }
            else if (hit.collider.name == "CarReady")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    move.isMove = false;
                    CameraRotate.isRotate = false;
                    FastEnd.enabled = true;
                }
                ButtonActive.SetActive(true);
                Text.SetActive(true);
                UIText.text = "Уехать";
            }
            else if(hit.collider.name == "CarWait")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    blackout.enabled = true;
                    StartCoroutine(hit.collider.GetComponent<CarName>().Wheel.gameObject.GetComponent<Wheel>().ChangePosition());
                }
                ButtonActive.SetActive(true);
                Text.SetActive(true);
                UIText.text = "Заменить";
            }
            else if (hit.collider.tag == "Laser")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    inventory.AddItems(hit.collider.gameObject);
                    hit.collider.enabled = false;
                }
                ButtonActive.SetActive(true);
                Text.SetActive(true);
                UIText.text = "Взять";
            }
            else if(hit.collider.tag == "List")
            {

                ButtonActive.SetActive(!ListOne.active);
                Text.SetActive(!ListOne.active);
                UIText.text = "Читать";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ListActive(hit.collider.GetComponent<ListNumber>().Number);
                    CameraRotate.isRotate = !CameraRotate.isRotate;
                    inventory.EnableItems = !inventory.EnableItems;
                    move.isMove = !move.isMove;
                }
               
            }
            else if (hit.collider.tag == "RockBig")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    BigObject = hit.collider.gameObject;
                    RbBigObject = BigObject.GetComponent<Rigidbody>();
                    RbBigObject.useGravity = false;
                    RbBigObject.constraints = RigidbodyConstraints.FreezeAll;
                    BigObject.layer = 2;
                    move.SetSpeed(2);
                    BringBitObject = true;
                }
            }
            else if (hit.collider.tag == "Rock")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    BringObjectLite = hit.collider.gameObject;
                    RbBigObject = BringObjectLite.GetComponent<Rigidbody>();
                    RbBigObject.useGravity = false;
                    RbBigObject.constraints = RigidbodyConstraints.FreezeAll;
                    BringObjectLite.layer = 2;
                    BringObject = true;
                }
            }
            else if (hit.collider.name == "Wheel")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    BringObjectLite = hit.collider.gameObject;
                    RbBigObject = BringObjectLite.GetComponent<Rigidbody>();
                    RbBigObject.useGravity = false;
                    RbBigObject.constraints = RigidbodyConstraints.FreezeAll;
                    BringObjectLite.layer = 2;
                    BringObject = true;
                }
            }
            else if (hit.collider.name == "Crystal")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    BringObjectLite = hit.collider.gameObject;
                    RbBigObject = BringObjectLite.GetComponent<Rigidbody>();
                    RbBigObject.useGravity = false;
                    RbBigObject.constraints = RigidbodyConstraints.FreezeAll;
                    BringObjectLite.layer = 2;
                    BringObject = true;
                }
            }
            else if (hit.collider.name == "Tablet")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.gameObject.SetActive(false);
                    inventory.isTablet = true;
                }

                ButtonActive.SetActive(CameraRotate.isRotate);
                Text.SetActive(CameraRotate.isRotate);
                UIText.text = "Взять";
            }
            else if(hit.collider.tag == "Сasket")
            {

                if (Input.GetKeyDown(KeyCode.E))
                {
                    CameraRotate.isRotate = !CameraRotate.isRotate;
                    cursore.CursorCenter(CameraRotate.isRotate);
                    inventory.EnableItems = true;
                    move.isMove = !move.isMove;
                }

                ButtonActive.SetActive(CameraRotate.isRotate);
                Text.SetActive(CameraRotate.isRotate);
                UIText.text = "Осмотреть";
            }
            else if (hit.collider.name == "Grandfather")
            {

                if (Input.GetKeyDown(KeyCode.E))
                {
                    CameraRotate.isRotate = !CameraRotate.isRotate;
                    cursore.CursorCenter(false);
                    inventory.EnableItems = false;
                    move.isMove = !move.isMove;
                }

                ButtonActive.SetActive(CameraRotate.isRotate);
                Text.SetActive(CameraRotate.isRotate);
                UIText.text = "Поговорить";
            }
            else
            {
                ButtonActive.SetActive(false);
                Text.SetActive(false);
            }
            
        }
    }

    public void ListActive(int number)
    {
       if(number == 1)
       {
            ListOne.active = !ListOne.active;
       }
       if(number == 2)
       {
            ListTwo.active = !ListTwo.active;
       }
       if (number == 3)
       {
            ListTree.active = !ListTree.active;
       }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject Player;
    public Camera CameraMenu;
    public Cursore cursore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        CameraMenu.enabled = false;
        Player.SetActive(true);
        cursore.CursorCenter(true);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casket : MonoBehaviour
{
    [SerializeField] private PlayerControl _playerControl;
    private PlayerInput _playerInput;
    [SerializeField] private List<ButtonCasket> _buttons;
    [SerializeField] private List<int> _targetCombination;
    [SerializeField] private List<int> _currentCombination;
    private Camera _camera;
    public Animator animator;
    public Cursore cursore;
    public GameObject OneLine;
    public GameObject TwoLine;
    public GameObject TreeLine; 

   [SerializeField] private LayerMask layer;

    public bool One;
    public bool Two;
    public bool Three;

    public bool onStay;
    private void Awake()
    {
        _playerInput  = PlayerControl.Input;
        _playerInput.Player.Escape.performed += contex => Cancle();
        _camera = Camera.main;
        _playerInput.Player.LMB.performed += context => ClickButton();
        _currentCombination = new List<int>();

        foreach (var button in _buttons)
        {
            button.OnPress += CheckCasket;
        }
    }
    // Update is called once per frame
    public void CheckCasket(int buttonNomber)
    {
        _currentCombination.Add(buttonNomber);
        Debug.Log(_targetCombination == _currentCombination);
        if (_targetCombination == _currentCombination)
        {
            animator.enabled = true;
            gameObject.tag = "List";
            _playerControl.ControlLock();
            Destroy(GetComponent<Casket>());
        }
        else if (_currentCombination.Count == _targetCombination.Count)
        {
            _currentCombination.Clear(); 
            _buttons.ForEach(button => button.ResetButton());
        }

        //if (One && Two && Three)
        //{
        //    animator.enabled = true;
        //    gameObject.tag = "List";
        //    _playerControl.ControlLock();
        //    Destroy(GetComponent<Casket>());
        //}

        //OneLine.SetActive(One);
        //TwoLine.SetActive(Two);
        //TreeLine.SetActive(Three);

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerControl playerControl))
        {
            _playerInput.Player.Use.performed += context => _playerControl.ControlLock();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerControl playerControl))
        {
            _playerInput.Player.Use.performed -= context => _playerControl.ControlLock();
        }
    }

    public void Cancle()
    {
        _playerControl.ControlLock();
    }

    private void ClickButton()
    {
        var mousePosition = _playerInput.Player.MousePosition.ReadValue<Vector2>();
        var mouseRay = _camera.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(mouseRay, out hit,layer))
        {
           if(hit.collider.TryGetComponent(out ButtonCasket button))
           {
                button.Press();
           }
        }
    }
}

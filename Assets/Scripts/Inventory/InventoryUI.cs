using UnityEngine;
using UnityEngine.Events;

public class InventoryUI : MonoBehaviour
{
    public UnityEvent InventoryShowing;

    [SerializeField] private Inventory _inventory;
    [SerializeField] private HotBar _hotbar;
    private UserInput _userInput;

    private bool _isOpen = false;

    public bool IsOpen => _isOpen;

    private void Awake()
    {
        
    }

    private void Start()
    {
        _inventory.gameObject.SetActive(false);
        _hotbar.gameObject.SetActive(false);
    }

    public void InventoryActive()
    {
        _isOpen = !_isOpen;
        _inventory.gameObject.SetActive(_isOpen);
        Cursor.lockState = (_isOpen) ? CursorLockMode.None : CursorLockMode.Locked;
        _hotbar.gameObject.SetActive(!_isOpen);
        InventoryShowing.Invoke();
    }
}

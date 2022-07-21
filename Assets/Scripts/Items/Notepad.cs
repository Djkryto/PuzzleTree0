using UnityEngine;

public class Notepad : MonoBehaviour,IReading
{
    [SerializeField] private GameObject _textCanvas;
    [SerializeField] private PlayerControl _playerControl;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private bool _isAddUseContext;
    private Camera _camera;
    private UserInput _userInput;
    private bool _isRead;

    private void Awake()
    {
        _userInput = new UserInput();
        if (_playerControl == null)
            _playerControl = FindObjectOfType<PlayerControl>();
    }

    private void OnEnable()
    {
        _userInput.Enable();
    }

    private void Start()
    {
        if(_isAddUseContext)
            _userInput.Player.Use.performed += context => CheckNotepad();
        _camera = Camera.main;
    }

    public void CheckNotepad()
    {
        Ray rayCamera = new Ray(_camera.transform.position, _camera.transform.forward);

        if (Physics.Raycast(rayCamera, out RaycastHit hit, 10))
        {
            if (hit.collider.TryGetComponent(out Notepad notepad) )
                if(notepad.enabled)
                    notepad.UseNotepad();
        }
    }

    public void UseNotepad()
    {
        _isRead = !_isRead;
        _textCanvas.SetActive(_isRead);
        _playerControl.ControlLock();
    }

    private void OnDisable()
    {
        _userInput.Disable();
    }
}

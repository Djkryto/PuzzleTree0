using UnityEngine;

public class Notepad : MonoBehaviour, IReadable
{
    [SerializeField] private GameObject _textCanvas;
    [SerializeField] private PlayerControl _playerControl;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private bool _isAddUseContext;
    [SerializeField] private AudioSource _soundList;
    private Camera _camera;
    private UserInput _userInput;
    private bool _isRead;

    private void Awake()
    {
        _userInput = new UserInput();
        _soundList = GameObject.Find("SoundList").GetComponent<AudioSource>();
        if (_playerControl == null)
            _playerControl = FindObjectOfType<PlayerControl>();

        if (_isAddUseContext)
        {
            GameObject[] notes = GameObject.FindGameObjectsWithTag("List");
            foreach( var note in notes)
            {
                _userInput.Player.Use.performed += context => note.GetComponent<Notepad>().CheckNotepad();
            }
        }
    }

    private void OnEnable()
    {
        _userInput.Enable();
    }

    private void Start()
    {
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
        _playerControl.SetControlLockState();
        _playerControl.ControlState.OnExitState += CloseNotepad;
        _soundList.Play();
    }

    private void CloseNotepad()
    {
        _isRead = false;
        _textCanvas.SetActive(_isRead);
        _soundList.Play();
    }

    private void OnDisable()
    {
        _userInput.Disable();
    }

    public void Read()
    {
        throw new System.NotImplementedException();
    }
}

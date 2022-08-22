using Cinemachine;
using System.Collections;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private InventoryView _inventoryView;
    [SerializeField] private ObjectInspector _objectInspector;
    [SerializeField] private HotbarView _hotbar;
    [SerializeField] private CinemachineInputProvider _cinemachineInputProvider;
    private ControlState _controlState;
    private IEnumerator _decelerationEnumerator;

    public ControlState ControlState => _controlState;
    public HotbarView HotbarView => _hotbar;

    private void Start()
    {
        var standardControl = new StandardControlState(_player, _playerCamera, _hotbar);
        standardControl.OnInventoryOpen += SetInventoryControlState;
        _controlState = standardControl;
        _controlState.OnExitState += SetControlLockState;
        _player.Vision.Detected += SetItemControlState;
        _player.Vision.Undetected += ResetControl;
        _controlState.EnableControl();
        _decelerationEnumerator = PlayerStoping();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void SetCustomState(ControlState controlState)
    {
        if(_controlState != controlState)
        {
            _controlState.DisableControl();
            _controlState = controlState;
            _controlState.EnableControl();
            _controlState.OnExitState += ResetControl;
            StartCoroutine(_decelerationEnumerator);
        }
    }

    public void SetControlLockState()
    {
        _cinemachineInputProvider.enabled = false;
        SetCustomState(new ControlLockState(_player));
    }

    public void SetItemControlState()
    {
        if(!(_controlState is ItemControlState))
        {
            _controlState.DisableControl();
            var itemControlState = new ItemControlState(_player, _playerCamera, _hotbar);
            itemControlState.InspectEvent += SetObjectInspectorState;
            _controlState = itemControlState;
            _controlState.EnableControl();
        }
    }

    public void SetCasketControlState(LayerMask controlObjectLayer)
    {
        if (!(_controlState is CasketControlState))
        {
            _controlState.DisableControl();
            _controlState = new CasketControlState(_player, _playerCamera, controlObjectLayer);
            _controlState.EnableControl();
            _controlState.OnExitState += ResetControl; 
            StartCoroutine(_decelerationEnumerator);
        }
    }

    public void SetObjectInspectorState(IInspectable item)
    {
        var inspectorControlState = new ObjectInspectorControlState(_player, _objectInspector);
        SetCustomState(inspectorControlState);
    }

    public void SetInventoryControlState()
    {
        _cinemachineInputProvider.enabled = false;
        var inventoryControlState = new InventoryControlState(_player, _inventoryView);
        SetCustomState(inventoryControlState);
    }

    public void ResetControl()
    {
        _cinemachineInputProvider.enabled = true;
        _controlState.DisableControl();
        StopCoroutine(_decelerationEnumerator);
        var standardControl = new StandardControlState(_player, _playerCamera, _hotbar);
        standardControl.OnInventoryOpen += SetInventoryControlState;
        _controlState = standardControl;
        _controlState.EnableControl();
        _controlState.OnExitState += SetControlLockState;
    }

    private void Update()
    {
        if (_controlState is StandardControlState)
        {
            var standartControl = _controlState as StandardControlState;
            standartControl.TiltPlayerTorso();
        }
    }

    private void FixedUpdate()
    {
        if(_controlState is StandardControlState)
        {
            var standartControl = _controlState as StandardControlState;
            standartControl.PlayerMove();
            standartControl.ScanObjectInFront();
        }
    }

    private void OnDisable()
    {
        _controlState.DisableControl();
    }

    private IEnumerator PlayerStoping()
    {
        while (!(_controlState is StandardControlState))
        {
            _player.Decceleration();
            yield return null;
        }
    }
}
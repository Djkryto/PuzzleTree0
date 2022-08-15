using Cinemachine;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private InventoryUI _inventoryUI;
    [SerializeField] private ObjectInspector _objectInspector;
    [SerializeField] private HotBar _hotbar;
    [SerializeField] private CinemachineInputProvider _cinemachineInputProvider;
    private ControlState _controlState;

    public ControlState ControlState => _controlState;

    private void Start()
    {
        _controlState = new StandardControlState(_player, _playerCamera, _inventoryUI, _hotbar);
        _controlState.OnChangeState += SetControlLockState;
        _player.Vision.Detected += SetItemControlState;
        _player.Vision.Undetected += ResetControl;
        _controlState.EnableControl();
    }

    public void SetControlLockState()
    {
        _cinemachineInputProvider.enabled = false;
        _controlState.DisableControl();
        _controlState = new ControlLockState(_player);
        _controlState.EnableControl();
        _controlState.OnChangeState += ResetControl;
    }

    public void SetItemControlState()
    {
        if(!(_controlState is ItemControlState))
        {
            _controlState.DisableControl();
            var itemControlState = new ItemControlState(_player, _playerCamera, _inventoryUI, _hotbar);
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
            _controlState.OnChangeState += ResetControl;
        }
    }

    public void SetObjectInspectorState(IInspectable item)
    {
        _controlState.DisableControl();
        var objectInspector = new ObjectInspectorControlState(_player, _objectInspector);
        objectInspector.OpenInspector(item);
        _controlState = objectInspector;
        _controlState.EnableControl();
        _controlState.OnChangeState += ResetControl;
    }

    public void ResetControl()
    {
        _cinemachineInputProvider.enabled = true;
        _controlState.DisableControl();
        _controlState = new StandardControlState(_player, _playerCamera, _inventoryUI, _hotbar);
        _controlState.EnableControl();
        _controlState.OnChangeState += SetControlLockState;
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

    //private IEnumerator PlayerStoping()
    //{
    //    //while (!_controlState is StandartControlState ^)
    //    //{
    //    //    _player.Decceleration();
    //    //    yield return null;
    //    //}
    //}
}
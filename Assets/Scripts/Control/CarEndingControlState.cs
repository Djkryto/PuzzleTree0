using UnityEngine;

public class CarEndingControlState : StandardControlState
{
    private Camera _playerCamera;
    private LayerMask _layerActivateControl;
    
    public CarEndingControlState(Player player, LayerMask layerActivateControl, Camera playerCamera, HotbarView hotbar) : base(player, playerCamera, hotbar)
    {
        _playerCamera = playerCamera;
        _layerActivateControl = layerActivateControl;
        Input.Player.Use.performed += context => ActivateEnding();
    }

    private void ActivateEnding()
    {
        var mousePosition = Input.Player.MousePosition.ReadValue<Vector2>();
        var mouseRay = _playerCamera.ScreenPointToRay(mousePosition);
        if(Physics.Raycast(mouseRay, out RaycastHit hitObject, _layerActivateControl))
        {
            TryStartEnding(hitObject);
        }
    }

    private void TryStartEnding(RaycastHit hitObject)
    {
        if(hitObject.transform.TryGetComponent(out Car car))
        {
            car.Ending();
        }
    }
}
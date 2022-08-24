using UnityEngine;

public class CasketControlState : ControlLockState
{
    private Camera _camera;
    private StandardControlState _standardControl;
    private LayerMask _controlObjectLayer;

    public CasketControlState(Player player, Camera camera, LayerMask controlObjectLayer) : base(player)
    {
        _camera = camera;
        _controlObjectLayer = controlObjectLayer;
        Input.Player.LMB.performed += context => ClickButton();
        _standardControl = new StandardControlState(player, camera, null);
        Input.Player.MouseLook.performed += context => Player.Rotate(_camera.transform.eulerAngles.y);
        Input.Player.MouseLook.performed += context => _standardControl.TiltPlayerTorso(context.ReadValue<Vector2>());
    }

    private void ClickButton()
    {
        var mousePosition = Input.Player.MousePosition.ReadValue<Vector2>();
        var mouseRay = _camera.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(mouseRay, out hit, _controlObjectLayer))
        {
            if (hit.collider.TryGetComponent(out ButtonCasket button))
            {
                button.Press();
            }
        }
    }
}
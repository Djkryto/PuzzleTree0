using Cinemachine;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private CinemachineInputProvider _inputProvider;

    public Camera Camera => _camera;

    public void LockCamera()
    {
        _inputProvider.enabled = false;
    }

    public void UnlockCamera()
    {
        _inputProvider.enabled = true;
    }

    public Ray GetRayFromCenter()
    {
        var cameraCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        return _camera.ScreenPointToRay(cameraCenter);
    }
}

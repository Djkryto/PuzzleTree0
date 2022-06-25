using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotate : MonoBehaviour
{
    [SerializeField] private float _horizontalSensitivity;
    [SerializeField] private float _verticalSensitivity;
    [SerializeField] private float _verticalLimit = 60f;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _aimTarget;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Camera _mainCamera;
    public bool isRotate;
    private PlayerInput _input;
    private Vector2 _currentAngle;

    private void Awake()
    {
        _input = new PlayerInput();
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    void Update()
    {
        if (isRotate)
        {
            Rotate();
        }
    }

    private void Rotate()
    {
        var parentRotation = _playerTransform.localEulerAngles;
        parentRotation.y = _mainCamera.transform.eulerAngles.y;
        _rigidbody.MoveRotation(Quaternion.Euler(parentRotation));


        //Ray desiredTargetRay = _mainCamera.ScreenPointToRay(new Vector2(Screen.width / 2f, Screen.height / 2f));
        //Vector3 desiredTargetPosition = desiredTargetRay.origin + desiredTargetRay.direction;
        //_aimTarget.position = desiredTargetPosition;
    }
}

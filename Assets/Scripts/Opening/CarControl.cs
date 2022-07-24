using UnityEngine;

public class CarControl : MonoBehaviour
{
    [SerializeField] private Rigidbody _carRigidbody;
    [SerializeField] private float _maxSpeed;

    [SerializeField] private Transform _wheelFrontLeftTransform;
    [SerializeField] private Transform _wheelFrontRightTransform;
    [SerializeField] private Transform _wheelRearLeftTransform;
    [SerializeField] private Transform _wheelRearRightTransform;

    [SerializeField] private WheelCollider _wheelFrontLeftCollider;
    [SerializeField] private WheelCollider _wheelFrontRightCollider;
    [SerializeField] private WheelCollider _wheelRearLeftCollider;
    [SerializeField] private WheelCollider _wheelRearRightCollider;

    [SerializeField] private Transform _rudderTransform;
    [SerializeField] private float _maxRudderAngle;
    [SerializeField] private float _engineForce;

    private UserInput _userInput;

    private void Awake()
    {
        _userInput = new UserInput();
    }

    private void Start()
    {
        _carRigidbody.velocity = transform.forward * _maxSpeed;
    }

    private void OnEnable()
    {
        _userInput.Enable();
    }

    private void OnDisable()
    {
        _userInput.Disable();
    }

    private void FixedUpdate()
    {
        CarLogic();
    }

    private void CarLogic()
    {
        CarMove();
        TurnCar();

        RotateWheel(_wheelFrontLeftCollider, _wheelFrontLeftTransform);
        RotateWheel(_wheelFrontRightCollider, _wheelFrontRightTransform);
        RotateWheel(_wheelRearLeftCollider, _wheelRearLeftTransform);
        RotateWheel(_wheelRearRightCollider, _wheelRearRightTransform);
    }

    private void CarMove()
    {
        if (_carRigidbody.velocity.magnitude < _maxSpeed)
        {
            _wheelFrontLeftCollider.motorTorque = _engineForce;
            _wheelFrontRightCollider.motorTorque = _engineForce;
        }
        else
        {
            _wheelFrontLeftCollider.motorTorque = 0f;
            _wheelFrontRightCollider.motorTorque = 0f;
        }
    }

    private void TurnCar()
    {
        var inputAxis = _userInput.Player.Move.ReadValue<Vector2>();
        var rudderAndle = Quaternion.Euler(-Vector3.forward * _maxRudderAngle * 10f * inputAxis.x);
        _rudderTransform.rotation = Quaternion.Lerp(_rudderTransform.rotation, rudderAndle, 10f * Time.deltaTime);
        _wheelFrontLeftCollider.steerAngle = _maxRudderAngle * inputAxis.x;
        _wheelFrontRightCollider.steerAngle = _maxRudderAngle * inputAxis.x;
    }

    private void RotateWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 position;
        Quaternion rotation;
        wheelCollider.GetWorldPose(out position, out rotation);
        wheelTransform.position = position;
        wheelTransform.rotation = rotation;
    }

    public void SlowDown()
    {
        _wheelFrontLeftCollider.brakeTorque = 3000f;
        _wheelFrontRightCollider.brakeTorque = 3000f;
    }
}

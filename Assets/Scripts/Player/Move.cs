using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private float Speed = 3;
    [SerializeField] private float FastSpeed;
    [SerializeField] private Rigidbody _playerRigidbody;
    public AudioManager audioManager;
    private UserInput _playerInput;
    public bool isMove;
    private float DefaultSpeed = 3;

    private void Awake()
    {
        _playerInput = new UserInput();
        _playerRigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _playerInput?.Enable();
    }

    private void OnDisable()
    {
        _playerInput?.Disable();
    }

    void FixedUpdate()
    {
        if (isMove)
        {
            Movement();
        }
    }
    private void Movement()
    {
        if (_playerInput.Player.Run.IsPressed())
        {
            Movement(FastSpeed);
        }
        else
        {
            Movement(Speed);
        }
    }

    private void Movement(float speed)
    {
        var speedVector = CalculateMovementVector() * speed;
        speedVector.y = _playerRigidbody.velocity.y;
        _playerRigidbody.velocity = speedVector;
    }

    private Vector3 CalculateMovementVector()
    {
        var inputMovementVector = _playerInput.Player.Move.ReadValue<Vector2>();
        Vector3 rightMovementVector = inputMovementVector.x * transform.right.normalized;
        Vector3 forwardMovementVector = inputMovementVector.y * transform.forward.normalized;
        var movementVector = rightMovementVector + forwardMovementVector;
        return movementVector;
    }

    public void SetSpeed(float changeble)
    {
        Speed = changeble;
    }
    //private void JumpLogic()
    //{
    //    if (Input.GetAxis("Jump") > 0)
    //    {
    //        if (isGrounded)
    //        {
    //            // Обратите внимание что я делаю на основе Vector3.up а не на основе transform.up
    //            // если наш персонаж это шар -- его up может быть в том числе и вниз и влево и вправо. 
    //            // Но нам нужен скачек только вверх! Потому и Vector3.up
    //            rb.AddForce(Vector3.up * JumpForce);
    //        }
    //    }
    //}
    public void SetDefaultSpeed()
    {
        Speed = DefaultSpeed;
    }
}

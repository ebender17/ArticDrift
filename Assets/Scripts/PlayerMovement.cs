using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader = default; 

    public CharacterController controller;
    public Transform cam;

  
    public float moveSpeed = 6f;
    public float turnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;

    public float gravity = -9.81f;
    private Vector3 _velocity;
    private bool isGrounded;
    public float jumpHeight = 3f; 

    public Transform groundCheck;
    public float groundRadius = 0.4f;
    public LayerMask whatIsGround; 

    [HideInInspector] public Vector3 direction;
    [HideInInspector] public bool jumpInput;
    [HideInInspector] public bool jumpInputStop; 

    private void OnEnable()
    {
        _inputReader.jumpEvent += OnJump;
        _inputReader.jumpCanceledEvent += OnJumpCanceled;
        _inputReader.moveEvent += OnMove;
    }

    private void OnDisable()
    {
        _inputReader.jumpEvent -= OnJump;
        _inputReader.jumpCanceledEvent -= OnJumpCanceled;
        _inputReader.moveEvent -= OnMove;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleMoveInput();
        HandleJumpInput();
    }

    private void FixedUpdate()
    {
       
    }
    private void HandleMoveInput()
    {
        if (direction.magnitude >= 0.1f)
        {
            //Pass in x first then y instead of y then x to get angle we want
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //Give us direction we want to move in taking into account dir of camera
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
        }
    }

    private void HandleJumpInput()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, whatIsGround);

        if (isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        if (jumpInput && isGrounded)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        _velocity.y += gravity * Time.deltaTime;
        controller.Move(_velocity * Time.deltaTime);
    }

    //------ Event Listeners ------
    private void OnMove(Vector2 movement)
    {
        direction = new Vector3(movement.x, 0f, movement.y).normalized;
    }

    private void OnJump()
    {
        Debug.Log("Player Jump Initiated");
        jumpInput = true;
        jumpInputStop = false;

    }

    private void OnJumpCanceled()
    {
        Debug.Log("Player Jump Cancelled");
        jumpInputStop = true;
        jumpInput = false;
    }

}

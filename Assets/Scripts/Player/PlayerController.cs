using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader = default; 

    public CharacterController controller;
    public Transform cam;
    private Animator anim;

    public float moveSpeed = 6f;
    public float turnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;

    public float gravity = -9.81f;
    private Vector3 _velocity;
    private bool isGrounded;
    [SerializeField] public float jumpHeight = 3f;
    public float fallMultiplier = 2.5f; //How much we multiply gravity by when character is falling down
    [Range(0, 1)] public float lowJumpMultiplier = 0.5f; //When we release button early and want to increase gravity so character does not jump quiete as high

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
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(direction.magnitude >= 0.1f)
        {
            HandleMoveInput();

            if(isGrounded)
                anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
            

        HandleJumpInput();
    }


    private void HandleMoveInput()
    {
        //Pass in x first then y instead of y then x to get angle we want
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        //Give us direction we want to move in taking into account dir of camera
        Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        controller.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
    }

    private void HandleJumpInput()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, whatIsGround);

        //This might register before being completely on the ground. Therefore, works better to set velocity to smaller number than 0...
        //to force player down on the ground.
        if (isGrounded && _velocity.y < 0)
        {
            //Reset velocity once grounded
            _velocity.y = -2f;
            anim.SetBool("isJumping", false);
        }

        //when velcity is less than 0 we are falling and want to apply more "gravity" for a snappier look
        if (_velocity.y < 0 && !isGrounded)
        {
            _velocity.y += fallMultiplier * gravity * Time.deltaTime;

            controller.Move(_velocity * Time.deltaTime);
        }
        else
        {
            _velocity.y += gravity * Time.deltaTime;
            controller.Move(_velocity * Time.deltaTime);
        }

        //If player tapped jump key quickly
        //Varying jump heights
        if (_velocity.y > 0 && jumpInputStop)
        {
            _velocity.y *= lowJumpMultiplier;
        } 

        if (jumpInput && isGrounded)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            anim.SetBool("isJumping", true);
        }

        
    }

    //------ Event Listeners ------
    private void OnMove(Vector2 movement)
    {
        direction = new Vector3(movement.x, 0f, movement.y).normalized;
    }

    private void OnJump()
    {
        jumpInput = true;
        jumpInputStop = false;
        Debug.Log("Jump pressed!");

    }

    private void OnJumpCanceled()
    {
        jumpInputStop = true;
        jumpInput = false;
    }

}

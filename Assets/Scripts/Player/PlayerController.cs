using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader = default; 

    public CharacterController controller;
    public Transform cam;
    private Animator anim;

    private Vector3 _currentMoveVelocity = Vector3.zero;
    private Vector3 _moveVelocity;
    public float moveSpeed = 6f;
    public float turnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;

    public float gravity = -9.81f;
    [HideInInspector] public Vector3 velocity;
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

    [Range(0, 1)] public float iceFriction = 1; //between 0 and 1, 0 means no friction
    private bool _applyFriction;


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

        //Want to continue applying movement if on icy ground
        if(_applyFriction)
        {
            HandleSurfaceFriction();
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
        
        _moveVelocity = moveDirection.normalized * moveSpeed;

        if(_applyFriction)
        {
            HandleSurfaceFriction();
        }
        else
        {
            controller.Move(_moveVelocity * Time.deltaTime);
        }
        
    }

    //Simple function to give slippery ice effect 
    private void HandleSurfaceFriction()
    {
        _currentMoveVelocity = Vector3.Lerp(_currentMoveVelocity, _moveVelocity, iceFriction * Time.deltaTime);

        controller.Move(_currentMoveVelocity * Time.deltaTime);

    }

    private void HandleJumpInput()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, whatIsGround);

        //This might register before being completely on the ground. Therefore, works better to set velocity to smaller number than 0...
        //to force player down on the ground.
        if (isGrounded && velocity.y < 0)
        {
            //Reset velocity once grounded
            velocity.y = -2f;
            anim.SetBool("isJumping", false);
            
        }


        //If player tapped jump key quickly
        //Varying jump heights
        if (velocity.y > 0 && jumpInputStop)
        {
            velocity.y *= lowJumpMultiplier;
        } 

        if (jumpInput && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            anim.SetBool("isJumping", true);
        }

        //when velcity is less than 0 we are falling and want to apply more "gravity" for a snappier look
        if (velocity.y < 0 && !isGrounded)
        {
            velocity.y += fallMultiplier * gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
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

    }

    private void OnJumpCanceled()
    {
        jumpInputStop = true;
        jumpInput = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Ice")
        {
            Debug.Log("Entered ice and ice tag detected.");
            _applyFriction = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform.tag == "Ice")
        {
            _applyFriction = false;
        }
    }

}

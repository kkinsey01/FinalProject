using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    GameState gameState;



    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public bool readyToJump;
    public float regJumpForce;

    [Header("KeyBinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode smallJumpKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    Vector3 oldPos;

    Rigidbody rb;

    bool bossFight;
    bool swingSword;

    public float rotationSpeed;

    private enum AnimationStateEnum
    {
        Idle = 0,
        Running = 1,
        Jumping = 2,
        Throw = 3,
        SwingSword = 4,
        RunWithSword = 5
    }
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        regJumpForce = jumpForce;
        gameState.movementSpeed = 7f;
        gameState.canJump = true;
        animator = gameObject.transform.Find("Model").GetComponent<Animator>();
        bossFight = false;
        swingSword = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        moveSpeed = gameState.movementSpeed;
        MyInput();
        SetAnimationState();
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
        if (gameState.canJump)
        {
            if (Input.GetKey(jumpKey) && readyToJump && grounded)
            {
                readyToJump = false;

                Jump();

                Invoke(nameof(ResetJump), jumpCooldown);
            }
            if (Input.GetKey(smallJumpKey) && readyToJump && grounded)
            {
                readyToJump = false;
                Jump2();
                Invoke(nameof(ResetJump), jumpCooldown);
            }
        }
        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        if (SceneManager.GetActiveScene().buildIndex == 8)
        {
            bossFight = true;
        }
        if (bossFight)
        {
            if (Input.GetKeyUp(KeyCode.F) && grounded)
            {
                Debug.Log("Swinging");
                swingSword = true;
            }
        }
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void LateUpdate()
    {
        oldPos = transform.position;
    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

    }
    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if(!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }
    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void Jump2()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce / 2, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("JumpPad"))
        {
            jumpForce = 1.75f * regJumpForce;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals("JumpPad"))
        {
            jumpForce = regJumpForce;
        }
    }
    void SetAnimationState()
    {
        AnimationStateEnum playerAnimationState;
        if (grounded)
        {
            if (moveDirection == Vector3.zero)
            {
                playerAnimationState = AnimationStateEnum.Idle;
            }
            else
            {
                if (!bossFight)
                {
                    playerAnimationState = AnimationStateEnum.Running;
                }
                else
                {
                    playerAnimationState = AnimationStateEnum.RunWithSword;
                }
            }
        }
        else
        {
            playerAnimationState = AnimationStateEnum.Jumping;
        }
        if (gameState.throwBall)
        {
            playerAnimationState = AnimationStateEnum.Throw;
            gameState.throwBall = false;
        }
        if (swingSword)
        {
            playerAnimationState = AnimationStateEnum.SwingSword;
            swingSword = false;
        }
        animator.SetInteger("playerState", (int)playerAnimationState);
    }
    void ResetDamageDone()
    {
        gameState.damageDone = false;
    }
}

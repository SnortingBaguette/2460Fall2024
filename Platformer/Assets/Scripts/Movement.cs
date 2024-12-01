using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Movement : MonoBehaviour
{
    public float movementSpeed = 7f;
    public float speedModifier = 15f;
    public float groundDrag;
    private float horizontalInput;

    public Transform orientation;

    Vector3 moveDirection;

    Rigidbody rb;

    public float playerHeight;
    public LayerMask groundMask;
    private bool grounded;

    public float jumpForce;
    public float airMultiplier;
    private bool readyToJump = true;
    private int amountOfJumps = 0;
    private float jumpCooldown = .25f;
    private WaitForSeconds jumpDelay;
    private int amountOfDashes = 0;
    private float dashDirection;
    private bool readyToDash = true;

    private float coyoteTime;

    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode dashKey = KeyCode.LeftShift;

    public Vector3Data teleportPosition;

    public float rbAccel = 1.6f;

    public UnityEvent dashStartEvent, dashEndEvent, jumpEvent, groundedEvent, notGroundedEvent, moveRightEvent, turnModelRightEvent, turnModelLeftEvent, moveLeftEvent, noMoveEvent, airborneEvent;
    public ParticleSystem walkingParticles;
    private int animationState = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        jumpDelay = new WaitForSeconds(jumpCooldown);
    }

    private void InputMethod()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if(Input.GetKeyDown(jumpKey))
        {
            Jump();
        }
        if (Input.GetKeyDown(dashKey))
        {
            Dash();
        }
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.1f, groundMask);
        if (grounded)
        {
            rb.drag = groundDrag;
            amountOfJumps = 0;
            coyoteTime = .15f;
            rb.mass = 1;
            groundedEvent.Invoke();
            walkingParticles.startSize = 0.3f * rb.velocity.magnitude;
        }
        else
        {
            rb.drag = 0;
            coyoteTime -= Time.deltaTime;
            rb.mass = rb.mass + rbAccel * Time.deltaTime;
            notGroundedEvent.Invoke();

        }
        InputMethod();
        SpeedControl();
                   
        
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        moveDirection = orientation.right * horizontalInput;
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * movementSpeed * speedModifier, ForceMode.Force);
            amountOfDashes = 0;
        }
        else
        {
            rb.AddForce(moveDirection.normalized * movementSpeed * speedModifier * airMultiplier, ForceMode.Force);
        }

        if(grounded)
        {
            if(horizontalInput == 1 && animationState != 1)
            {
                moveRightEvent.Invoke();
                animationState = 1;
                turnModelRightEvent.Invoke();
            }
            else if(horizontalInput == -1 && animationState != -1)
            {
                moveLeftEvent.Invoke();
                animationState = -1; 
                turnModelLeftEvent.Invoke();
            }
            else if(horizontalInput == 0 && animationState != 0)
            {
                noMoveEvent.Invoke();
                animationState = 0;
            }                                   //Turn the model depending on the input
        }
        else if (!grounded)
        {

            if (animationState != 3)
        {
            airborneEvent.Invoke();
            animationState = 3;
        }

            if(horizontalInput == 1)
            {
                turnModelRightEvent.Invoke();
            }
            else if(horizontalInput == -1)
            {
                turnModelLeftEvent.Invoke();
            }
        }


        /*if(grounded && animationState != 1)
            {
                moveRightEvent.Invoke();
                animationState = 1;
            }

        if(horizontalInput == 1 )
        {
            turnModelRightEvent.Invoke();
            if(grounded && animationState != 1)
            {
                moveRightEvent.Invoke();
                animationState = 1;
            }
        } 
        else if(horizontalInput == -1) 
        {
            turnModelLeftEvent.Invoke();
            if(animationState != -1 && grounded)
            {
                moveLeftEvent.Invoke();
                animationState = -1;
            }
        }
        else if (horizontalInput == 0 && animationState != 0 && grounded)
        {
            noMoveEvent.Invoke();
            animationState = 0;
        }
        else if (animationState != 3 && !grounded)
        {
            airborneEvent.Invoke();
            animationState = 3;
        }
        Debug.Log(horizontalInput);*/
        
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if(flatVel.magnitude > movementSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * movementSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        
        if (amountOfJumps < 1 && readyToJump)
        {
            jumpEvent.Invoke();
            rb.mass = 1f;
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            if(coyoteTime <= 0)
            {
                amountOfJumps++;
            }

            readyToJump = false;
            StartCoroutine(JumpCooldown());
        }

    }
    private void Dash()
    {
        if (amountOfDashes < 2 && horizontalInput != 0 && readyToDash)
        {
            dashStartEvent.Invoke();
            rb.mass = 1;
            dashDirection = horizontalInput;
            movementSpeed = 20f;
            ResetVelocity();
            rb.useGravity = false;
            rb.AddForce(transform.right * 10f * dashDirection, ForceMode.Impulse);
            amountOfDashes++;
            readyToDash = false;
            StartCoroutine(DashCooldown());
            readyToJump = false;
            StartCoroutine(JumpCooldown());
        }
    }
    private IEnumerator JumpCooldown()
    {
        yield return jumpDelay;
        readyToJump = true;
        
    }

    private IEnumerator DashCooldown()
    {
        yield return jumpDelay;
        readyToDash = true;
        rb.useGravity = true;
        movementSpeed = 7f;
        dashEndEvent.Invoke();
    }

    public void ResetVelocity()
    {
        rb.velocity = new Vector3(0f, 0f, 0f);
    }

    public void ResetDashAndJump()
    {
        amountOfDashes = 0;
        amountOfJumps = 0;
    }

    public void TeleportToLastPos()
    {
        transform.position = teleportPosition.value;
    }
}

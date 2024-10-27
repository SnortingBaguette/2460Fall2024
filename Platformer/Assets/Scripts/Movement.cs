using System.Collections;
using UnityEngine;

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
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.001f, groundMask);
        if (grounded)
        {
            rb.drag = groundDrag;
            amountOfJumps = 0;
            coyoteTime = .15f;
        }
        else
        {
            rb.drag = 0;
            coyoteTime -= Time.deltaTime;
        }
        InputMethod();
        SpeedControl();
                   
        Debug.Log(amountOfDashes + " " + readyToDash + " " + coyoteTime);
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
            dashDirection = horizontalInput;
            movementSpeed = 20f;
            ResetVelocity();
            rb.useGravity = false;
            rb.AddForce(transform.right * 10f * dashDirection, ForceMode.Impulse);
            amountOfDashes++;
            readyToDash = false;
            StartCoroutine(DashCooldown());
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
    }

    private void ResetVelocity()
    {
        rb.velocity = new Vector3(0f, 0f, 0f);
    }

    public void ResetDashAndJump()
    {
        amountOfDashes = 0;
        amountOfJumps = 0;
    }
}

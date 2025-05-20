using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isGameOver = false;

    [Header("Player Config")]
    public Transform orientation;
    public float moveSpeed;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;

    public float playerHeight = 1f;

    [Header("Drag Controller")]
    public LayerMask groundLayer;
    public float groundDrag;
    bool isReadyToJump = true;
    bool isGrounded = false;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;
    private Vector3 wallContactNormal = Vector3.zero;
    private bool hasWallContact = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    public void OnGameOver()
    {
        isGameOver = true;
    }
    void Update()
    {
        if (isGameOver)
        {
            return;
        }
        isGrounded = Physics.CheckSphere(transform.position - new Vector3(0, playerHeight / 2f, 0), 0.3f, groundLayer);

        HandleInputs();
        SpeedControl();


        if (isGrounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0;
    }


    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            wallContactNormal = collision.contacts[0].normal;
            hasWallContact = true;
        }
    }


    void FixedUpdate()
    {
        if (isGameOver)
        {
            return;
        }
        if (hasWallContact)
        {
            moveDirection = Vector3.ProjectOnPlane(moveDirection, wallContactNormal).normalized;

            if (isGrounded && rb.linearVelocity.y <= 0.01f)
            {
                rb.linearVelocity = moveDirection * moveSpeed;
            }
            else
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y - 0.2f, rb.linearVelocity.z);
            }
        }

        MovePlayer();

        hasWallContact = false;
    }


    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (isGrounded)
            rb.AddForce(moveDirection * moveSpeed * 10f, ForceMode.Force);
        else if (!isGrounded)
            rb.AddForce(moveDirection * moveSpeed * 10f * airMultiplier, ForceMode.Force);

    }

    void SpeedControl()
    {
        Vector3 flatVal = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (flatVal.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVal.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }




    private void HandleInputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(jumpKey) && isReadyToJump && isGrounded)
        {
            isReadyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }


    private void ResetJump()
    {
        isReadyToJump = true;
    }
}



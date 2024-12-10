using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 10.0f;
    public float jumpHeight = 3;

    public float dashSpeed = 100;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1.0f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundMask;

    [Header("Jump Mechanics")]
    public float coyoteTime = 0.2f;
    public float jumpBufferTime = 0.2f;

    public int maxJumps = 1;

    [Header("Audio")]
    public AudioClip jumpSound; 
    public AudioClip footstepSound; 
    public float footstepInterval = 0.5f; 

    private AudioSource audioSource;

    private int jumpsLeft;
    private float jumpBuffer;
    private float coyoteCounter;
    private bool isGrounded;
    private Rigidbody2D rb;
    private float horizontal;

    private bool isDashing;
    private float dashTime;
    private float dashCooldownTime;

    private float footstepTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundMask);

        if (isGrounded)
        {
            coyoteCounter = coyoteTime;
            jumpsLeft = maxJumps;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBuffer = jumpBufferTime;
        }

        if ((coyoteCounter > 0 || jumpsLeft > 0) && jumpBuffer > 0)
        {
            jumpBuffer = 0;

            var jumpVelocity = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);

            if (audioSource != null && jumpSound != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }

            if (!isGrounded)
            {
                jumpsLeft--;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTime <= 0)
        {
            isDashing = true;
            dashTime = dashDuration;
            dashCooldownTime = dashCooldown;
        }

        if (isDashing)
        {
            rb.velocity = new Vector2(dashSpeed * horizontal, rb.velocity.y);
            dashTime -= Time.deltaTime;

            if (dashTime <= 0)
            {
                isDashing = false;
            }
        }

        dashCooldownTime -= Time.deltaTime;

        if (isGrounded && Mathf.Abs(horizontal) > 0.1f)
        {
            footstepTimer -= Time.deltaTime;

            if (footstepTimer <= 0 && footstepSound != null)
            {
                audioSource.PlayOneShot(footstepSound);
                footstepTimer = footstepInterval;
            }
        }
        else
        {
            footstepTimer = 0;
        }
    }

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        print(other.relativeVelocity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (groundCheck != null)
        {
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}

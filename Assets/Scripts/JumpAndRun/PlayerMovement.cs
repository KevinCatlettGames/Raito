using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;         // A position marking where to check if the player is grounded.
    [Tooltip("Size of the overlap circle to determine if the player is grounded")]
    [SerializeField] float groundCheckRadius;               // Radius of the overlap circle to determine if grounded.
    [SerializeField] private LayerMask groundLayer;         // A mask determining what is ground to the character.
    [SerializeField] private Transform wallCheck;           // A position marking where to check if the player is touching a wall.
    [Tooltip("Size of the overlap circle to determine if the player is walled.")]
    [SerializeField] float wallCheckRadius;                 // Radius of the overlap circle to determine if the player is touching a wall.
    [SerializeField] private LayerMask wallLayer;           // A mask determining what is wall to the character.
    [Space]
    [Tooltip("Moving speed of the player.")]
    [SerializeField] float speed;                           // Amount of Speed when running.
    [Tooltip("Jump power the player.")]
    [SerializeField] float jumpingPower;                    // Amount of Power added when the player jumps.
    [Tooltip("Amount of extra jumps of the player can do.")]
    [SerializeField] int extraJumpsValue;                   // Amount of extra jumps the player can do after jumping.
    [Tooltip("Speed of the player while sliding on a wall.")]
    [SerializeField] float wallSlidingSpeed;                // Amount of Speed when sliding on a wall.
    [Space]
    [SerializeField] float dashingPower;
    [SerializeField] float dashingTime;
    [SerializeField] float dashingCooldown;

    private float horizontal;
    private int extraJumps;                                 // Counter for extra jumps. 
    private bool isFacingRight = true;                      // For determining which way the player is currently facing.
    private bool isWallSliding;

    private bool canDash = true;
    private bool isDashing;


    // Start is called before the first frame update
    void Start()
    {
        extraJumps = extraJumpsValue;
    }

    // Update is called once per frame
    void Update()
    {
        // If the player is grounded, 
        if (IsGrounded())
        {
            extraJumps = extraJumpsValue;   // ... resets extra jumps.
        }

        Flip();                     // ... flip the player.
        WallSlide();                // ... player slides on a wall.
        UpdateAnimationState();     // ... update the animation states.
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void Flip()
    {
        if (!isFacingRight && horizontal > 0f || isFacingRight && horizontal < 0f)
        {
            // Switches the way the player is labeled as facing.
            isFacingRight = !isFacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        // Moves the player horizontally.
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        // if player have extra jumps.
        if (context.performed && extraJumps > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            // reduce extra jumps by 1.
            extraJumps--;
        }
        // if the player have no more extra jumps.
        else if (context.performed && IsGrounded() && extraJumps == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private bool IsGrounded()
    {
        // The player is grounded if a circle cast to the ground check position hits anything designated as ground.
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private bool IsWalled()
    {
        // The player is walled if a circle cast to the wall check position hits anything designated as wall.
        return Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, wallLayer);
    }

    private void WallSlide()
    {
        if (horizontal != 0f && IsWalled() && !IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private void UpdateAnimationState()
    {
        // Horizontal movement Animation.
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        // Jumping and Falling Animation.
        if (rb.velocity.y > .1f && !IsGrounded())
        {
            animator.SetBool("IsJumping", true);
        }
        else if (rb.velocity.y < -.1f && !IsGrounded())
        {
            animator.SetBool("IsFalling", true);
        }
        else
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", false);
        }

        // Wall sliding Animation.
        if (isWallSliding == true)
        {
            animator.SetBool("IsWallsliding", true);
        }
        else
        {
            animator.SetBool("IsWallsliding", false);
        }

        if (isDashing == true)
        {
            animator.SetBool("IsDashing", true);
        }
        else
        {
            animator.SetBool("IsDashing", false);
        }
    }
}

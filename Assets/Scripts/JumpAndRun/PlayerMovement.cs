// Written by Keir Lobo.
// Sound implemented by Nils Ebert. 

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region Variables

    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;

    [Header("Groundcheck Settings")]
    [SerializeField, Tooltip("Size of the overlap circle to determine if the player is grounded")] 
    private Transform groundCheck;
    [SerializeField, Tooltip("Radius of the overlap circle to determine if grounded.")] 
    float groundCheckRadius;
    [SerializeField, Tooltip("A mask determining what is ground to the character.")] 
    private LayerMask groundLayer;

    [Header("Wallcheck Settings")]
    [SerializeField, Tooltip("Size of the overlap circle to determine if the player is walled.")] 
    private Transform wallCheck;                            // A position marking where to check if the player is touching a wall.
    [SerializeField, Tooltip("Radius of the wallcheck overlap circle.")] 
    float wallCheckRadius;                                  // Radius of the overlap circle to determine if the player is touching a wall.
    [SerializeField, Tooltip("A mask that determines what is wall to the player.")] 
    private LayerMask wallLayer;                            // A mask determining what is wall to the character.

    [Header("Player Movement Settings")]
    [SerializeField, Tooltip("Moving speed of the player.")] 
    float speed;                                            // Amount of Speed when running.
    [SerializeField, Tooltip("Jump power of the player.")] 
    float jumpingPower;                                     // Amount of Power added when the player jumps.
    [SerializeField, Tooltip("Amount of extra jumps of the player can do after jumping.")] 
    int extraJumpsValue;                                    // Amount of extra jumps the player can do after jumping.
    [SerializeField, Tooltip("Speed of the player while sliding on a wall.")] 
    float wallSlidingSpeed;                                 // Amount of Speed when sliding on a wall.

    [Header("PLayer Dash Settings")]
    [SerializeField, Tooltip("Amount of Power added when the player dashes.")] 
    float dashingPower;
    [SerializeField, Tooltip("Amount of time how long the dash occurs.")] 
    float dashingTime;
    [SerializeField, Tooltip("Amount of time when the player can dash again after dashing.")]
    float dashingCooldown;

    [Header("Particle Systems")]
    [SerializeField] ParticleSystem dust;
    [SerializeField] ParticleSystem sparkle;

    [Header("Wallslide Sound")]
    [SerializeField] AudioSource audioSourceWallslide;
    [SerializeField] AudioClip audioClipWallslide;

    private float horizontal;
    private int extraJumps;                                 // Counter for extra jumps. 
    private bool isFacingRight = true;                      // For determining which way the player is currently facing.
    private bool isWallSliding;                             // For determining if player is wall sliding.
    private bool canDash = true;                            // For determining if player can dash again.
    private bool isDashing;                                 // For determining if player is dashing.

    // Animation States
    const string PLAYER_IDLE = "Player_Idle";
    const string PLAYER_RUN = "Player_Running";
    const string PLAYER_JUMP = "Player_Jumping";
    const string PLAYER_FALL = "Player_Falling";
    const string PLAYER_WALLSLIDE = "Player_WallSliding";
    const string PLAYER_DASH = "Player_Dash";

    RespawnToStartPosition respawnToStartPosition;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        extraJumps = extraJumpsValue;
        respawnToStartPosition = GetComponent<RespawnToStartPosition>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the player is grounded or walled 
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
        if (isDashing) return;

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    /// <summary>
    /// Flips that character based on facing direction.
    /// </summary>
    private void Flip()
    {
        if (!isFacingRight && horizontal > 0f || isFacingRight && horizontal < 0f)
        {
            // Create dust, if player is grounded.
            if (IsGrounded())
            {
                CreateDust();
            }

            // Switches the way the player is labeled as facing.
            isFacingRight = !isFacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    /// <summary>
    /// Player horizontal movement.
    /// </summary>
    /// <param name="context"></param>
    public void Move(InputAction.CallbackContext context)
    {
        if (respawnToStartPosition.Respawning) return;

        horizontal = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (respawnToStartPosition.Respawning) return;

        // if player have extra jumps.
        if (context.performed && extraJumps > 0)
        {
            // If player jumps of the ground, create dust.
            if (IsGrounded())
            {
                CreateDust();
            }
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            // reduce extra jumps by 1.
            extraJumps--;
            animator.Play(PLAYER_JUMP, -1, 0f);
        }
        // if the player have no more extra jumps.
        else if (context.performed && IsGrounded() && extraJumps == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);        
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (respawnToStartPosition.Respawning) return;

        if (context.performed && canDash)
        {
            CreateSparkle();
            StartCoroutine(Dash());
        }
    }

    public void BoostUp(float power)
    {
        CreateDust();
        extraJumps = extraJumpsValue;
        rb.velocity = new Vector2(rb.velocity.x, power);
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
            var velocity = rb.velocity;
            rb.velocity = new Vector2(velocity.x, Mathf.Clamp(velocity.y, -wallSlidingSpeed, float.MaxValue));
            isWallSliding = true;
            PlayWallslideAudio();
        }
        else
        {
            isWallSliding = false;
            StopWallslideAudio();
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

    private void ChangeAnimationState(string newState)
    {
        animator.Play(newState);
    }

    private void UpdateAnimationState()
    {
        // Idle and Running state
        if (IsGrounded() && !isDashing)
        {
            if (horizontal != 0)
            {
                ChangeAnimationState(PLAYER_RUN);
            }
            else if (horizontal == 0)
            {
                ChangeAnimationState(PLAYER_IDLE);
            }
        }
        
        // Jump and Falling state
        if (!IsGrounded() && !isWallSliding && !isDashing)
        {
            if (rb.velocity.y > .1f)
            {
                ChangeAnimationState(PLAYER_JUMP);
            }
            else if (rb.velocity.y < -.1f)
            {
                ChangeAnimationState(PLAYER_FALL);
            }
        }

        // Wallsliding and Dashing state
        if (isWallSliding)
        {
            ChangeAnimationState(PLAYER_WALLSLIDE);
        }
        else if (isDashing)
        {
            ChangeAnimationState(PLAYER_DASH);
        }
    }

    private void CreateDust()
    {
        dust.Play();
    }
    
    private void CreateSparkle()
    {
        sparkle.Play();
    }

    private void PlayWallslideAudio()
    {
        if (audioSourceWallslide.isPlaying == false)
        {
            audioSourceWallslide.PlayOneShot(audioClipWallslide, 0.25f);
        }
    }

    private void StopWallslideAudio()
    {
        audioSourceWallslide.Stop();
    }

}

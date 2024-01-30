// Written by Kevin Catlett.

using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles how and when the player moves to the mouse position after clicking. 
/// </summary>
public class MoveToMousePosition : MonoBehaviour
{
    [Tooltip("The players animator.")]
    [SerializeField] Animator animator;

    [Tooltip("When the players distance to the position he is moving to is this amount, stop the movement.")]
    [SerializeField] float stoppingDistance = .5f;

    [Tooltip("The layer to ignore for the movement raycast.")]
    [SerializeField] LayerMask ignoreLayer;
    
    [Tooltip("The movement speed of the player.")]
    [SerializeField] float speed = 5;
    
    [Tooltip("The camera of the scene.")] 
    [SerializeField] Camera cam;
    
    // The transform on this gameObject.
    Transform myTransform;

    // The position the character should walk to once input occurs.
    Vector2 targetPosition; 

    // The object the player has clicked on.
    GameObject clickedObject;

    // If the player should be moving.
    bool move;

    // The characters horizontal scale changes depending on the scale when start is called.
    float startHorizontalScale;

    // Invoking occurs when the target position has been reached.
    public delegate void OnTargetPositionReached();
    public event OnTargetPositionReached targetPositionReached;

    [SerializeField] ParticleSystem dust;
    float dustTime = .5f;
    float currentDustTimer;
    #region Methods
    
    /// <summary>
    /// Sets the startSpeed variable.
    /// Sets the startHorizontalScale variable.
    /// Gets the transform on this gameObject.
    /// </summary>
    void Start()
    {
        startHorizontalScale = transform.localScale.x;
        myTransform = transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.None;
        currentDustTimer = dustTime;
    }

    /// <summary>
    /// Calls the Movement method.
    /// </summary>
    void Update()
    {
        DoMovement();
        if(move)
        {
            currentDustTimer -= Time.deltaTime;
            if(currentDustTimer <= 0)
            {
                currentDustTimer = dustTime;
                dust.Play();
            }
        }
        else
        {
            currentDustTimer = dustTime;
        }
    }
    
    /// <summary>
    /// Controls the movement of the player.
    /// </summary>
    void DoMovement()
    {
        if (!move)
            return;
        
        DistanceToTarget();
        
        transform.position = Vector2.MoveTowards(myTransform.position, targetPosition, speed * Time.deltaTime); // Continue moving the character.
    }
    
    /// <summary>
    /// Activates movement and sets movement values.
    /// </summary>
    void StartMovement()
    {
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
            move = true;
            Flip();
            animator.Play("Player_Running");
            clickedObject = null;
        }
    }
    
    /// <summary>
    /// Ends movement and resets movement values.
    /// </summary>
    void EndMovement()
    {
        move = false;
        animator.Play("Player_Idle");
        targetPositionReached?.Invoke();
        targetPositionReached = null;
    }
    
    /// <summary>
    /// Gets the distance to the target position and ends movement if the target position has been reached.
    /// </summary>
    void DistanceToTarget()
    {
        float distanceToTarget = Vector2.Distance(transform.position, targetPosition); // Get the distance to the target position.
        
        if (distanceToTarget <= stoppingDistance) //Disable movement if the target position is reached.
            EndMovement();
    }

    /// <summary>
    /// Checks if the position that the mouse is at is a valid position to move to, and triggers movement if so.
    /// </summary>
    void OnMouseClick()
    {
            targetPositionReached = null;
            targetPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            clickedObject = ObjectAtMousePosition();

            EvaluateMovement();      
    }
    
    /// <summary>
    /// Checks for a clicked object and calls the StartMovement method.
    /// </summary>
    void EvaluateMovement()
    {
        if (clickedObject)
        {
            if (clickedObject.CompareTag("PaCNonWalkable"))
            {
                EndMovement();
            }

            if (clickedObject.CompareTag("KeyContainer"))
            {
                targetPositionReached += clickedObject.GetComponent<KeyContainer>().InsertKey;
                StartMovement();
            }
        }
        else
        {
            StartMovement();   
        }
    }
    
    /// <summary>
    /// Flips the character in the direction it is moving in.
    /// </summary>
    void Flip()
    {
        if (targetPosition.x < myTransform.position.x && myTransform.localScale.x > 0)
        {
            //flip to left
            myTransform.localScale = new Vector2(-myTransform.localScale.x, myTransform.localScale.y);
        }
        else if (targetPosition.x > myTransform.position.x && myTransform.localScale.x < 0)
        {
            //flip to right
            myTransform.localScale = new Vector2(startHorizontalScale, myTransform.localScale.y);
        }
    }
    
    /// <summary>
    /// Returns if a object is where the mouse click occured.
    /// </summary>
    /// <returns></returns>
    GameObject ObjectAtMousePosition()
    {
        RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            return hit.transform.gameObject;
        }
        return null;
    }

    /// <summary>
    /// Gets called by the Point and Click Input Action.
    /// </summary>
    public void MouseClick(InputAction.CallbackContext context)
    {
        if(context.performed) OnMouseClick();
    }
    #endregion 

}

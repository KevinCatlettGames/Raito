using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles how and when the player moves to the mouse position after clicking. 
/// </summary>
public class MoveToMousePosition : MonoBehaviour
{
    #region Variables

    MousePosition mousePosition; // A reference to the mouse position singleton.

    Vector2 currentPosition; // The position the player should move to.

    GameObject clickedObject; // The object the player has clicked on.

    float distanceToTarget; // The current distance to the target.

    bool move; // If the player should be moving.

    [SerializeField]
    SpriteRenderer spriteRenderer;

    [SerializeField]
    Animator animator;

    [SerializeField]
    float stoppingDistance = .5f; // When the players distance to the position he is moving to is this amount, stop the movement.

    [SerializeField]
    float speed = 5; // The movement speed of the player.

    #endregion

    #region Start
    // Start is called before the first frame update
    void Start()
    {
        mousePosition = MousePosition.instance;
    }

    #endregion

    #region Update
    // Update is called once per frame
    void Update()
    {
        if (move)
        {
         
            distanceToTarget = Vector3.Distance(transform.position, currentPosition);

            if (distanceToTarget <= stoppingDistance)
            {
                move = false;

                if (clickedObject != null)
                {
                    clickedObject.GetComponent<CollectItem>().Collect();
                }

                animator.SetFloat("Speed", 0);
                return;
            }

            transform.position = Vector2.MoveTowards(transform.position, currentPosition, speed * Time.deltaTime);
        }

    }

    #endregion

    #region Methods
    /// <summary>
    /// Checks if the position that the mouse is at is a valid position to move to, and triggers movement if so.
    /// </summary>
    void EvaluateMoveInformation()
    {      
        move = true;
        clickedObject = mousePosition.ObjectAtMousePosition();
        if (clickedObject != null)
        {
            if (clickedObject.GetComponent<IsNotWalkable>())
            {       
                move = false;
                animator.SetFloat("Speed", 0);
                return;
            }

        }
        Vector2 currentMousePosition = mousePosition.WorldMousePosition();
        currentPosition = currentMousePosition;

        if (transform.position.x < currentPosition.x)
        {
            spriteRenderer.flipX = false;
        }
        else if (transform.position.x > currentPosition.x)
        {
            spriteRenderer.flipX = true;
        }

        animator.SetFloat("Speed", 1);
    }

    #endregion

    #region Input Action call
    /// <summary>
    /// Gets called by the Point and Click Input Action.
    /// </summary>
    public void MouseClick(InputAction.CallbackContext context)
    {
        EvaluateMoveInformation();
    }

    #endregion 

}

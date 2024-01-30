// Written by Nils Ebert. 

using UnityEngine;

public class BuddyFollowsPlayer : MonoBehaviour
{
    private Vector3 normalPosition;

    [Header("Movement Settings")]
    [SerializeField] public float moveSpeed = 0.1f;
    [SerializeField] float maxDistance = 5;
    [Header("Still Position")]
    [SerializeField] public Transform buddySpot;
    [SerializeField] Collider2D buddyCollider;
    private Rigidbody2D rb;
    private Vector2 position;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        position = buddySpot.position;

        if(transform.parent != null)
        {
            transform.parent = null;
        }
    }

    void Update()
    {
        normalPosition.x = buddySpot.transform.position.x;                       // Gets his normal/still x position in pixels.
        normalPosition.y = buddySpot.transform.position.y;                       // Gets his normal/still y position in pixels.
        position = Vector2.Lerp(transform.position, normalPosition, moveSpeed);  // "Lerp" so that it basically always finds a position between its current position and position it should go -> follows slowly 

        if (buddyCollider != null)
        {
            if (Vector2.Distance(transform.position, buddySpot.transform.position) >= maxDistance)
            {
                buddyCollider.enabled = false;
            }
            else if (Vector2.Distance(transform.position, buddySpot.transform.position) < 2)
            {
                buddyCollider.enabled = true;
            }
        }
          
    }


    private void FixedUpdate()
    {
        rb.MovePosition(position);
    }
}

// Written by Nils Ebert.

using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField] private float movingSpeed;
    [SerializeField] private int startingPoint;
    [SerializeField] private Transform[] points;        // An array of transform points (positions where the platform needs to move)
    private int i;

    [SerializeField] private bool playerGetsParented = true;




    void Start()
    {
        transform.position = points[startingPoint].position;
    }



    void Update()
    {

        // Checking the distance of the platform and the point
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {

            i++;
            
            // Check if the platform was on the last point after the index increase
            if (i == points.Length)
            {
                i = 0;  // Reset the index so it moves to the first platform again
            }

        }

        // Moving the platform to the position of the point with the index "i".
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, movingSpeed * Time.deltaTime);

    }



    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (playerGetsParented && collision.collider.CompareTag("Player"))
        {
            if (transform.position.y < collision.transform.position.y)
            {
                collision.transform.SetParent(transform);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.collider.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }

    }

}

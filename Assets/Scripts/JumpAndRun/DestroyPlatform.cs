// Written by Keir Lobo. 

using UnityEngine;

public class DestroyPlatform : MonoBehaviour
{
    
    [SerializeField, Tooltip("Time until the Platform gets destroyed after collision.")] private float destroyDelay;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject, destroyDelay);
        }
    }
}

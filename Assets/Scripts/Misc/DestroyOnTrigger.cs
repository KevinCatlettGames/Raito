// Written by Kevin Catlett.

using UnityEngine;

/// <summary>
/// Destroys this gameObject through the OnTriggerEnter2D method.
/// </summary>
public class DestroyOnTrigger : MonoBehaviour
{
    [Tooltip("The tag that the trigger should compare.")]
    [SerializeField] private string destroyTag;
    
    [Tooltip("The duration until the gameObject is destroyed after the trigger occurs.")]
    [SerializeField] private float destroyWaitTime;

    /// <summary>
    /// If the destroyWaitTime value is zero, change it to dot zero one, else a error occurs. 
    /// </summary>
    void Start()
    {
        if (destroyWaitTime <= 0)
            destroyWaitTime = .01f;
    }

    /// <summary>
    /// When a trigger occurs with the correct tag, destroy this gameObject.
    /// Wait for a duration set by the destroyWaitTime variable.
    /// </summary>
    /// <param name="col"></param> The collider which triggered this method. 
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(destroyTag))
        {
            Debug.Log("Destroy tag triggered");
            Destroy(gameObject, destroyWaitTime);
        }
    }
}

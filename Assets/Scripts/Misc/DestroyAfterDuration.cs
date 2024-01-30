// Written by Kevin Catlett.

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script destroy the gameObject it is attached to after a duration. 
/// </summary>
public class DestroyAfterDuration : MonoBehaviour
{
    [Tooltip("The time until this gameObject is destroyed.")]
    [SerializeField] float duration;
    
    /// <summary>
    /// Calls the destroy method for this gameObject.
    /// Waits for duration length. 
    /// </summary>
    void Start()
    {
        if (duration <= 0)
            duration = .1f;
        
        Destroy(gameObject, duration);
    }
}

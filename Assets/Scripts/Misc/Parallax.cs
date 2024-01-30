// Written by Kevin Catlett.

using System.Collections;
using UnityEngine;

/// <summary>
/// Moves the gameObjects transform which this is attached to in the positive x axis. 
/// </summary>
public class Parallax : MonoBehaviour
{
    [Header("Start/End position and speed")]
    [Tooltip("The horizontal position this gameObject should be moved to when the horizontalEndPosition is reached.")]
    [SerializeField] float horizontalStartPosition;
   
    [Tooltip("The gameObject will be repositioned to the horizontalStartPosition once this value is reached in the x axis.")]
    [SerializeField] float horizontalEndPosition;

    [Tooltip("The speed in which this gameObjects transform should move.")]
    [SerializeField] float speed;

    [Header("Stopping values")]
    [Tooltip("Should the movement stop after a duration.")]
    [SerializeField] bool stopAfterDuration;
    
    [Tooltip("The duration until movement is stopped if stopAfterDuration is true.")]
    [SerializeField] float parallaxDuration;
    
    // Is the gameObject moving. 
    bool move = true;
    
    #region Methods
    /// <summary>
    /// Calls the StopAfterDuration coroutine if the stopAfterDuration boolean is true.
    /// </summary>
    void Start()
    {
        if (stopAfterDuration)
        {
            StartCoroutine((StopAfterDuration()));
        }
    }

    /// <summary>
    /// Calls the Relocate method.
    /// </summary>
    void Update()
    {
        if (move)
        {
            MoveRight();
            Relocate();
        }
    }

    /// <summary>
    /// Moves the transform to the right with corresponding with the speed value. 
    /// </summary>
    void MoveRight()
    {
        transform.Translate(Vector3.right * (Time.deltaTime * speed));
    }

    /// <summary>
    /// Changes this gameObjects position to the horizontalStartPosition once the horizontalEndPosition is reached. 
    /// </summary>
    void Relocate()
    {
        if (transform.position.x >= horizontalEndPosition)
        {
            Vector3 newPosition = new Vector3(horizontalStartPosition, transform.position.y, transform.position.z);
            transform.position = newPosition;
        }
    }

    /// <summary>
    /// Stops movement after a duration. 
    /// </summary>
    /// <returns></returns> Waits for seconds, using the parallaxDuration variables. 
    IEnumerator StopAfterDuration()
    {
        yield return new WaitForSeconds(parallaxDuration);
        move = false;
    }
    #endregion
}

// Written by Kevin Catlett.

using UnityEngine;

/// <summary>
/// Rotates the transform around its own z axis. 
/// </summary>
public class RotateAroundOwnAxis : MonoBehaviour
{
    [Tooltip("The amount in which the transform rotates around its own z axis per frame")]
    [SerializeField] float zAmount;

    #region Methods
    /// <summary>
    /// Calls the Rotate method.
    /// </summary>
    void Update()
    {
        Rotate();
    }

    /// <summary>
    /// Rotates the transform around its own z axis.
    /// </summary>
    void Rotate()
    {
        transform.Rotate(0, 0, zAmount, Space.Self);
    }
    #endregion
}

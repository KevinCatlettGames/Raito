// Written by Kevin Catlett. 

using System;
using UnityEngine;

/// <summary>
/// Changes the texture of the cursor when OnEnable.
/// Resets the texture of the cursor when OnDisable.
/// </summary>
public class CursorHandler : MonoBehaviour
{
    [Tooltip("The Texture2D to change the cursor texture to.")]
    [SerializeField] Texture2D mouseIcon;

    /// <summary>
    /// Changes the cursor texture to the mouseIcon variable.
    /// </summary>
    void OnEnable()
    {
        Cursor.SetCursor(mouseIcon, Vector2.zero, CursorMode.Auto);
    }

    private void Update()
    {
        if (!Cursor.visible)
        {
            Cursor.visible = true;
        }
    }

    /// <summary>
    /// Resets the cursot texture. 
    /// </summary>
    private void OnDisable()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}

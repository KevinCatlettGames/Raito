// Written by Kevin Catlett.

using UnityEngine;

/// <summary>
/// Actives a world space canvas parented to this gameObject if the key that can be collected in this level has not been collected yet.
/// The canvas can show an arrow for giving the player direction in where to go next.
/// </summary>
public class LevelEntranceCanvas : MonoBehaviour
{
    [Tooltip("The gameObject of the canvas that should be activated.")]
    [SerializeField] GameObject canvas;

    [Tooltip("The index of the key that can be collected in these levels.")]
    [SerializeField] int keyIndex;
    
    private KeyDataHandler keyDataHandler;
    
    /// <summary>
    /// Sets the keyDataHandler singleton.
    /// Calls the activate canvas method.
    /// </summary>
    void Start()
    {
        if(KeyDataHandler.instance)
           keyDataHandler = KeyDataHandler.instance;

        ActivateCanvas();
    }

    /// <summary>
    /// Activates the gameObject of the canvas.
    /// </summary>
    void ActivateCanvas()
    {
        if (!keyDataHandler.KeyCollected[keyIndex])
            canvas.SetActive(true);
    }
}

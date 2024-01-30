// Written by Kevin Catlett.

using UnityEngine;

/// <summary>
/// Scales a gameobject up or down depending on how far it is from the center of the screen in the y axis.
/// </summary>
public class ScaleTransformWithScreenPosition : MonoBehaviour
{
    [Tooltip("The amount the x axis is scaled.")]
    [SerializeField] float xScaleFactor;
    
    [Tooltip("The amount the y axis is scaled.")]
    [SerializeField] float yScaleFactor;
    
    // The original scale of the x axis.
    float originalXScale;
    
    // The original scale of the y axis.
    float originalYScale;

    #region Methods
    /// <summary>
    /// Sets the originalXScale and originalYScale variables.
    /// </summary>
    void Start()
    {
        Vector2 localScale = transform.localScale;
        originalXScale = localScale.x;
        originalYScale = localScale.y;
    }
    
    /// <summary>
    /// Changes the scale of the transform after Update.
    /// </summary>
    void LateUpdate()
    {
        ChangeScale();
    }
    
    /// <summary>
    /// Scales the gameobject this is attached to. Takes the current x localScale into consideration to determine scaling direction. 
    /// </summary>
    void ChangeScale()
    {
        if (transform.localScale.x > 0) // Should the gameobject be scaled in the positiv direction.
        {
            float xScaling = ScaleFactor(originalXScale, xScaleFactor);
            float yScaling = ScaleFactor(originalYScale, yScaleFactor);

            // scale positiv way         
            transform.localScale = new Vector2(xScaling, yScaling);
        }

        else if (transform.localScale.x < 0) // Should the gameobject be scaled in the negativ direction.
        {
            float xScaling = ScaleFactor(originalXScale, xScaleFactor);
            float yScaling = ScaleFactor(originalYScale, yScaleFactor);

            // scale negativ way
            transform.localScale = new Vector2(-xScaling, yScaling);
        }
    }

    /// <summary>
    /// Calculates how much the local scale should be changed per frame.
    /// </summary>
    /// <param name="originalScale"></param> Determines the scale when the gameobject is in the zero point of the y axis.
    /// <param name="scaleFactor"></param> How much the local scale should change. 
    /// <returns></returns> A float value representing the scale amount for this frame.
    float ScaleFactor(float originalScale, float scaleFactor)
    {
       return originalScale - (transform.position.y / scaleFactor);
    }

    #endregion
}

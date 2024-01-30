// Written by Kevin Catlett.

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Iterates through the collected keys and makes the equivalent button interactable. 
/// </summary>
public class LevelSelectionUnlock : MonoBehaviour
{
    [Tooltip("The level selection buttons in order from left to right.")]
    [SerializeField] Button[] levelButtons;
    
    // The collected keys saved in a array.
    bool[] keycollected;

    #region Methods
    /// <summary>
    /// Gets a reference to the keysCollected boolean array on the keyDataHandler singleton.
    /// Calls the ButtonInteractable method. 
    /// </summary>
    void Start()
    {
        if (KeyDataHandler.instance)
        {
            keycollected = KeyDataHandler.instance.KeyCollected;

           ButtonInteractable();
        }
    }

    /// <summary>
    /// Makes buttons interactable depending on if the keys have been collected or not.
    /// </summary>
    void ButtonInteractable()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (keycollected[i])
            {
                levelButtons[i].interactable = true;
            }
        }
    }
    #endregion
}

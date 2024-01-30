// Written by Kevin Catlett.

using UnityEngine;

/// <summary>
/// Activates the level exit once all key parts in the level have been collected.
/// </summary>
public class LevelKeyCollecting : MonoBehaviour
{
    [Header("Key collecting")]
    [Tooltip("The amount of key parts to collect in this level. There can also be 0 key parts in a level.")]
    [SerializeField] int totalKeyParts;

    [Tooltip("Should this level give a key.")]
    [SerializeField] bool collectKeyInScene;
    
    [Tooltip("If a key can be collected, under what key index should it be saved as collected.")]
    [SerializeField] int keySlotIndex;
    
    [Header("Level exit")]
    [Tooltip("The exit gameobject of the level. Will be activated when all key parts are collected.")]
    [SerializeField] GameObject levelExit;

    // Increments each time a new key parts was collected.
    int keyPartsCollectedAmount = 0; 

    // Singleton which stores the key information.
    KeyDataHandler keyDataHandler;

    #region Methods
    /// <summary>
    /// Gets a reference to the keyDataHandler singleton.
    /// </summary>
    void Start()
    {
        if (KeyDataHandler.instance)
            keyDataHandler = KeyDataHandler.instance;
    }

    /// <summary>
    /// Add a key part to the collected key parts.
    /// Save a finished key if all parts have been collected.
    /// </summary>
    public void CollectKeyPart()
    {
        keyPartsCollectedAmount++;
        bool allPartsCollected = AllPartsCollected();
        
        if(allPartsCollected)
        {
            if(collectKeyInScene)
            {
                keyDataHandler.CollectKey(keySlotIndex); // Change the key information to be collected.
            }
            levelExit.SetActive(true);
        }
    }

    /// <summary>
    /// Checks if a key index should be marked as collected on the key data handler.
    /// </summary>
    /// <returns></returns>
    bool AllPartsCollected()
    {
        if (keyPartsCollectedAmount >= totalKeyParts) // Are key parts collected.
        {
            if (keyDataHandler) // Should a key be saved.
            {             
                return true;
            }       
        }
        return false;
    }
    #endregion
}

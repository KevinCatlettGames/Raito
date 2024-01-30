// Written by Kevin Catlett.

using UnityEngine;

/// <summary>
/// Moves the player gameObject to the right position when the SetInitialPlayerPosition method is called.
/// This script is used to show the player which levels the player has just played
/// and moves the player to the entrance of that level. 
/// </summary>
public class PointAndClickSpawnHandler : MonoBehaviour
{
    // Singleton referencing this instance. 
    public static PointAndClickSpawnHandler instance;

    [Tooltip("The player gameObject that will be moved.")]
    [SerializeField] GameObject player;
    
    [Tooltip("The possible transforms the player will be moved to in order of the level entrances.")]
    [SerializeField] Transform[] spawnTransforms;
    
    /// <summary>
    /// Sets the singleton and destroys this gameObject if a singleton has already been set. 
    /// </summary>
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
          instance = this; 
        }
    }
    
    /// <summary>
    /// When called the player is positioned to the corresponding spawnTransform.
    /// The spawnIndex parameter decides, which index is used inside of the spawnTransforms array. 
    /// </summary>
    /// <param name="spawnIndex"></param> The index of the transform where the player should be spawned to.
    public void SetInitialPlayerPosition(int spawnIndex)
    {
        player.transform.position = spawnTransforms[spawnIndex].position; // Move the player to the spawn transform. 
    }
}

// Written by Kevin Catlett.
// Audio implementation written by Nils Ebert.
using UnityEngine;

/// <summary>
/// Collects a key part inside of the JumpAndRun levels and handles the logic when triggered by the player.
/// </summary>
public class KeyTrigger : MonoBehaviour
{
    [Header("Level ending script")]
    [Tooltip("The level ending script responsible for activating the level exit.")]
    [SerializeField] LevelKeyCollecting keyPartCounter;
    
    [Header("Audio")]
    [Tooltip("The audioSource responsible for soundFX when the key has been collected.")]
    [SerializeField] AudioSource audioSource;
    
    [Tooltip("The audioClip played when the key has been collected.")]
    [SerializeField] AudioClip audioClip;

    #region Methods
    /// <summary>
    /// Trigger on player collider2D.
    /// </summary>
    /// <param name="collision"></param> The collider2D triggering this method.
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
           OnPlayerTrigger();
        }
    }

    /// <summary>
    /// Adds a key part to the total collected key parts.
    /// Plays a audioClip and destroys this gameObject.
    /// </summary>
    void OnPlayerTrigger()
    {
        keyPartCounter.CollectKeyPart();
        PlayKeyAudio();
        Destroy(this.gameObject);
    }
    
    /// <summary>
    /// Plays the audioClip on the audioSource.
    /// </summary>
    void PlayKeyAudio()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
    #endregion 
}

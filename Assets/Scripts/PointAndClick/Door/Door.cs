// Written by Kevin Catlett.

using System.Collections;
using UnityEngine;

/// <summary>
/// Point and Click scene specific.
/// This class opens the door once all keys have been inserted into their slots and handles the effects.
/// </summary>
public class Door : MonoBehaviour
{
    [Header("Exit activation")]
    [Tooltip("For activating the exit trigger once all keys have been inserted.")]
    [SerializeField] GameObject gameExit;

    [Header("Wall colliders")]
    [Tooltip("The gameObject holding the wall collider when the door is closed.")]
    [SerializeField] GameObject doorClosedWallCollision;
    
    [Tooltip("The gameObject holding the wall collider when the door is open.")]
    [SerializeField] GameObject doorOpenWallCollision;
    
    [Header("")]
    [Tooltip("For activating the particle systems when the door opens.")]
    [SerializeField] GameObject openingParticles;

    [Tooltip("The boolean parameter inside of the animator to open the door.")]
    [SerializeField] string doorOpeningBoolean;
    
    [Header("Door opening camera shake.")]

    [Tooltip("The amplitude of the cineMachine camera shake for this specific shake.")]
    [SerializeField] float shakeAmplitude;
    
    [Tooltip("The frequency of the cineMachine camera shake for this specific shake.")]
    [SerializeField] float shakeFrequency;
    
    [Tooltip("The duration of the camera shake.")]
    [SerializeField] float shakeTime;
    
    // Singleton for subscribing to the onKeysPlaced event. 
    KeyDataHandler keyDataHandler; 
    
    // The animator on this gameObject to trigger the door opening animation.
    Animator animator; 
    
    // The audioSource on this gameObject to trigger the door opening soundFX.
    AudioSource audioSource;

    #region Methods
    /// <summary>
    /// Subscribes to the allKeysPlaced event in the keyDataHandler singleton.
    /// Gets references to the animator and audioSource on this gameObject. 
    /// </summary>
    void Awake()
    {
        if (KeyDataHandler.instance)
        {
            keyDataHandler = KeyDataHandler.instance;
            keyDataHandler.allKeysPlaced += UnlockDoor;
        }
        
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Activates the gameObject holding the exit trigger and the effects when the door opens. 
    /// </summary>
    void UnlockDoor()
    {
        gameExit.SetActive((true));
        
        doorClosedWallCollision.SetActive(false);
        doorOpenWallCollision.SetActive(true);
        
        OpeningEffects();
    }

    /// <summary>
    /// Activate the effects that play when the door is opening.
    /// Disable the particles after the camera shake has ended. 
    /// </summary>
    void OpeningEffects()
    {
        animator.SetBool(doorOpeningBoolean, true); // animation
        audioSource.PlayOneShot(audioSource.clip); // soundFX
        openingParticles.SetActive(true); // particles
        CameraShaker.instance.DoCameraShake(shakeAmplitude, shakeFrequency, shakeTime);
        
        StartCoroutine((DisableParticles())); // Disable particles after effect.
    }
    
    /// <summary>
    /// Unsubscribe to the allKeysPlaced event on disable. 
    /// </summary>
    void OnDisable()
    {
        if (keyDataHandler)
        {
            if(KeyDataHandler.instance)
               KeyDataHandler.instance.allKeysPlaced -= UnlockDoor;
        }
    }
    
    /// <summary>
    /// Coroutine for disabling the particle effect after the camera shake has ended.
    /// </summary>
    /// <returns></returns> Continues this coroutine after the camera shake has ended.
    IEnumerator DisableParticles()
    {
        yield return new WaitForSeconds(shakeTime);
        openingParticles.SetActive((false));
    }
    #endregion
}

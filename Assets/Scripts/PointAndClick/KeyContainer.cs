// Written by Kevin Catlett.
// Audio implementation written by Nils Ebert. 

using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

/// <summary>
/// Point and Click scene specific.
/// Changes the state of the key containers depending on if the corresponding key is collected or inserted. 
/// </summary>
public class KeyContainer : MonoBehaviour
{
    [Header("Key values")]
    [Tooltip("The index of the key for this container.")]
    [SerializeField] int keyIndex;

    [Tooltip("The key gameObject which plays a inserting animation at the key container.")]
    [SerializeField] GameObject insertedKey;

    [Header("Audio")]
    [Tooltip("Audio source for the audio clip.")]
    [SerializeField] AudioSource audioSource;
    
    [Tooltip("Audio clip to be played when the key is inserted.")]
    [SerializeField] AudioClip audioClip;

    [Tooltip("A gameObject holding a world space canvas with a image hovering above the container")] 
    [SerializeField] GameObject containerCanvas;

    [Tooltip("A gameObject holding a sprite on the door to active when the key has been collected.")] 
    [SerializeField] GameObject doorLight;

    [Tooltip("The time until the door light is activated after inserting the key.")]
    [SerializeField] float doorLightActivationWaitTime;
    
    // To enable the light if the correct key has been collected.
    Light2D containerLight;
    
    // Singleton for getting the key information.
    KeyDataHandler keyDataHandler;
    
    // Singleton used to save the saving and loading key information.
    SaveAndLoad saveSystem; 
    
    #region Methods
    /// <summary>
    /// Gets references to the keyDataHandler and saveSystem singleton.
    /// Gets a reference to the light2D on this gameObject.
    /// Handles light and effect activation depending on the corresponding keys state in the keyDataHandler. 
    /// </summary>
    void Start()
    {
        if (KeyDataHandler.instance)
        {
            keyDataHandler = KeyDataHandler.instance;
        }

        if (SaveAndLoad.instance)
        {
            saveSystem = SaveAndLoad.instance;
        }
        
        containerLight = GetComponent<Light2D>();
        CanvasActivation();
        InsertingEffect();
      
    }

    /// <summary>
    /// Activates a gameObject holding a world space canvas if the corresponding key has been collected but not placed.
    /// </summary>
    void CanvasActivation()
    {
        if (keyDataHandler.KeyCollected[keyIndex] && !keyDataHandler.KeyPlaced[keyIndex])
        {
          containerCanvas.SetActive(true);
        }
    }

    /// <summary>
    /// Activates the key inserting effect if the corresponding key has been placed.
    /// </summary>
    void InsertingEffect()
    {
        if (keyDataHandler.KeyPlaced[keyIndex])
        {
            insertedKey.SetActive(true);
            containerLight.enabled = true;
            StartCoroutine(ActivateDoorLightAfterDuration());

            if(saveSystem)
               saveSystem.Save();
        }
    }
    
    /// <summary>
    /// Plays a sound when the key is inserted.
    /// </summary>
    void PlayKeyInsertedSound()
    {
        if (audioSource)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
    
    /// <summary>
    /// Activates and enables everything to show a key inserting effect and toggles that the key has been placed.
    /// Calls the save method on the saveSystem singleton.
    /// </summary>
    public void InsertKey()
    {
        if (keyDataHandler.KeyCollected[keyIndex] && !keyDataHandler.KeyPlaced[keyIndex])
        {
            containerLight.enabled = true;
            containerCanvas.SetActive(false);
            StartCoroutine(ActivateDoorLightAfterDuration());
            
            PlayKeyInsertedSound();
            
            insertedKey.SetActive(true);
            keyDataHandler.PlaceKey(keyIndex);
          
            if(saveSystem)
               saveSystem.Save();
        }
    }
    
    /// <summary>
    /// Activates the door late after wait time.
    /// </summary>
    /// <returns></returns> Continues the coroutine after the duration of doorLightActivationWaitTime.
    IEnumerator ActivateDoorLightAfterDuration()
    {
        yield return new WaitForSeconds(doorLightActivationWaitTime);
        doorLight.SetActive(true);
    }

    #endregion
}

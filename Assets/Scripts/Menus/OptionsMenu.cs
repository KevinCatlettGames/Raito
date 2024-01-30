// Written by Kevin Catlett.

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

/// <summary>
/// Functionality for the options menu.
/// Functionality for audio management.
/// </summary>
public class OptionsMenu : MonoBehaviour
{
    [Header("Audio mixer")]
    [Tooltip("The audio mixer for managing sound volumes.")]
    [SerializeField] AudioMixer audioMixer;
    
    [Header("Volume sliders")]
    [Tooltip("The global volume slider.")]
    [SerializeField] Slider globalVolumeSlider;
    
    [Tooltip("The music volume slider.")]
    [SerializeField] Slider musicVolumeSlider;
    
    [Tooltip("The sound volume slider.")]
    [SerializeField] Slider soundVolumeSlider;
    
    // Save and load singleton.
    SaveAndLoad saveAndLoad;
    
    // Sound data handler singleton.
    SoundDataHandler soundDataHandler;

    // Constant sound mixer group names. 
    const string Master = "MasterVolume";
    const string Music = "MusicVolume";
    const string Sound = "SoundVolume";

    #region Methods
    /// <summary>
    /// Getting references to singletons.
    /// Calls the sliderInitializing method.
    /// </summary>
    void Awake()
    {
        if (SoundDataHandler.instance)
            soundDataHandler = SoundDataHandler.instance;

        if(SaveAndLoad.instance) 
            saveAndLoad = SaveAndLoad.instance;
        
        SliderInitializing();
    }
    
    /// <summary>
    /// Updates the slider values to represent the volumes of the sound mixer groups.
    /// Sets the onValueChanged events on the volume sliders to update the values of the soundDataHandler.
    /// </summary>
    void SliderInitializing()
    {
        if (!soundDataHandler)
        {
            return;
        }

        globalVolumeSlider.value = soundDataHandler.GlobalVolume;
        musicVolumeSlider.value = soundDataHandler.MusicVolume;
        soundVolumeSlider.value = soundDataHandler.SoundVolume;

        globalVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        soundVolumeSlider.onValueChanged.AddListener(SetSoundVolume);
        
    }
    
    /// <summary>
    /// Loads the scene which has the buildIndex parameter as index in the scene manager.
    /// </summary>
    /// <param name="buildIndex"></param> The index of the scene that should be loaded.
    public void OpenScene(int buildIndex)
    {
        saveAndLoad.Save();
        SceneManager.LoadScene(buildIndex);
    }
    
    /// <summary>
    /// Sets the master volume in the audioMixer and soundDataHandler.
    /// </summary>
    /// <param name="value"></param> The value that should be used.
    public void SetMasterVolume(float value)
    {
        SetVolume(value, Master);
    }
    
    /// <summary>
    /// Sets the music volume in the audioMixer and soundDataHandler.
    /// </summary>
    /// <param name="value"></param> The value that should be used.
    public void SetMusicVolume(float value)
    {
        SetVolume(value,Music);
    }
  
    /// <summary>
    /// Sets the sound volume in the audioMixer and soundDataHandler.
    /// </summary>
    /// <param name="value"></param> The value that should be used.
    public void SetSoundVolume(float value)
    {
       SetVolume(value,Sound);
    }

    /// <summary>
    /// Sets the audioMixer value in a audioMixer group and updates the soundDataHandler.
    /// </summary>
    /// <param name="value"></param> The value that should be used.
    /// <param name="mixerGroup"></param> The mixer group where the value should be updated in. 
    void SetVolume(float value, string mixerGroup)
    {
        float tempValue = Mathf.Log10(value) * 20; // Changes the value parameter to function with the audioMixer.
        
        if (value == 0)
            audioMixer.SetFloat(mixerGroup, -80);
        else
            audioMixer.SetFloat(mixerGroup, tempValue);
        
        switch (mixerGroup)
        {
            case Master:
                soundDataHandler.GlobalVolume = value;
                break;
            case Music:
                soundDataHandler.MusicVolume = value;
                break;
            case Sound:
                soundDataHandler.SoundVolume = value;
                break;
        }
    }
    #endregion 
}

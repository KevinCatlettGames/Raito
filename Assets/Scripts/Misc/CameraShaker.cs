// Written by Kevin Catlett.

using System.Collections;
using UnityEngine;
using Cinemachine;

/// <summary>
/// Handles camera shaking on a virtual camera with a BasicMultiChannelPerlin component.
/// </summary>
public class CameraShaker : MonoBehaviour
{
    public static CameraShaker instance; 

    [Tooltip("The virtucal camera the camera shake should happen on.")]
    [SerializeField] CinemachineVirtualCamera vcam;
    
    // The MultiChannelPerlin component on the virtual camera.
    CinemachineBasicMultiChannelPerlin noise;
    
    #region Methods
    /// <summary>
    /// Gets a reference to the BasicMultiChannelPerlin component on the virtual camera.
    /// </summary>
    void Awake()
    {
        if(CameraShaker.instance == null)
        {
            CameraShaker.instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        if(vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>())
           noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    
    /// <summary>
    /// Sets the noise amplitude and frequency values, initializing the camera shake.
    /// </summary>
    /// <param name="amplitudeGain"></param> Gain to apply to the amplitude.
    /// <param name="frequencyGain"></param> Factor to apply to the frequency.
    void Noise(float amplitudeGain, float frequencyGain)
    {
        noise.m_AmplitudeGain = amplitudeGain;
        noise.m_FrequencyGain = frequencyGain;
    }

    /// <summary>
    /// Initializes the camera shake by calling the Noise method.
    /// Deactivates the camera shake by calling the EndShakeAfterDuration coroutine.
    /// </summary>
    /// <param name="amplitudeGain"></param> Gain to apply to the amplitude.
    /// <param name="frequencyGain"></param> Factor to apply to the frequency.
    /// <param name="shakeTime"></param> The duration until the noise values return to zero.
    public void DoCameraShake(float amplitudeGain, float frequencyGain, float shakeTime)
    {
        Noise(amplitudeGain, frequencyGain);
        StartCoroutine(EndShakeAfterDuration(shakeTime));
    }

    /// <summary>
    /// Ends the camera shake. 
    /// </summary>
    /// <param name="shakeTime"></param> The duration of the camera shake. 
    /// <returns></returns> Ends the camera shake after returning, based on the shakeTime parameter value.
    IEnumerator EndShakeAfterDuration(float shakeTime)
    {
        yield return new WaitForSeconds(shakeTime);
        Noise(0,0);
    }
    #endregion
}
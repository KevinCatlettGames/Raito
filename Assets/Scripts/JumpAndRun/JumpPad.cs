// Written by Nils Ebert. 

using System;
using UnityEngine;

public class JumpPad : MonoBehaviour
{

    [Header("Boost Power")]
    [Range(1, 40)][SerializeField] public int Power = 16;

    [Header("Player")]
    [SerializeField] private PlayerMovement player;

    [Header("Sound")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    [Header("Camera Shake")]
    [SerializeField] float shakeFrequency;
    [SerializeField] float shakeAmplitude;
    [SerializeField] float shakeTime;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CameraShaker.instance.DoCameraShake(shakeAmplitude, shakeFrequency, shakeTime);
            PlayBoostUpAudio();
            player.BoostUp(Power);
        }
    }

    private void PlayBoostUpAudio()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }

}

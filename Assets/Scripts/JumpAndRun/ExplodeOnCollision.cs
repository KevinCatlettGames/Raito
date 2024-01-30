// Written by Keir Lobo.
// Particle System and Sound implemented by Nils Ebert.

using System.Collections;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(Explodable))]

public class ExplodeOnCollision : MonoBehaviour
{
    private Explodable explodable;
    [SerializeField] private float delayTime;

    [Header("Sound")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    [Header("Particle System")]
    [SerializeField] ParticleSystem dust;

    [Header("Camera Shake")]
    [SerializeField] float shakeFrequency;
    [SerializeField] float shakeAmplitude;
    [SerializeField] float shakeTime;
    
    void Start()
    {
        explodable = GetComponent<Explodable>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Explode());
        }
    }

    private IEnumerator Explode()
    {
        dust.Play();
        yield return new WaitForSeconds(delayTime);
        CameraShaker.instance.DoCameraShake(shakeAmplitude, shakeFrequency, shakeTime);
        dust.Stop();
        PlayCrackAudio();
        
        explodable.explode();
        ExplosionForce ef = GameObject.FindObjectOfType<ExplosionForce>();
        ef.doExplosion(transform.position);
    }

    private void PlayCrackAudio()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
}

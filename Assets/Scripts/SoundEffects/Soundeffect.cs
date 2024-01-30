// Written by Nils Ebert.

using UnityEngine;

public class Soundeffect : MonoBehaviour
{

    [SerializeField] private AudioSource step;
    [SerializeField] private AudioSource dash;
    [SerializeField] private AudioSource jump;
    [SerializeField] private AudioSource death;

    public void PlayStepSound()
    {
        step.Play();
    }

    public void PlayDashSound()
    {
        dash.Play();
    }

    public void PlayJumpSound()
    {
        jump.Play();
    }
    
    public void PlayDeathSound()
    {
        death.Play();
    }

}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuCharMovement : MonoBehaviour
{
    public Animator anim;
    public string animationName;

    public float walkTime;
    public float walkSpeed;
    bool walk = true;
    public AudioSource audioS;

    private void Start()
    {
        StartCoroutine(StopWalk());
    }

    public void Update()
    {
        if(walk)
        {
            transform.Translate(Vector2.left * walkSpeed * Time.deltaTime);
        }
    }

    IEnumerator StopWalk()
    {
        yield return new WaitForSeconds(walkTime);
        walk = false;
        anim.Play(animationName);
    }

    public void PlayStepSound()
    {
        audioS.Play();
    }
}

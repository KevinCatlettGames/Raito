// Written by Kevin Catlett. 
// Animation implementation written by Keir Lobo. 

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Saves the position of the gameObject in Awake and moves it to the position once a trigger occurs.
/// </summary>
public class RespawnToStartPosition : MonoBehaviour
{
    [Tooltip("Time until the respawn occurs after triggering.")]
    [SerializeField] float timeUntilRespawn = .5f;
    
    [Tooltip("The tag which should be compared to when a trigger occurs.")]
    [SerializeField] string triggerTag;

    [Tooltip("The animator on the player.")]
    [SerializeField] Animator animator;
    
    [Tooltip("The movement script on the player.")]
    [SerializeField] PlayerMovement playerMovement;
    
    // Has the trigger occured and waiting to respawn. 
    bool respawning;

    public bool Respawning { get { return respawning; }  }
    #region Methods
    /// <summary>
    /// Calls the Respawn coroutine once a trigger occurs with a gameObject with the triggerTag as tag. 
    /// </summary>
    /// <param name="collision"></param> The collider which triggered the OnTriggerEnter2D method. 
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(triggerTag) && !respawning)
        {
            StartCoroutine(Respawn());
        }
    }

    /// <summary>
    /// Waits for a duration and moves the transform afterwards.
    /// </summary>
    /// <returns></returns> Waits for seconds represented by the timeUntilRespawn variable. 
    IEnumerator Respawn()
    {
        animator.Play("Player_Die");
        playerMovement.enabled = false;

        respawning = true;
        yield return new WaitForSeconds(timeUntilRespawn);

        respawning = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion
}

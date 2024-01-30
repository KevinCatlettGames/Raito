// Written by Kevin Catlett.

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Loads the credits scene when a trigger occurs.
/// </summary>
public class LoadEndScene : MonoBehaviour
{
    [Tooltip("The index of the credits scene.")]
    [SerializeField] int endSceneIndex;
    
    [Header("Transition values")]
    [Tooltip("The animator of the transition gameObject.")]
    [SerializeField] Animator transitionAnim;
    
    [Tooltip("The time until the credits scene is loaded.")]
    [SerializeField] float transitionTime;
    
    [Tooltip("The name of the trigger parameter on the animator which should be called when a trigger occurs.")]
    [SerializeField] string transitionTrigger;
    
    #region Methods
    /// <summary>
    /// Activates the transition animation.
    /// Calls the LoadLevel coroutine. 
    /// </summary>
    /// <param name="col"></param> The collider which caused the trigger.
    void OnTriggerEnter2D(Collider2D col)
    {
        transitionAnim.SetTrigger((transitionTrigger));
        StartCoroutine(LoadLevel());
    }

    /// <summary>
    /// Waits for the duration of the transitionTime, then loads the scene with the endSceneIndex as build index. 
    /// </summary>
    /// <returns></returns> WaitForSeconds using the transitionTime variable. 
    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(endSceneIndex);
    }
    #endregion
}

// Written by Kevin Catlett.

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// When trigger enter gets called on a correctly tagged gameobject, load a scene.
/// </summary>
public class LoadSceneOnTrigger : MonoBehaviour
{
    [Tooltip("The scene to load.")]
    [SerializeField] int NextSceneIndex; // The scene to load.

    [Tooltip("The tag to check for a trigger.")]
    [SerializeField] string triggerTag;

    [Tooltip("The Animator of the transition between two scenes.")]
    [SerializeField] Animator transition;

    [Tooltip("The time it takes to load the new scene.")]
    [SerializeField] float transitionTime;

    SaveAndLoad saveSystem; // Singleton used to save the levels information. 

    void Start()
    {
        if(SaveAndLoad.instance)
        saveSystem = SaveAndLoad.instance;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(triggerTag))
        {
            SaveAndLoadNextScene();
        }
    }

    void SaveAndLoadNextScene()
    {
        if (saveSystem)
        {
            saveSystem.Save();
        }

        StartCoroutine(LoadLevel(NextSceneIndex));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

}

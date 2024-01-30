// Written by Kevin Catlett.

using UnityEngine;
using UnityEngine.SceneManagement;

// Provides a method for loading a scene using the scenes buildIndex entailed in the SceneManager.
public class SceneLoading : MonoBehaviour
{
    /// <summary>
    /// Loads a scene from the scene manager.
    /// </summary>
    /// <param name="buildIndex"></param> The build index of the scene which should be loaded.
    public void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
}

// Written by Kevin Catlett.

using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Functionality for the credits menu.
/// </summary>
public class CreditsMenu : MonoBehaviour
{
    /// <summary>
    /// Opens a scene corresponding to the buildIndex parameter.
    /// </summary>
    /// <param name="buildIndex"></param> The index of the scene that should be loaded.
    public void OpenScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
}

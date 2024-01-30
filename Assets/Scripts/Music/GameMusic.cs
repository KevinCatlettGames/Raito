// Written by Kevin Catlett. 

using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Music manager base class. Destroys itself once the scene changes to a scene where it should not exist.
/// </summary>
public class GameMusic : MonoBehaviour
{
    [Tooltip("Destroy this gameobject if one of these scenes get loaded.")]
    [SerializeField] string[] scenesWhereDestroy;

    #region Methods
    /// <summary>
    /// Makes this gameObject not destroy itself on scene change.
    /// </summary>
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Subscribe to the activeSceneChanged event of the SceneManager.
    /// </summary>
    void Start()
    {
        SceneManager.activeSceneChanged += OnSceneLoaded;
    }

    /// <summary>
    /// Unsubscribes to the activeSceneChanged event of the SceneManager.
    /// </summary>
    void OnDisable()
    {
        SceneManager.activeSceneChanged -= OnSceneLoaded;
    }
    
    /// <summary>
    /// Subscribed to the activeSceneChanged event on the SceneManager.
    /// Destroys this gameObject if it should not be in the newly loaded scene.
    /// </summary>
    /// <param name="oldScene"></param> The scene which this gameObject has come from.
    /// <param name="newScene"></param> The scene which this gameObject has just entered.
    void OnSceneLoaded(Scene oldScene, Scene newScene)
    {
        foreach (string o in scenesWhereDestroy) // Check if this scene is a scene where this should be destroyed in. 
        {
            if (newScene.name == o) // If this should be destroyed.
            {
                SceneManager.activeSceneChanged -= OnSceneLoaded; // Unsubscribe!
                Destroy(gameObject); 
            }
        }
    }
    #endregion


}

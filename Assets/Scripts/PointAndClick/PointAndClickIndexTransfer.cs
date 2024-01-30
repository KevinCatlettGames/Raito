// Written by Kevin Catlett. 

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// A gameObject with this script is placed in every JumpAndRun scene.
/// This gameObject is transferred to the newly loaded scene.
/// If it is the point and click scene, the players position is changed to represent coming out of a level entrance.
/// If it is not the point and click scene, this gameObject is destroyed. 
/// </summary>
public class PointAndClickIndexTransfer : MonoBehaviour
{
    public static PointAndClickIndexTransfer instance;
    
    [Tooltip("The level collection where this gameObject came from, represented as int.")]
    [SerializeField] int indexToSpawnPlayer;

    [Tooltip("The build index of the point and click scene.")]
    [SerializeField] int pointAndClickSceneIndex;

    #region Methods
    /// <summary>
    /// Makes this object not get destroyed on scene change.
    /// Subscribes to the activeSceneChanged event on the scene manager.
    /// </summary>
    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += InitializePointAndClickPlayerPosition;
    }
    
    /// <summary>
    /// Gets called when the activeSceneChanged event is invoked.
    /// Unsubscribes to the activeSceneChanged event.
    /// Gets destroyed.
    /// If the new scene is the point and click scene, call the SetInitialPlayerPosition method on the SpawnHandler singleton.
    /// Pass the indexToSpawnPlayer value to the SetInitialPlayerPosition value for correct player repositioning. 
    /// </summary>
    /// <param name="currentScene"></param> The scene currently loaded.
    /// <param name="nextScene"></param> The next scene which will be loaded.
    public void InitializePointAndClickPlayerPosition(Scene currentScene, Scene nextScene)
    {
        if  (nextScene.buildIndex == pointAndClickSceneIndex) // In point and click scene.
        {
            if (PointAndClickSpawnHandler.instance)
            {
                PointAndClickSpawnHandler.instance.SetInitialPlayerPosition(indexToSpawnPlayer); // Reposition the player to show which level was loaded.
            }
        }
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= InitializePointAndClickPlayerPosition; // Unsubscribe!
    }

    #endregion
}

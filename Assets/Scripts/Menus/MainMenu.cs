// Written by Kevin Catlett.

using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Entails functionality for the Main Menu.
/// Changes the start game button onClick event depending on the values in the keyDataHandler singleton.
/// </summary>
public class MainMenu : MonoBehaviour
{
    [Tooltip("For changing the onclick event of the button depending on if a key has been collected in the last play session.")]
    [SerializeField] Button StartGameButton;

    [Tooltip("For changing the start game button text depending on if a key has been collected in the last play session.")] 
    [SerializeField] TextMeshProUGUI startGameText;

    [Tooltip("The index of the first level.")]
    [SerializeField] int firstLevelBuildIndex;

    [Tooltip("The index of the level selection scene.")]
    [SerializeField] int levelSelectionBuildIndex; 

    // Singleton used for storing the key states.
    KeyDataHandler keyCollection;

    // UnityAction for loading the saveData when the game begins.
    UnityAction OnStartGame; 
    
    #region Methods
    /// <summary>
    /// Activates cursor visibility.
    /// Gets a reference to the keyDataHandler singleton
    /// Sets the start game button values.
    /// Subscribes to the OnStartGame event.
    /// </summary>
    void Start()
    {
        Cursor.visible = true;

        if (KeyDataHandler.instance)
        {
            keyCollection = KeyDataHandler.instance;
            StartButtonSetting();
        }
        else
        {
            OnStartGame += HandleStartWithoutLevelSelection;
        }

        StartGameButton.onClick.AddListener(OnStartGame); // Set onClick event to OnStartGame Unity Action
    }

    /// <summary>
    /// Checks if a key has been collected and changes the OnClick event of the button to either open the first level
    /// or the level selection. 
    /// </summary>
    void StartButtonSetting()
    {
        bool anyKeyCollected = false;
        
        foreach (bool b in keyCollection.KeyCollected) // Check if a key has been collected.
        {
            if (b)
            {
                startGameText.text = "Level Selection";
                anyKeyCollected = true; // A key has been collected.
            }
        }
         
        if(anyKeyCollected)
            OnStartGame += HandleStartWithLevelSelection; // Change onClick event to open level selection. 
        else
            OnStartGame += HandleStartWithoutLevelSelection; // Change onClick event to open first level.
    }
    
    /// <summary>
    /// Calls the OpenMenuScene method to load the first level.
    /// </summary>
    void HandleStartWithoutLevelSelection()
    {
        OpenMenuScene(firstLevelBuildIndex);
    }

    /// <summary>
    /// Calls the OpenMenuScene method to load the level selection. 
    /// </summary>
    void HandleStartWithLevelSelection()
    {
        OpenMenuScene(levelSelectionBuildIndex);
    }

    /// <summary>
    /// Loads the scene which has the buildIndex parameter as index in the scene manager.
    /// </summary>
    /// <param name="buildIndex"></param> The index of the scene that should be loaded.
    public void OpenMenuScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
    
    /// <summary>
    /// Quits the application.
    /// Exits play mode if called from within the editor.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR

        if (EditorApplication.isPlaying)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
#endif

    }
    #endregion
}

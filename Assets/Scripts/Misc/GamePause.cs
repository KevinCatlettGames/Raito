// Written by Kevin Catlett.

using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the pausing of the game and handles the active state of the canvases to use.
/// </summary>
public class GamePause : MonoBehaviour
{
    [Header("Canvas parent")]
    [Tooltip("The canvas active while playing the level. Can be null.")]
    [SerializeField] GameObject levelCanvas;

    [Tooltip("The canvas active while the game is paused. Can be null")]
    [SerializeField] GameObject pauseCanvas;

    [Header("")]
    [Tooltip("The options menu gameObject.")]
    [SerializeField] GameObject optionsMenu;

    [Tooltip("The controls menu gameObject")] 
    [SerializeField] GameObject controlsMenu;
    
    [Tooltip("If the cursor is invisible.")]
    [SerializeField] bool cursorIsHidden = true;
    
    // The pause state of the game. 
    bool paused;
    
    // If the options menu is open.
    bool inOptions;

    // If the controls menu is open.
    bool inControls;
    
    // Singleton for saving changes made in the options menu. 
    SaveAndLoad saveAndLoad;

    #region Methods
    /// <summary>
    /// Changes the visibility of the cursor.
    /// Gets a reference to the saveAndLoad singleton. 
    /// </summary>
    void Awake()
    {
        if (cursorIsHidden)
        {
            Cursor.visible = false;
        }

        if(SaveAndLoad.instance)
        {
           saveAndLoad = SaveAndLoad.instance;
        }
    }

    /// <summary>
    /// Calls the HandlePause method when the escape button is pressed.
    /// </summary>
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            HandlePause();
        }
    }

    /// <summary>
    /// Sets the paused state and handles the active state of the level and pause canvas.
    /// Sets the timeScale value to pause or unpause the game.
    /// Sets the cursors visibility.  
    /// </summary>
    public void HandlePause()
    {
        paused = !paused;

        if(paused) // Deactivate the level canvas, activate the pause canvas. 
        {
            Time.timeScale = 0;
            if (levelCanvas)
            {
                levelCanvas.SetActive(false);               
            }
            if (pauseCanvas)
            {
                pauseCanvas.SetActive(true);
            }

            if (cursorIsHidden)
            {
                cursorIsHidden = false;
                Cursor.visible = true;
            }
        }
        else // Deactivate the pause canvas, activate the level canvas. 
        {
            Time.timeScale = 1;
            if (levelCanvas)
            {
                levelCanvas.SetActive(true);
            }
            if (pauseCanvas)
            {
                pauseCanvas.SetActive(false);
            }

            if (!cursorIsHidden)
            {
                cursorIsHidden = true;
                Cursor.visible = false;
            }

            if (inOptions)
            {
                HandleOptionsMenu();
            }

            if (inControls)
            {
                HandleControlsMenu();
            }
        }
    }

    /// <summary>
    /// Activates or deactivates the options menu gameObject depending on the state of the inOptions boolean. 
    /// </summary>
    public void HandleOptionsMenu()
    {
        inOptions = !inOptions;

        if(inOptions)
        {
            optionsMenu.SetActive(true);
        }

        else
        {
            optionsMenu.SetActive(false);
            if (saveAndLoad)
            {
                saveAndLoad.Save(); // Save the current values inside of the options menu. 
            }
        }
    }
    
    /// <summary>
    /// Activates or deactivates the controls menu gameObject depending on the state of the inControls boolean. 
    /// </summary>
    public void HandleControlsMenu()
    {
        inControls = !inControls;

        if(inControls)
        {
            controlsMenu.SetActive(true);
        }

        else
        {
            controlsMenu.SetActive(false);
        }
    }

    /// <summary>
    /// Loads a scene with the same build index as the buildIndex parameter.
    /// </summary>
    /// <param name="buildIndex"></param> The build index of the scene that should be loaded.
    public void LoadScene(int buildIndex)
    {
        Time.timeScale = 1; // Sets the time scale to one before loading a new scene,
                            // so the newly loaded scene is not paused when loaded.
                            
        SceneManager.LoadScene(buildIndex);
    }
    #endregion

}

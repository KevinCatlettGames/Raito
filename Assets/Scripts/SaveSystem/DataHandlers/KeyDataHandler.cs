// Written by Kevin Catlett.

using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// A singleton handling key information.
/// Deriving from Data Handler class allows communication with the games save system.
/// </summary>
public class KeyDataHandler : DataHandler
{
    public static KeyDataHandler instance; // Singleton

    [Tooltip("How many keys are in the game.")]
    [SerializeField] int totalKeyAmount;

    [Tooltip("Scenes in which to check if the keys have all been placed.")]
    [SerializeField] string[] ScenesToInvokeOnKeysPlaced;

    [Tooltip("Stores which keys have been successfully collected in the levels.")]
    [SerializeField] bool[] keyCollected;
    public bool[] KeyCollected { get { return keyCollected; } private set { keyCollected = value; } }

    [Tooltip("Stores which keys have been successfully placed in the point and click scene.")]
    [SerializeField] bool[] keyPlaced;
    public bool[] KeyPlaced { get { return keyPlaced; }  set { keyPlaced = value; } }

    // Invoking occurs so other classes know wether all keys have already been placed.
    public delegate void OnKeysPlaced();
    public event OnKeysPlaced allKeysPlaced;
    
    #region Methods
    void Awake()
    {
        // Setting the singleton.
        if (instance == null) { instance = this; }
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);      
    }
    
    void OnEnable()
    {
        SceneManager.activeSceneChanged += OnSceneLoaded; // Check if the event should be invoked in the newly loaded scene.
    }

    /// <summary>
    /// Changes if a key has been collected.
    /// </summary>
    /// <param name="keyIndex"></param> The index of the key whos value should be changed.
    public void CollectKey(int keyIndex)
    {
        keyCollected[keyIndex] = true;
    }

    /// <summary>
    /// Changes if a key has been placed.
    /// </summary>
    /// <param name="keyIndex"></param> The index of the key whos value should be changed
    public void PlaceKey(int keyIndex)
    {
        KeyPlaced[keyIndex] = true;
        KeysPlacedCheck();
    }

    /// <summary>
    /// Invokes the keys placed event if the keys have all been placed.
    /// </summary>
    void KeysPlacedCheck()
    {
        bool invokeEvent = AllKeysPlaced();
      
        if(invokeEvent)
        {
            allKeysPlaced?.Invoke();
        }
        
    }

    /// <summary>
    /// Gets called when a scene is loaded.
    /// </summary>
    void OnSceneLoaded(Scene oldScene, Scene newScene)
    {
        foreach (string s in ScenesToInvokeOnKeysPlaced)
        {
            if (s == newScene.name)
            {
                KeysPlacedCheck();
            }
        }
    }

    /// <summary>
    /// Loops through all placed keys and returns true if all keys have been placed.
    /// </summary>
    bool AllKeysPlaced()
    {
        int keysInserted = 0; //Checking all placed key booleans.
        for (int i = 0; i < this.keyPlaced.Length; i++)
        {
            if (this.keyPlaced[i])
            {
                keysInserted++;
            }
        }

        if (keysInserted == keyPlaced.Length) // Return wether all keys have been placed.
        {
            return true;
        }

        return false;
    }
    
    public override void RecieveData(SaveData saveData)
    {      
        keyCollected[0] = saveData.firstKeyCollected;
        keyCollected[1] = saveData.secondKeyCollected;
        keyCollected[2] = saveData.thirdKeyCollected;
        keyPlaced[0] = saveData.firstKeyInserted;
        keyPlaced[1] = saveData.secondKeyInserted;
        keyPlaced[2] = saveData.thirdKeyCollected;
    }

    public override void SendData(SaveData saveData)
    {
        saveData.firstKeyCollected = keyCollected[0];
        saveData.secondKeyCollected = keyCollected[1];
        saveData.thirdKeyCollected = keyCollected[2];
        
        saveData.firstKeyInserted = keyPlaced[0];
        saveData.secondKeyInserted = keyPlaced[1];
        saveData.thirdKeyInserted = keyPlaced[2];

    }
    #endregion 
}

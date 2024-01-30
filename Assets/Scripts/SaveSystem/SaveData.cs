// Written by Kevin Catlett.

using System;

/// <summary>
/// This class is used to save all information that needs to be saved and gets passed from and to the save system to save and set values.
/// </summary>
[Serializable]
public class SaveData
{
    // Which key has been collected.
    public bool firstKeyCollected;
  
    public bool secondKeyCollected;

    public bool thirdKeyCollected;

    // Which key has been inserted.
    public bool firstKeyInserted;
   
    public bool secondKeyInserted;

    public bool thirdKeyInserted;
    
    public float globalVolume; // Stores the global volume.
   

    public float musicVolume; // Stores the current volume of ingame music.
   

    public float soundVolume; // Stores the current volume of ingame sound.
   
}

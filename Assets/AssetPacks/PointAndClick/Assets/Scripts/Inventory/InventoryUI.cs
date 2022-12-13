using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Updates and handles the inventories UI to always show the correct items and their order inside of the inventory.
/// </summary>
public class InventoryUI : MonoBehaviour
{
    #region Variables
    public static InventoryUI instance; // Singleton.

    Inventory inventory; // A reference to the inventory singleton.

    [SerializeField]
    Color emptySlotColor; // The color a slot should be when no item is represented.

    [SerializeField]
    List<Image> slotImages; // A List with the inventory images in order from left to right.

    #endregion

    #region Awake and Start
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        instance = this; // Sets the singleton.
    }

    private void Start()
    {
        inventory = Inventory.instance;
    }

    #endregion

    #region Methods
    /// <summary>
    /// Removes all image sprites and adds new sprites representing the current inventory in order. 
    /// </summary>
    public void UpdateInventorySlots()
    {
        ResetInventorySlots();

        List<Item> currentItems = inventory.ItemsInInventory;

        for(int i = 0; i < currentItems.Count; i++)
        {
            slotImages[i].sprite = currentItems[i].Sprite;
            slotImages[i].color = Color.white;
        }
    }

    /// <summary>
    /// Called when a button on a slot is pressed.
    /// </summary>
    /// <param name="slotIndex"></param> The slot that the player interacted with.
    public void InteractWithInventorySlot(int slotIndex)
    {
        Debug.Log("Interacted with slot");
        Debug.Log(slotIndex);

        List<Item> currentItems = inventory.ItemsInInventory;

        if (currentItems.Count >= slotIndex && currentItems.Count > 0) // Is the slot currently occupied.
        {
            inventory.RemoveFromInventory(slotIndex);
            ResetInventorySlots();
            UpdateInventorySlots();       
        }
    }

    /// <summary>
    /// Resets all of the slot images.
    /// </summary>
    public void ResetInventorySlots()
    {
        foreach(Image image in slotImages)
        {
           image.sprite = null;
           image.color = emptySlotColor;
        }
    }

    #endregion
}

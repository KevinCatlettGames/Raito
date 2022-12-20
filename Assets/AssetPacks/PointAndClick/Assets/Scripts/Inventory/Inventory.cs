using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    InventoryUI handleInventoryUI;
    ActiveItemUI activeItemUI;

    [SerializeField]
    List<Item> itemsInInventory; // The current items in the inventory.
    [HideInInspector]
    public List<Item> ItemsInInventory { get { return itemsInInventory; } set { } }

    [SerializeField]
    int itemCapacity; // The maximum amount of unique items the inventory can hold.

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        instance = this;

        itemsInInventory = new List<Item>();
    }

    private void Start()
    {
        handleInventoryUI = InventoryUI.instance;
        activeItemUI = ActiveItemUI.instance;
    }

    public void AddToInventory(Item item)
    {
        Debug.Log("Adding to inventory");
        if (itemsInInventory.Count < itemCapacity)
        {
            itemsInInventory.Add(item);
        }
        handleInventoryUI.UpdateInventorySlots();
    }

    public void RemoveFromInventory(int removeIndex)
    {
       switch(ItemsInInventory[removeIndex].ItemType)
        {
            case ItemUsage.Placeable:
                Debug.Log("Is Placeable");
                activeItemUI.ActivateActiveItemImage(ItemsInInventory[removeIndex].Sprite);
                itemsInInventory.RemoveAt(removeIndex);
                break;
            case ItemUsage.Usable:
                itemsInInventory.RemoveAt(removeIndex); 
                // TODO Use the item.
                break;
        }       
    }
}

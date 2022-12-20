using UnityEngine;

public class CollectItem : MonoBehaviour
{
    [SerializeField]
    bool addToInventory;

    [SerializeField]
    Item item;

    [SerializeField]
    bool isInPointAndClickLevel;

    Inventory inventory;

    private void Start()
    {
        inventory = Inventory.instance;
    }

    private void OnMouseDown()
    {
        if (addToInventory && !isInPointAndClickLevel)
        {
            Collect();
        }
    }

    public void Collect()
    {      
        inventory.AddToInventory(item);
        Destroy(this.gameObject);       
    }
}

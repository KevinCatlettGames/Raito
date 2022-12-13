using UnityEngine;
using UnityEngine.UI;

public enum ItemUsage { Placeable, Usable /*, Equipable */};

[CreateAssetMenu(menuName = "Item")]
/// <summary>
/// Items are added to the inventory and can be used by the player to interact with the game.
/// </summary>
public class Item : ScriptableObject
{
    #region Variables
    [SerializeField]
    ItemUsage itemType;
    public ItemUsage ItemType { get { return itemType; } set { } }

    [SerializeField]
    Sprite sprite;
    public Sprite Sprite { get { return sprite; } set { } }

    #endregion
}

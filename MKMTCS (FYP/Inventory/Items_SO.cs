using UnityEngine;

public enum ItemTypes 
{
    Consumable,
    Material,
    Placeable
}
public enum ConsumableTypes 
{
    None,
    ForHealth,
    ForMana,
    ForHunger
}

[CreateAssetMenu(fileName = "Items_SO", menuName = "ScriptableObjects/Items_SO")]
public class Items_SO : ScriptableObject
{
    [Header("Item Settings")]
    public string ItemName;
    [TextArea(5,5)]
    public string ItemDescription;
    public GameObject ItemObject;
    public ItemTypes ItemType;
    public Sprite ItemSprite;
    public ConsumableTypes ComsumableType;

    [Header("Consumable Item Settings")]
    public float number;

#if UNITY_EDITOR
    private void OnValidate()
    {
        ItemName = this.name;
        if (this.ItemType != ItemTypes.Consumable)
            number = 0;
    }
#endif
}

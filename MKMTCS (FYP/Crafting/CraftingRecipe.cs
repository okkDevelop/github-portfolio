using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "CraftingRecipe_SO", menuName = "ScriptableObjects/CraftingRecipe_SO")]
public class CraftingRecipe : ScriptableObject
{
    //public Items_SO ItemsObject;
    public string ProductName;
    public Sprite CraftSprite;
    public GameObject Product;
    public List<InventoryItemData> Material;

#if UNITY_EDITOR
    private void OnValidate()
    {
        ProductName = this.name;
    }
#endif
}
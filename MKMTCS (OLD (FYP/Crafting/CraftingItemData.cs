using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingItemData : MonoBehaviour
{
    public CraftingRecipe craftingRecipe;
    [SerializeField] private Items_SO items;

    [SerializeField] private GameObject RecipeNeeded;
    [SerializeField] private Transform CraftingRecipeLayout;
    private Button CraftBtn;

    private void Awake()
    {
        CraftBtn = GetComponentInChildren<Button>();
        foreach (InventoryItemData craftingMaterial in craftingRecipe.Material) 
        {
            GameObject materialNeeded = Instantiate(RecipeNeeded, CraftingRecipeLayout);

            Image newProductSprite = transform.Find("CraftableItemSprite").GetComponent<Image>();
            TextMeshProUGUI newProductName = GetComponentInChildren<TextMeshProUGUI>();

            newProductSprite.sprite = craftingRecipe.CraftSprite;
            newProductName.text = craftingRecipe.ProductName;

            Image materialImage = materialNeeded.GetComponent<Image>();
            TextMeshProUGUI materialText = materialNeeded.GetComponentInChildren<TextMeshProUGUI>();

            materialImage.sprite = craftingMaterial.items.ItemSprite;
            materialText.text = craftingMaterial.stackingNumber.ToString();

            CraftBtn.onClick.AddListener(() => Inventory.Instance.RemoveItem(craftingMaterial.items, craftingMaterial.stackingNumber));  // Correctly adding the listener
        }
    }

    private void Update()
    {
        CraftBtn.interactable = CanCraft() ? true : false;
    }

    private bool CanCraft()
    {
        foreach (var materialNeeded in craftingRecipe.Material)
        {
            if (Inventory.Instance.GetItemCount(materialNeeded.items) < materialNeeded.stackingNumber)
            {
                return false;
            }
        }
        return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory Item Prefab Settings")]
    [SerializeField] private GameObject inventoryItemPrefabs;
    [SerializeField] private Transform inventoryScrollViewContent;
    [SerializeField] private ToolSelection toolSelection;
    private ItemActionLibrary itemActionLibrary;

    public static InventoryManager Instance;

    private void Awake()
    {
        if (Instance == null) 
            Instance = this;
    }

    private void Start() 
    {
        itemActionLibrary = GetComponent<ItemActionLibrary>();
    }

    public void InstantiateInventoryItemPrefab(InventoryItemData inventoryItems)
    {
        GameObject newItemPrefab = Instantiate(inventoryItemPrefabs, inventoryScrollViewContent);
        newItemPrefab.SetActive(true);

        Image newItemSprite = newItemPrefab.transform.Find("ItemImage").GetComponent<Image>();
        TextMeshProUGUI newItemName = newItemPrefab.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI newItemStackingSize = newItemPrefab.transform.Find("StackingSize").GetComponent<TextMeshProUGUI>();
        Button useBtn = newItemPrefab.transform.Find("UseBtn").GetComponent<Button>();
        Button dropBtn = newItemPrefab.transform.Find("DropBtn").GetComponent<Button>();

        newItemSprite.sprite = inventoryItems.items.ItemSprite;
        newItemName.text = inventoryItems.items.ItemName;
        newItemStackingSize.text = inventoryItems.stackingNumber.ToString();

        if (inventoryItems.items.ItemType == ItemTypes.Consumable)
            useBtn.onClick.AddListener(() => ItemActionLibrary.Instance.Consume(inventoryItems.items));
        else if (inventoryItems.items.ItemType == ItemTypes.Placeable)
        {
            Debug.Log("this function still in developing");
            useBtn.onClick.AddListener(() => itemActionLibrary.Place(inventoryItems.items.ItemObject));
            useBtn.onClick.AddListener(() => Inventory.Instance.RemoveItem(inventoryItems.items, 1));
        }
        else
        {
            GameObject useBtnGameObject = useBtn.gameObject;
            useBtnGameObject.SetActive(false);
        }

        dropBtn.onClick.AddListener(() => Inventory.Instance.DropItem(inventoryItems.items, 1));  // Correctly adding the listener
    }

    public void UpdateStackingSize(InventoryItemData inventoryItems) 
    {
        foreach (Transform inventoryPrefabObject in inventoryScrollViewContent) 
        {
            TextMeshProUGUI existingItemName = inventoryPrefabObject.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();

            if (existingItemName.text == inventoryItems.items.ItemName) 
            {
                TextMeshProUGUI existingItemStackingSize = inventoryPrefabObject.transform.Find("StackingSize").GetComponent<TextMeshProUGUI>();

                existingItemStackingSize.text = inventoryItems.stackingNumber.ToString();

                break;
            }
        }
    }

    public void DestoryInventoryItemPrefab(InventoryItemData inventoryItems) 
    {
        foreach (Transform inventoryPrefabObject in inventoryScrollViewContent)
        {
            TextMeshProUGUI existingItemName = inventoryPrefabObject.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();

            if (existingItemName.text == inventoryItems.items.ItemName)
            {
                GameObject ItemPrefabsToDestroy = existingItemName.transform.parent.gameObject;
                Destroy(ItemPrefabsToDestroy);

                break;
            }
        }
    }
}
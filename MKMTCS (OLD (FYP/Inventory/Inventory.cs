using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Transform DropTransform;

    public Dictionary<string, InventoryItemData> StoredItems = new Dictionary<string, InventoryItemData>();

    public static Inventory Instance;

    private void Awake()
    {
        if (Instance == null) 
            Instance = this;
    }

    private void Update()
    {
        LogStoredItems();
    }

    public void AddItem(Items_SO items)
    {
        if (StoredItems.ContainsKey(items.ItemName))
        {
            InventoryItemData existingItems = StoredItems[items.ItemName];
            existingItems.stackingNumber += 1;
            StoredItems[items.ItemName] = existingItems;
            InventoryManager.Instance.UpdateStackingSize(existingItems);
        }
        else
        {
            InventoryItemData newItems = new InventoryItemData();
            newItems.items = items;
            newItems.stackingNumber = 1;
            StoredItems.Add(items.ItemName, newItems);
            InventoryManager.Instance.InstantiateInventoryItemPrefab(newItems);
        }
    }

    public void AddItem(Items_SO items, int number) 
    {
        if (StoredItems.ContainsKey(items.ItemName))
        {
            InventoryItemData existingItems = StoredItems[items.ItemName];
            existingItems.stackingNumber += number;
            StoredItems[items.ItemName] = existingItems;
            InventoryManager.Instance.UpdateStackingSize(existingItems);
        }
        else
        {
            InventoryItemData newItems = new InventoryItemData();
            newItems.items = items;
            newItems.stackingNumber = number;
            StoredItems.Add(items.ItemName, newItems);
            InventoryManager.Instance.InstantiateInventoryItemPrefab(newItems);
        }
    }

    public void RemoveItem(Items_SO items)
    {
        if (StoredItems.ContainsKey(items.ItemName))
        {
            InventoryItemData existingItems = StoredItems[items.ItemName];
            existingItems.stackingNumber -= 1;
            InventoryManager.Instance.UpdateStackingSize(existingItems);

            if (existingItems.stackingNumber <= 0)
            {
                StoredItems.Remove(items.ItemName);
                InventoryManager.Instance.DestoryInventoryItemPrefab(existingItems);
            }
            else
                StoredItems[items.ItemName] = existingItems;
        }
    }

    public void RemoveItem(Items_SO items, int number)
    {
        if (StoredItems.ContainsKey(items.ItemName))
        {
            InventoryItemData existingItems = StoredItems[items.ItemName];

            int existingItemsNumber = GetItemCount(items);

            if (existingItemsNumber >= number)
            {
                existingItems.stackingNumber -= number;
                InventoryManager.Instance.UpdateStackingSize(existingItems);

                if (existingItems.stackingNumber <= 0)
                {
                    StoredItems.Remove(items.ItemName);
                    InventoryManager.Instance.DestoryInventoryItemPrefab(existingItems);
                }
                else
                    StoredItems[items.ItemName] = existingItems;
            }
            else
                return;
        }
    }

    public bool HasItem(Items_SO item)
    {
        return StoredItems.ContainsKey(item.ItemName) && StoredItems[item.ItemName].stackingNumber > 0;
    }

    public int GetItemCount(Items_SO item)
    {
        if (StoredItems.ContainsKey(item.ItemName))
            return StoredItems[item.ItemName].stackingNumber;

        return 0;
    }

    public void DropItem(Items_SO items, int number) 
    {
        if (HasItem(items)) 
        {
            var obj = Instantiate(items.ItemObject, DropTransform);
            obj.transform.parent = null;
            RemoveItem(items, number);
        }
    }

    //for debugging
    public void LogStoredItems()
    {
        foreach (KeyValuePair<string, InventoryItemData> item in StoredItems)
        {
            Debug.Log($"Item Name: {item.Key}, Item Object: {item.Value.items.ItemName}, Stacking Number: {item.Value.stackingNumber}");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class UpgradeBuildings : MonoBehaviour
{
    [Header("Next Upgrade Building Settings")]
    [SerializeField] private GameObject upgradeObject;
    [SerializeField] private bool isPortal;
    [SerializeField] private InventoryItemData[] inventoryItemNeeded;
    private Canvas canvaObject;
    private GameObject canva;

    private bool playerCollided;

    public InventoryItemData[] InventoryItemNeeded => inventoryItemNeeded;

    private void Start()
    {
        canvaObject = GetComponentInChildren<Canvas>();
        canva = canvaObject.gameObject;
        canva.SetActive(false);
    }

    private void Update()
    {
        if (CanUpgrade() && playerCollided)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                Debug.Log("upgrading");
                foreach (var itemNeeded in inventoryItemNeeded)
                {
                    Inventory.Instance.RemoveItem(itemNeeded.items, itemNeeded.stackingNumber);
                    Debug.Log("removing");
                }

                SoundsManager.Instance.PlaySound("Upgrade");

                if (isPortal)
                {
                    GameEventsManager.Instance.questEvents.FixedPortal();
                    upgradeObject.SetActive(true);
                }
                else 
                {
                    GameEventsManager.Instance.questEvents.HouseUpgrade();
                    Instantiate(upgradeObject);
                }

                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            Debug.Log("player enter");
            playerCollided = true;
            canva.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canva.SetActive(false);
            playerCollided = false;
            canva.SetActive(false);
        }
    }

    private bool CanUpgrade() 
    {
        foreach (var itemNeeded in inventoryItemNeeded) 
        {
            if (Inventory.Instance.GetItemCount(itemNeeded.items) < itemNeeded.stackingNumber)
            {
                return false;
            }
        }
        return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GenerateMaterialNeeded : MonoBehaviour
{
    [SerializeField] private GameObject generateMeterialGameObject;
    [SerializeField] private Transform generateTransfrom;
    private UpgradeBuildings upgradeBuildings;
    private InventoryItemData[] inventoryItemData;

    private void Awake()
    {
        upgradeBuildings = GetComponentInParent<UpgradeBuildings>();
        inventoryItemData = upgradeBuildings.InventoryItemNeeded;
    }

    private void Start()
    {
        foreach (var material in inventoryItemData) 
        {
            GameObject materialListed = Instantiate(generateMeterialGameObject, generateTransfrom);

            Image materialSprite = materialListed.GetComponentInChildren<Image>();
            TextMeshProUGUI materialStackingNumber = materialListed.GetComponentInChildren<TextMeshProUGUI>();

            materialSprite.sprite = material.items.ItemSprite;
            materialStackingNumber.text = material.stackingNumber.ToString();
        }
    }
}

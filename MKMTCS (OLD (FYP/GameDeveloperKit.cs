using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GameDeveloperKit : MonoBehaviour
{
    [Header("player status")]
    [SerializeField] private GameObject playerObject;
    private PlayerStatus playerStatus;
    private FirstPersonController firstPersonController;

    [Header("player inventory")]
    [SerializeField] private Inventory inventory;
    [SerializeField] private Items_SO[] items;

    private void Start()
    {
        playerStatus = playerObject.GetComponent<PlayerStatus>();
        firstPersonController = playerObject.GetComponent<FirstPersonController>();
    }

    private void Update()
    {
        ChangePlayerSpeed();
        AddItems();
    }

    private void ChangePlayerSpeed() 
    {
        if (Input.GetKeyDown(KeyCode.U)) 
        {
            playerStatus.HealthValue = 100;
            playerStatus.HungerValue = 100;
            playerStatus.ManaValue = 100;

            firstPersonController.CurrentWalkingSpeed += 10;
            firstPersonController.CurrentRunningSpeed += 10;
        }
        else if(Input.GetKeyDown(KeyCode.O))
        {
            firstPersonController.CurrentWalkingSpeed -= 10;
            firstPersonController.CurrentRunningSpeed -= 10;
        }
    }

    private void AddItems()
    {
        if (Input.GetKeyDown(KeyCode.L)) 
        {
            foreach (var item in items) 
            {
                inventory.AddItem(item, 50);
            }
        }
    }
}

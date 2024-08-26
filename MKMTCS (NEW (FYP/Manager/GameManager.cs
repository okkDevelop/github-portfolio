using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform revivePoint;

    [HideInInspector]public bool WinTrigger;

    public static GameManager Instance;

    private void Awake()
    {
        if(Instance == null)
            Instance = this; 
    }

    public void Revive() 
    {
        PlayerStatus.Instance.HealthValue = 100;
        PlayerStatus.Instance.HungerValue = 100;
        PlayerStatus.Instance.ManaValue = 100;

        var characterController = player.GetComponent<CharacterController>();
        if (characterController != null)
            characterController.enabled = false;

        player.transform.position = revivePoint.position;

        if (characterController != null)
            characterController.enabled = true;
    }
}

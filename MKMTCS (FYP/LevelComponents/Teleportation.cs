using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    [SerializeField] private Transform teleportTransform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            var characterController = other.gameObject.GetComponent<CharacterController>();
            if (characterController != null)
                characterController.enabled = false;

            other.gameObject.transform.position = teleportTransform.position;

            if (characterController != null)
                characterController.enabled = true;
        }
    }
}

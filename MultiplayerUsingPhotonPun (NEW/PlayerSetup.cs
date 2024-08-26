using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using WeirdBrothers;
using UnityEngine.UI;

public class PlayerSetup : MonoBehaviourPunCallbacks
{
	[SerializeField] private GameObject playerCameraPrefab; // Reference to the camera prefab
    [SerializeField] private GameObject playerCanvaPrefab;
    private WeirdBrothers.PlayerController playerController;
    //private GameObject playerCameraInstance; // Instance of the camera prefab
    [SerializeField] private GameObject TPC;
    [SerializeField] private GameObject AimC;

    [SerializeField] private GameObject minimapCamera;
    [SerializeField] private GameObject minimapUI;

    void Start()
    {
		bool isMine = photonView.IsMine;
		playerCanvaPrefab.SetActive(isMine);
        // Get references to other components
        playerController = GetComponent<PlayerController>();
		playerController.enabled = isMine;
		playerCameraPrefab.SetActive(isMine);
        //playerUIPrefab.SetActive(isMine);
		TPC.SetActive(isMine);
		AimC.SetActive(isMine);
        if (minimapCamera != null) minimapCamera.SetActive(isMine);
        if (minimapUI != null) minimapUI.SetActive(isMine);
    }

    /*void Update()
    {
        // Synchronize the camera's position and rotation with the player's
        if (photonView.IsMine)
        {
            // Calculate the desired position and rotation of the camera
            Vector3 targetPosition = transform.position + transform.TransformDirection(cameraOffset);
            Quaternion targetRotation = Quaternion.LookRotation(transform.position - targetPosition, Vector3.up);

            // Smoothly interpolate the camera's position and rotation towards the target
            playerCameraInstance.transform.position = Vector3.Lerp(playerCameraInstance.transform.position, targetPosition, Time.deltaTime * cameraFollowSpeed);
            playerCameraInstance.transform.rotation = Quaternion.Slerp(playerCameraInstance.transform.rotation, targetRotation, Time.deltaTime * cameraFollowSpeed);
        }
    }*/
    
	GameObject FindGameObjectUnderCanvas(string gameObjectName)
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            Transform[] children = canvas.GetComponentsInChildren<Transform>(true);
            foreach (Transform child in children)
            {
                if (child.gameObject.name == gameObjectName)
                {
                    return child.gameObject;
                }
            }
        }
        return null;
    }
}

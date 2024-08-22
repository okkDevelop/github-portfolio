using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    private AudioSource audioToTrigger;
    [SerializeField] private Transform playerTransformPosition;
    [SerializeField] private float maxVolume = 1f;
    [SerializeField] private float maxDistance = 10f;

    private void Start()
    {
        audioToTrigger = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            audioToTrigger.Play();
        }
        else
            return;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            audioToTrigger.Stop();
        }
        else
            return;
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerTransformPosition != null && audioToTrigger.isPlaying)
        {
            // Calculate the distance between the player and the sound sphere
            float distance = Vector3.Distance(playerTransformPosition.position, transform.position);
            // Adjust the volume based on the distance
            audioToTrigger.volume = Mathf.Clamp01((maxDistance - distance) / maxDistance) * maxVolume;
        }
    }
}

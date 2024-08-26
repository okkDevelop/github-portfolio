using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_walkingBehavior : MonoBehaviour
{
    public float speed = 1.0f; // The speed at which the NPC walks
    public float distance = 2.0f; // The distance the NPC walks before turning around

    private int direction = 1; // The direction the NPC is currently facing (1 = right, -1 = left)
    private float distanceTraveled = 0; // The distance the NPC has traveled since turning around

    void Update()
    {
        // Move the NPC horizontally along the x-axis
        transform.Translate(direction * speed * Time.deltaTime, 0, 0);

        // Keep track of the distance the NPC has traveled
        distanceTraveled += Mathf.Abs(direction * speed * Time.deltaTime);

        // If the NPC has traveled the maximum distance, turn around
        if (distanceTraveled >= distance)
        {
            direction *= -1; // Reverse the direction
            distanceTraveled = 0; // Reset the distance traveled
            // Flip the NPC's sprite to face the new direction
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }
}

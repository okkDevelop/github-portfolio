using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Transform target;
	private Vector3 offset;
	
    public float smoothTime = 0.3f; // The time it takes for the camera to reach the target position
    public float yOffset = 4.0f; // The height offset of the camera from the target object
	public float zOffset = 0f;
	public float xRotation = 0f;

    private Vector3 velocity = Vector3.zero;
	
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {		
		//Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, offset.z + target.position.z);
		//transform.position = newPosition;
    }
	
	private void FixedUpdate()
	{
		/*if(!playerManager.StartGame)
		{
			return;
		}*/
		if(playerManager.StartGame)
		{
			// Set the target position as the position of the target object plus the yOffset
			Vector3 targetPosition = target.position + new Vector3(0, yOffset, zOffset);

			// Use the Lerp method to smoothly move the camera position towards the target position
			transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

			// Adjust the camera rotation to look at the target
			//transform.LookAt(target);
			
			// Rotate the camera on the x-axis by 30 degrees
			transform.rotation = Quaternion.Euler(xRotation, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
		}
		
	}
	
}

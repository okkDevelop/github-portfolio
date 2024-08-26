using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloodController : MonoBehaviour
{
	public float attractorSpeed;
	//public Transform childCollider;
	private PlayerHealth _playerHealth;
	
	void Awake()
	{
		_playerHealth = GetComponent<PlayerHealth>();
	}
	
	private void OnTriggerStay2D(Collider2D hit)
	{
		if(hit.tag == "Player")
		{
			Debug.Log("suck");
			transform.position = Vector3.MoveTowards(transform.position, hit.transform.position, attractorSpeed * Time.deltaTime);
			if(Vector3.Distance(transform.position, hit.transform.position) <= 0)
			{
				Debug.Log("take");
				Destroy(gameObject);
				_playerHealth.IncreaseBlood(1);
			}
		}
	}
}

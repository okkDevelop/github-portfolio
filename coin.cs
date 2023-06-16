using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{	
	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			FindObjectOfType<AudioManager>().PlaySound("pickUpCoin");
			playerManager.coinCal++;
			//getCoin.Play();
			Destroy(gameObject);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soulController : MonoBehaviour
{
	public Animator soulDisappear;
	public float damageAmount = -20f;
	
	private void OnTriggerEnter2D(Collider2D hit)
	{
		if(hit.tag == "Player")
		{
			//Debug.Log("touch");
			PlayerHealth playerHealth = hit.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.PlayerTakeDamage(damageAmount);
            }
			soulDisappear.SetTrigger("vanish");
			StartCoroutine(delayForDisappear());
		}
	}
	
	private IEnumerator delayForDisappear()
	{
		yield return new WaitForSeconds(0.6f);
		Destroy(gameObject);
	}
	
}

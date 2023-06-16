using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakableObject : MonoBehaviour
{
	public GameObject broken;
	
	private float twoSec = 2f;
	private float threeSec = 3f;
	
	private int appearCheck = 0;
	
	/*
	private void OnTriggerEnter2D(Collider2D hit)
	{
		if(hit.tag == "Player")
		{
			appearCheck++;
			breakIt();
		}		
	}
	
	void OnCollisionEnter2D(Collision2D hit2)
	{
		if(hit2.gameObject.tag == "Player")
		{
			breakIt();
		}
	}
	*/
	
	public void breakIt()
	{
		Destroy(this.gameObject);
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sortingOrder = -10;
		GameObject brokenPiece = Instantiate(broken, transform.position, Quaternion.identity);
		StartCoroutine(DelayedDestroy(brokenPiece, twoSec));
		foreach(Transform child in brokenPiece.transform)
		{
			child.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-2f,2f),Random.Range(3f,5f));
		}
		//this.gameObject.SetActive(false);
		StartCoroutine(DelayedDestroy(this.gameObject, threeSec));
	}
	
	private IEnumerator DelayedDestroy(GameObject obj, float seconds)
	{
		yield return new WaitForSeconds(seconds);
		Destroy(obj);
	}
	
}
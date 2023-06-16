using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rayCast : MonoBehaviour
{
	public float range = 1.5f;
	public float range2 = 2f;
	public Animator hitAnimator;
	
	Vector3 rotationRight = Quaternion.Euler(0f, 45f, 0f) * Vector3.forward;
	Vector3 rotationLeft = Quaternion.Euler(0f, -45f, 0f) * Vector3.forward;

    // Update is called once per frame
    void Update()
    {
        //ray cast code
		Vector3 direction = Vector3.forward;
		
		Ray theRay = new Ray(transform.position, transform.TransformDirection(direction * range));
		Ray rightRay = new Ray(transform.position, rotationRight * range2);
		Ray leftRay = new Ray(transform.position, rotationLeft * range2);
		
		Debug.DrawRay(transform.position, transform.TransformDirection(direction * range));
		Debug.DrawRay(transform.position, rotationRight * range2, Color.red);
		Debug.DrawRay(transform.position, rotationLeft * range2, Color.blue);
		
		
		if(Physics.Raycast(theRay, out RaycastHit hit , range))
		{
			if(hit.collider.tag == "enemyNobstacle" || hit.collider.tag == "festivalEnemy")
			{
				Debug.Log("detected");
				if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
				{
					Debug.Log("kill");
					FindObjectOfType<AudioManager>().PlaySound("enemyDead");
					Destroy(hit.collider.gameObject);
				}
			}
		}
		else if(Physics.Raycast(leftRay, out RaycastHit hitLeft , range2))
		{
			if(hitLeft.collider.tag == "enemyNobstacle" || hit.collider.tag == "festivalEnemy")
			{
				Debug.Log("detectedLeft");
				if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
				{
					Debug.Log("kill");
					hitAnimator.SetTrigger("Attack");
					FindObjectOfType<AudioManager>().PlaySound("enemyDead");
					Destroy(hitLeft.collider.gameObject);
					hitAnimator.SetTrigger("AnimationEnd");
				}				
			}
		}
		else if(Physics.Raycast(rightRay, out RaycastHit hitRight , range2))
		{
			if(hitRight.collider.tag == "enemyNobstacle" || hit.collider.tag == "festivalEnemy")
			{
				Debug.Log("detectedRight");
				if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
				{
					Debug.Log("kill");
					hitAnimator.SetTrigger("Attack");
					FindObjectOfType<AudioManager>().PlaySound("enemyDead");
					Destroy(hitRight.collider.gameObject);
					hitAnimator.SetTrigger("AnimationEnd");
				}				
			}
		}
		
    }
}

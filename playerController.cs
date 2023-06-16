using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerController : MonoBehaviour
{
	private CharacterController controller;
	private Vector3 direction;
	public float forwardSpeed;
		
	private int desiredLane = 1; //0=left 1=middle 2=right
	public float laneDistance;//the distance between two lans
	
	public float jumpForce;
	public float gravity = -20;
	
	public static int healthCal = 3;
	public TMP_Text healthText;
	
	public Animator playerAnimator;
		
	private bool canaTakeDamage = true;
	private float damageCooldown = 2f;
	
	//testing
	
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {	
		direction.z = forwardSpeed;
		direction.y += gravity * Time.deltaTime;
		
		if(controller.isGrounded)
		{
			//direction.y = -1;
			if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
			{
				playerAnimator.SetTrigger("JumpButton");
				Jump();
				playerAnimator.SetTrigger("AnimationEnd");
			}
			else if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
			{
				StartCoroutine(Slide());
			}
		}

		if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
		{
			desiredLane++;
			if(desiredLane == 3)
				desiredLane = 2;
		}
		
		if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
		{
			desiredLane--;
			if(desiredLane == -1)
				desiredLane = 0;
		}
		
		//calculate where we should be in the future
		Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

		if(desiredLane == 0)
		{
			targetPosition += Vector3.left * laneDistance;
		}
		else if(desiredLane == 2)
		{
			targetPosition += Vector3.right * laneDistance;
		}
		
		if(transform.position == targetPosition)
			return;
		Vector3 diff = targetPosition - transform.position;
		Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
		if(moveDir.sqrMagnitude < diff.sqrMagnitude)
			controller.Move(moveDir);
		else
			controller.Move(diff);
    }
	
	private void FixedUpdate()
	{
		if(!playerManager.StartGame)
		{
			return;
		}
		controller.Move(direction * Time.fixedDeltaTime);
	}
	
	private void Jump()
	{
		direction.y = jumpForce;
	}
	
	private void OnTriggerEnter(Collider hit)
	{		
		if((hit.tag == "enemyNobstacle" || hit.tag == "obstacles") && canaTakeDamage)
		{
			canaTakeDamage = false;
			StartCoroutine(ResetDamageCooldown(damageCooldown));
			Debug.Log(healthCal);
			healthCal--;
			healthText.SetText(healthCal.ToString());
			FindObjectOfType<AudioManager>().PlaySound("playerHurt");
			if(healthCal < 1)
			{
				playerManager.gameOver = true;
				healthCal = 3;
			}
		}
	}
	
	private IEnumerator Slide()
	{
		playerAnimator.SetTrigger("SlideButton");
		yield return new WaitForSeconds(1f);
		playerAnimator.SetTrigger("AnimationEnd");
	}
	
	private IEnumerator ResetDamageCooldown(float delay)
	{
		yield return new WaitForSeconds(delay);
		canaTakeDamage = true;
	}	
}

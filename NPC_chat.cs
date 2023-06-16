using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC_chat : MonoBehaviour
{
	//declare for get the value
	public TextMeshProUGUI textComponent;
	//to store the chat that we want
	public string[] lines;
	//for implement the text speed
	public float textSpeed;
	//to implement the chat panel
	public GameObject wholePanel;
	//interact message
	public GameObject interactMessage;
	
	//Reference to the NPC's movement script
	public NPC_walkingBehavior npcMovement;
	public Animator npcAnimator;
	
	private int index;
	//to detect the player is close
	private bool playerClose;
	private int check = 0;
	
	void Start()
	{
		//set the index to 0 to let the element inside the array can be read from first
		index = 0;
		//clear all the text indide the panel
		textComponent.text = string.Empty;
	}
	
	void Update()
	{
		//active chat panel when press F and close enough
		if(Input.GetKeyDown(KeyCode.F) && playerClose == true)
		{
			Debug.Log("close");
			//active the chat panel
			wholePanel.SetActive(true);
			if(textComponent.text == lines[index])
			{
				Debug.Log("reading");
				NextLine();
			}
			else
			{
				Debug.Log("end");
				StopAllCoroutines();
				textComponent.text = lines[index];
			}
			npcAnimator.SetBool("stayOmove", true);
			npcMovement.enabled = false;
		}
	}
	
	IEnumerator TypeLine()
	{
		//evey single word in the array[i] will show one by one
		foreach(char c in lines[index].ToCharArray())
		{
			//show the single word one by one
			textComponent.text += c;
			//the speed when the word to show
			yield return new WaitForSeconds(textSpeed);
		}
	}
	
	void NextLine()
	{
		//if index less than the length will keep running
		if(index < lines.Length - 1)
		{
			index++;
			//this line dun know
			textComponent.text = string.Empty;
			StartCoroutine(TypeLine());
		}
		else
		{
			Debug.Log("keep moving");
			//hide the chat panel if all the chat element was shown
			wholePanel.SetActive(false);
		}
	}
	
	private void OnTriggerEnter2D(Collider2D hit)
	{
		//detect the player chatacter and if collide set it to true
		if(hit.tag == "Player")
		{
			playerClose = true;
			check = 1;
			Debug.Log(check);
			if(check == 1)
			{
				interactMessage.SetActive(true);
			}
		}
	}
	
	private void OnTriggerExit2D(Collider2D hit)
	{
		//detect the player chatacter and if leave set it to false
		if(hit.tag == "Player")
		{
			check = 0;
			Debug.Log(check);
			if(check == 0)
			{
				wholePanel.SetActive(false);
				interactMessage.SetActive(false);
			}
			playerClose = false;
			//set the chat to empty
			textComponent.text = string.Empty;
			//to let player can replay the chat after leave
			index = 0;
			//Enable NPC movement script after conversation ends
			npcMovement.enabled = true;
			npcAnimator.SetBool("stayOmove", false);
		}
	}
	
}

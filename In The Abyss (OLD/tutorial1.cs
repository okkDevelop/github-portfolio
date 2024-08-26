using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial1 : MonoBehaviour
{
	
	public GameObject tutorialUI1;
	
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
			tutorialUI1.SetActive(true);
        }
    }
	
	private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
			tutorialUI1.SetActive(false);
        }
    }
}

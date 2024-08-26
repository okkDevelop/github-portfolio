using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneSkipper : MonoBehaviour
{
    public float holdTime = 3f; // Time required to hold the key
    private float currentHoldTime = 0f; // Current time the key has been held

    private bool canSkip = false; // Flag to indicate if skipping is in progress

    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            currentHoldTime += Time.deltaTime;

            if (currentHoldTime >= holdTime && !canSkip)
            {
                canSkip = true;
                SkipCutscene();
            }
        }
        else
        {
            currentHoldTime = 0f;
            canSkip = false;
        }
    }

    private void SkipCutscene()
    {
        // Add your code to skip the cutscene here
        Debug.Log("Cutscene skipped!");
		StartGame();
    }
	
	public void StartGame()
	{
		SceneManager.LoadScene("Level 0.5");
	}
	
}

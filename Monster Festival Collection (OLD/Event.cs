using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Event : MonoBehaviour
{
	//get the specify UI from hierachy
	public GameObject pauseMenu;
	public GameObject quitMenu;
	
	//function for reload the scene
	public void ReplayGame()
	{
		SceneManager.LoadScene("UI_scene");
		playerController.healthCal = 3;
	}
	
	//function for quit game
	public void QuitGame()
	{
		Application.Quit();
	}
	
	//function for specify menu
	public void ResumeFromQuit()
	{
		quitMenu.SetActive(false);
	}
	
	//function for specify menu
	public void ResumeFromPause()
	{
		//because after resume the game should run from where player press pause
		Time.timeScale = 1;
		pauseMenu.SetActive(false);
	}
	
}

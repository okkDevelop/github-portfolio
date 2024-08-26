using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerManager : MonoBehaviour
{
	//actually more UI Manager
	public static bool gameOver;
	public GameObject gameOverPanel;
	public static bool StartGame;
	
	public GameObject pauseMenu;
	public GameObject MainMenuPanel;
	public GameObject InGamePanel;	
	public GameObject QuitMenu;
	
	public static int coinCal;
	public TMP_Text coinText;
	
	public static float markCal;	
	public TMP_Text markTextFromInGame;
	public TMP_Text markTextFromDefeated;
	
	public GameObject playerIdle;
	public GameObject playerCharacter;
	
	//public GameObject Camera;
	
	private int InGameCheck;
	
    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
		Time.timeScale = 1;
		
		StartGame = false;
		
		markCal = 0f;
		coinCal = 0;
		InGameCheck = 0;
    }

    // Update is called once per frame
    void Update()
    {	
		markCal = markCal + (1 * Time.deltaTime);
		int markToInt = Mathf.RoundToInt(markCal);
		coinText.SetText(coinCal.ToString());
		markTextFromInGame.SetText(markToInt.ToString());
		markTextFromDefeated.SetText(markToInt.ToString());
		
        if(gameOver)
		{
			Time.timeScale = 0;
			gameOverPanel.SetActive(true);
			InGamePanel.SetActive(false);
			InGameCheck = 0;
		}
		
		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(InGameCheck == 0)
			{
				StartGame = true;
				MainMenuPanel.SetActive(false);
				InGamePanel.SetActive(true);
				FindObjectOfType<AudioManager>().PlaySound("BGM");
				playerIdle.SetActive(false);
				playerCharacter.SetActive(true);	
				InGameCheck = 1;
			}
			else
				return;
		}

		if(StartGame)
		{
			if(Input.GetKeyDown(KeyCode.Escape))
			{
				Time.timeScale = 0;
				pauseMenu.SetActive(true);
			}
		}
		else if(!StartGame)
		{
			if(Input.GetKeyDown(KeyCode.Escape))
				QuitMenu.SetActive(true);
		}
		
    }
	
}

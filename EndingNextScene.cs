using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingNextScene : MonoBehaviour
{
	void OnEnable()
	{
		Debug.Log("NextSceneLoaded");
		SceneManager.LoadScene("Menu2", LoadSceneMode.Single);
	}
}

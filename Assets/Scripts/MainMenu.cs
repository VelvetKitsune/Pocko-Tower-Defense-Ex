using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public string levelToLoad = "TowerDefenseScene";
	
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Return))
		{
			Play();
		}
	}
	
	public void Play()
	{
		//Debug.Log ("Play");
		SceneManager.LoadScene (levelToLoad, LoadSceneMode.Single);
	}
	
	public void Quit()
	{
		//Debug.Log ("Quit");
		Application.Quit();
	}
}

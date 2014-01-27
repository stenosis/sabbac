using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState : MonoBehaviour
{
	private static GameState instance;
	private string level;
	private string nextLevel = null;
	private List<float> highscores;
	private int maxHighscores = 6;
	private int playgroundX;
	private int playgroundZ;
	private float highscore;
	private float startTime;
	private bool gameHasStarted;
	private GameObject maincamera;
	private GameObject endcamera;
	private GameObject ball;
	private GameObject gamearea;
	private List<GameObject> playground;
	private List<GameObject> exceptions;
	private bool newRandomLevel;
	private int difficultyLevel;
	private bool isIntro;
	
	private GameState() {}
	
	public static GameState Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new GameObject("GameState").AddComponent("GameState") as GameState;
			}
			return instance;
		}
	}
	
	public void InitLevel()
	{
		print("InitLevel");
		this.maincamera = GameObject.FindGameObjectWithTag("MainCamera");
		this.maincamera.SetActive(true);
		this.endcamera = GameObject.FindGameObjectWithTag("EndCamera");
		this.ball = GameObject.FindGameObjectWithTag("Ball");
		this.gamearea = GameObject.FindGameObjectWithTag("GameArea");
		this.endcamera.SetActive(false);
		this.newRandomLevel = false;
		this.level = Application.loadedLevelName;
		this.highscore = 0;
		this.gameHasStarted = false;
		this.isIntro = false;
	}
	
	public void LoadNextLevel()
	{
		if (string.IsNullOrEmpty(this.nextLevel))
		{
			this.newRandomLevel = true;
			Application.LoadLevel("Level1");
		}
		else
		{
			Application.LoadLevel(this.nextLevel);
		}
	}
	
	public void LoadHighscores()
	{
		int numberOfHighscores = PlayerPrefs.GetInt("NumberOfHighscores");
		this.highscores = new List<float>();
		for (int i = 0; i < numberOfHighscores; i++)
		{
			this.highscores.Add(PlayerPrefs.GetFloat("Highscore_" + (i + 1)));
		}
	}
	
	private void AddHighscore()
	{
		if (this.highscores == null)
		{
			LoadHighscores();
		}
		
		if (this.highscores.Count == 0)
		{
			this.highscores.Add(this.highscore);
		}
		else
		{
			for (int i = 0; i < this.highscores.Count; i++)
			{
				if (this.highscore <= this.highscores[i] 
					|| this.highscores.Count < this.maxHighscores)
				{
					this.highscores.Add(this.highscore);
					this.highscores.Sort();
					break;
				}
			}
		}
				
		if (this.highscores.Count > this.maxHighscores)
		{
			this.highscores.RemoveRange(this.maxHighscores, this.highscores.Count - this.maxHighscores);
		}
	}
	
	private void SaveHighscores()
	{
		if (this.highscores == null)
		{
			LoadHighscores();
		}
		
		for (int i = 0; i < this.highscores.Count; i++)
		{
			PlayerPrefs.SetFloat("Highscore_" + (i + 1), (float) this.highscores[i]);
		}
		PlayerPrefs.SetInt("NumberOfHighscores", this.highscores.Count);
	}
	
	public void LevelEnd()
	{
		(this.gamearea.gameObject.GetComponent("GameArea") as GameArea).InitLevelEnd();
		this.gameHasStarted = false;
		AddHighscore();
		SaveHighscores();		
		(this.endcamera.GetComponent("MainMenu") as MainMenu).LevelEnd();
	}
	
	public void LevelStart()
	{
		print("LevelStart");
		this.startTime = Time.time;
		this.gameHasStarted = true;
		this.isIntro = false;
		if (this.difficultyLevel != 0)
		{
			this.gamearea.gameObject.AddComponent("AnimateRandomBox");
		}
	}
	
	public void LevelPause()
	{
		(Camera.main.GetComponent("MouseOrbit") as MonoBehaviour).enabled = false;
		Time.timeScale = 0f;
	}
	
	public void LevelResume()
	{
		(Camera.main.GetComponent("MouseOrbit") as MonoBehaviour).enabled = true;
		Time.timeScale = 1f;
	}
	
	public void LevelRestart()
	{
		this.gameHasStarted = false;
		Application.LoadLevel(Application.loadedLevel);
	}
	
	public void GameQuit()
	{
		instance = null;
		SaveHighscores();
		Application.Quit();
	}
	
	public void GameOver()
	{
		(Camera.main.GetComponent("MouseOrbit") as MonoBehaviour).enabled = false;
		(Camera.main.GetComponent("MainMenu") as MainMenu).GameOver();
	}
	
	void OnApplicationQuit() 
	{
		if (instance != null)
		{
			GameQuit();
		}
	}
	
	public string GetLevel()
	{
		return this.level;
	}
	
	public void SetLevel(string newLevel)
	{
		this.level = newLevel;
	}
	
	public List<float> GetHighscores()
	{
		return this.highscores;
	}
	
	public float GetHighscore()
	{
		if (this.gameHasStarted)
		{
			this.highscore = Time.time - this.startTime;
		}
		
		return this.highscore;
	}
	
	public bool GetGameHasStarted()
	{
		return this.gameHasStarted;
	}
	
	public GameObject GetMainCamera()
	{
		return this.maincamera;
	}
	
	public GameObject GetEndCamera()
	{
		return this.endcamera;
	}
	
	public GameObject GetBall()
	{
		return this.ball;
	}
	
	public bool IsNewRandomLevel()
	{
		return this.newRandomLevel;
	}
	
	public void SetNextLevel(string nextLevel)
	{
		this.nextLevel = nextLevel;
	}
	
	public string GetNextLevel()
	{
		return this.nextLevel;
	}
	
	public void SetDifficultyOfCurrentLevel(int diff)
	{
		this.difficultyLevel = diff;
	}
	
	public int GetDifficultyOfCurrentLevel()
	{
		return this.difficultyLevel;
	}
	
	public bool GetIsIntro()
	{
		return this.isIntro;
	}
	
	public void SetIsIntro(bool isIntro)
	{
		this.isIntro = isIntro;
	}
	
	public List<GameObject> GetPlayground()
	{
		return this.playground;
	}
	
	public void SetPlayground(List<GameObject> playground)
	{
		this.playground = playground;
	}
	
	public List<GameObject> GetExceptions()
	{
		return this.exceptions;
	}
	
	public void SetExceptions(List<GameObject> exceptions)
	{
		this.exceptions = exceptions;
	}
	
	public void SetPlaygroundX (int x)
	{
		this.playgroundX = x;
	}
	
	public int GetPlaygroundX ()
	{
		return this.playgroundX;
	}
	
	public void SetPlaygroundZ (int z)
	{
		this.playgroundZ = z;
	}
	
	public int GetPlaygroundZ ()
	{
		return this.playgroundZ;
	}
}

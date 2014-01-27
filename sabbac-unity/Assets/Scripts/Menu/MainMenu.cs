using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
	public bool drawMenu = true;
	public Texture2D logo;
	public float logo_scale = 1.3f;
	public int posY = 1000;
	public Texture2D icon_highscore;
	public Texture2D icon_about;
	public GUISkin guiSkin;
	public bool showTime = true;
	
	// Übersetzungen
	private string str_start_game = "Start New Game";
	private string str_options = "Options";
	private string str_quit = "Quit";
	private string str_back = "Zurück";
	private string str_mainmenu = "Hauptmenü";
	private string str_restart = "Spiel neu starten";
	private string str_resume = "Spiel fortfahren";
	private string str_finishedlvl = "Level erfolgreich beendet";
	private string str_paused = "=Spiel pausiert=";
	private string str_gameover = "Game Over\nSie sind leider runtergefallen";
	private const string VERSIONR = "0.7 beta";
	
	// Menuzustände
	public int state = 0;
	private const int MAINMENU = 0;
	private const int HIGHSCORE = 1;
	private const int OPTIONS = 2;
	private const int CREDITS = 3;
	private const int GAMEOVER = 4;
	private const int LEVELEND = 5;
	private const int PAUSEGAME = 6;
	private const int INGAME = 7;
	
	// Use this for initialization
	void Start ()
	{
		// Create GameState start point
		DontDestroyOnLoad(GameState.Instance);	
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Press Esc for pause the game
		if (Input.GetKeyDown(KeyCode.Escape) && this.state != LEVELEND && !GameState.Instance.GetIsIntro()) 
		{
			if (this.state == PAUSEGAME)
			{
				this.state = INGAME;
				GameState.Instance.LevelResume();
			}
			else
			{
				this.state = PAUSEGAME;
				GameState.Instance.LevelPause();
			}
		}
	}
	
	// Draw GUI
	void OnGUI ()
	{
		GUI.skin = this.guiSkin;
		int fontSize = (int)(Screen.width * Screen.height * 0.000013f);
		GUI.skin.label.fontSize = GUI.skin.box.fontSize = GUI.skin.button.fontSize = fontSize;
		
		Texture2D texture = new Texture2D(1, 1);
	    texture.SetPixel(0,0, new Color(255,255,255,0.5f));
	    texture.Apply();
	    GUI.skin.box.normal.background = texture;
		
		if (this.drawMenu) 
		{
			switch (this.state) 
			{
			
			// state 0 = Main Menu
			case MAINMENU:
				
				Screen.showCursor = true;
				// Draw Logo
				float logo_w = (this.logo.width * (Screen.width * 0.00055f)) * this.logo_scale;
				//float logo_h = (this.logo.height * (Screen.height * 0.00077f)) * this.logo_scale;
				float logo_h = logo_w * 0.25f;
				float logo_x = (Screen.width / 2) - (logo_w / 2);
				float logo_y = (Screen.height * 100 / this.posY);
				GUI.DrawTexture (new Rect (logo_x, logo_y + Screen.height * 0.05f, logo_w, logo_h), this.logo);
				
				// Start Game Button
				float btn_x = (Screen.width / 2) * 0.6f;
				float btn_y = logo_y + (Screen.height * 0.4f);
				float btn_h = Screen.height * 0.08f;
				float btn_w = Screen.width / 2 - (Screen.width * 0.2f);
				if (GUI.Button (new Rect (btn_x, btn_y, btn_w + (Screen.width * 0.02f), btn_h), this.str_start_game)) 
				{
					this.state = INGAME;
					Application.LoadLevel("Level1");
				}
				
				// Highscore Button
				float btn_score_w = (Screen.width * 0.08f);
				float btn_score_x = btn_x + btn_w + (Screen.width * 0.02f);
				if (GUI.Button (new Rect (btn_score_x, btn_y, btn_score_w, btn_h), this.icon_highscore)) 
				{
					print ("Show Highscore");
					GameState.Instance.LoadHighscores();
					this.state = HIGHSCORE;
				}
				
				// Options Button
				float btn_pos_y2 = btn_y + (Screen.height * 0.1f);
				float btn_small_w = (btn_w / 1.5f) - Screen.width * 0.01f;		
				if (GUI.Button (new Rect (btn_x, btn_pos_y2, btn_small_w, btn_h), this.str_options))
				{
					print ("Show Options");
					this.state = OPTIONS;
				}
				
				// Quit Button
				float btn_pos_x2 = (Screen.width / 2) + (Screen.width * 0.01f);		
				if (GUI.Button (new Rect (btn_pos_x2, btn_pos_y2, btn_small_w, btn_h), this.str_quit)) 
				{
					print ("Quit Game");
					GameState.Instance.GameQuit();
				}
				
				// Credit Button
				float btn_credits_w = Screen.width * 0.07f;
				if (GUI.Button (new Rect (10, Screen.height - btn_h - 10, btn_credits_w, btn_h), this.icon_about)) 
				{
					print ("Show Credits");
					this.state = CREDITS;
				}
				
				// Version Number
				float vnr_x = Screen.width - Screen.width * 0.11f;
				float vnr_y = Screen.height - Screen.height * 0.06f;
				fontSize = (int)(Screen.width * Screen.height * 0.00002f);
				
				GUI.skin.label.fontSize = GUI.skin.box.fontSize = GUI.skin.button.fontSize = fontSize;
				GUI.Box (new Rect(vnr_x,vnr_y,100,100), "Ver: " + VERSIONR, new GUIStyle());
				
				break;
				
			// state 1 = Highscore
			case HIGHSCORE:
				
				Screen.showCursor = true;
				string txt_highscore = "\n\n\n\n";
				List<float> highscoreList = GameState.Instance.GetHighscores();
								
				// Set Highscorelist
				if(highscoreList.Count > 0) 
				{
					for (int i = 0; i < highscoreList.Count; i++)
					{
						txt_highscore += (i + 1) + ". " + (int) highscoreList[i] + " Sekunden\n";
					}
				} 
				else 
				{
					txt_highscore += "01. - Rickardo & Rulando\n" +
						"02. - HamSolo\n" +
						"03. - BaconImpsum\n" +
						"04. - BudLight\n" +
						"05. - QueenKong\n";
				}
								
				float box_w = Screen.width * 0.25f;
				float box_h = Screen.width * 0.35f;
				float box_x = Screen.width/2 - box_w/2;
				float box_y = Screen.height/2 - box_h/2;
				GUI.Box (new Rect (box_x, box_y, box_w, box_h), "=Highscore=" + txt_highscore);
				
				btn_w = Screen.width * 0.2f;
				btn_h = Screen.width * 0.06f;
				btn_x = Screen.width/2 - btn_w/2;
				btn_y = Screen.height/2 - btn_h/2 + box_h/2.6f;
				
				if (GUI.Button (new Rect(btn_x, btn_y, btn_w, btn_h), str_back)) 
				{
					this.state = MAINMENU;
				}
				break;
				
			// state 2 = Options
			case OPTIONS:
				
				Screen.showCursor = true;
				string txt_options = "\n\n\n\nNot yet implemented.";
				
				box_w = Screen.width * 0.25f;
				box_h = Screen.width * 0.35f;
				box_x = Screen.width/2 - box_w/2;
				box_y = Screen.height/2 - box_h/2;
				GUI.Box (new Rect (box_x, box_y, box_w, box_h), "=Options=" + txt_options);
				
				btn_w = Screen.width * 0.2f;
				btn_h = Screen.width * 0.06f;
				btn_x = Screen.width/2 - btn_w/2;
				btn_y = Screen.height/2 - btn_h/2 + box_h/2.6f;
				
				if (GUI.Button (new Rect(btn_x, btn_y, btn_w, btn_h), str_back)) 
				{
					this.state = MAINMENU;
				}
				
				break;
				
			// state 3 = Credits
			case CREDITS:
				
				Screen.showCursor = true;
				string txt_credits = "\n\n\n\nSPE - WS13/14 @ WHS\nJan Ruland\nTim Rieck\n" +
					"\n\nSong: Kevin McLeod -\nWillow and the Light\nReleased under CC-Licence";
				
				box_w = Screen.width * 0.25f;
				box_h = Screen.width * 0.35f;
				box_x = Screen.width/2 - box_w/2;
				box_y = Screen.height/2 - box_h/2;
				GUI.Box (new Rect (box_x, box_y, box_w, box_h), "=Credits=" + txt_credits);
				
				btn_w = Screen.width * 0.2f;
				btn_h = Screen.width * 0.06f;
				btn_x = Screen.width/2 - btn_w/2;
				btn_y = Screen.height/2 - btn_h/2 + box_h/2.6f;
				
				if (GUI.Button (new Rect(btn_x, btn_y, btn_w, btn_h), str_back)) 
				{
					this.state = MAINMENU;
				}
				break;
				
			// state 4 = Gameover
			case GAMEOVER:
				
				Screen.showCursor = true;
				box_w = Screen.width * 0.25f;
				box_h = Screen.width * 0.16f;
				box_x = Screen.width/2 - box_w/2;
				box_y = box_h/5;
				GUI.Box (new Rect (box_x, box_y, box_w, box_h), str_gameover);
				
				btn_w = Screen.width * 0.2f;
				btn_h = Screen.width * 0.06f;
				btn_x = Screen.width/2 - btn_w/2;
				btn_y = box_y + (box_h/1.6f);
								
				if (GUI.Button (new Rect(btn_x, btn_y - btn_h, btn_w, btn_h), str_restart)) 
				{
					GameState.Instance.LevelRestart();
				}
				
				if (GUI.Button (new Rect(btn_x, btn_y, btn_w, btn_h), str_mainmenu)) 
				{
					Time.timeScale = 1f;
					Application.LoadLevel("MainMenu");
				}
								
				break;
				
			// state 5 = Levelend
			case LEVELEND:
				
				Screen.showCursor = true;
				box_w = Screen.width * 0.3f;
				box_h = Screen.height * 0.075f;
				box_x = Screen.width/2 - box_w/2;
				box_y = Screen.height * 0.85f;
				
				string score = this.str_finishedlvl + "\nZeit: " + (int) GameState.Instance.GetHighscore() + " Sekunden\n";
				GUI.Box (new Rect(box_x, box_y, box_w, box_h), score);
				
				break;
	
			// state 6 = Pause game
			case PAUSEGAME:
				
				if (Application.loadedLevelName != "MainMenu")
				{
					Screen.showCursor = true;
					box_w = Screen.width * 0.25f;
					box_h = Screen.width * 0.18f;
					box_x = Screen.width/2 - box_w/2;
					box_y = Screen.height/2 - box_h/2;
					GUI.Box (new Rect (box_x, box_y, box_w, box_h), str_paused);
					
					btn_w = Screen.width * 0.2f;
					btn_h = Screen.width * 0.06f;
					btn_x = Screen.width/2 - btn_w/2;
					btn_y = Screen.height/2 - btn_h/2 + box_h/3.0f;
					
					if (GUI.Button (new Rect(btn_x, btn_y - btn_h, btn_w, btn_h), str_resume)) 
					{
						this.state = INGAME;
						GameState.Instance.LevelResume();
					}
					
					if (GUI.Button (new Rect(btn_x, btn_y, btn_w, btn_h), str_mainmenu)) 
					{
						Time.timeScale = 1f;
						Application.LoadLevel("MainMenu");
					}
				} 
				else 
				{
					this.state = MAINMENU;
				}
				
				break;
			
			// state 7 = InGame for HUD Elements
			case INGAME:
				
				Screen.showCursor = false;	
				// Show time
				if(this.showTime && this.state != MAINMENU && this.state != LEVELEND) 
				{

					int timestamp = (int) GameState.Instance.GetHighscore();
					string time = "Zeit: " + timestamp + " Sekunden";
					
					float width;
					if(timestamp < 100) {
						width = Screen.width * 0.13f;
					} else {
						width = Screen.width * 0.14f;
					}
					
					GUI.Box (new Rect(10,10,width,Screen.height * 0.048f), time);
									
				}
				break;
			}
		}
		
		// Reset the Gamestate
		if(this.state > INGAME) {
		
			this.state = MAINMENU;
		}
	}
	
	public void GameOver()
	{
		this.state = GAMEOVER;
		this.drawMenu = true;
		Time.timeScale = 0f;
	}
	
	public void LevelEnd()
	{
		this.state = LEVELEND;
		this.drawMenu = true;
	}
}
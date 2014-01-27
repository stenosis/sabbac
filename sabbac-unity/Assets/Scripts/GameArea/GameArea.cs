using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GameArea : MonoBehaviour
{
	[SerializeField]
	private int steps = 4;
	[SerializeField]
	private int minSize = 5;
	[SerializeField]
	private int maxSize = 15;
	[SerializeField]
	private List<GameObject> playground;
	[SerializeField]
	private List<GameObject> exceptions;
	[SerializeField]
	private int playgroundX;
	[SerializeField]
	private int playgroundZ;
	[SerializeField]
	private GameObject boxFence;
	[SerializeField]
	private GameObject boxFenceCorner;
	[SerializeField]
	private GameObject box;
	[SerializeField]
	private GameObject goal;
	[SerializeField]
	private GameObject splineRoot;
	[SerializeField]
	private GameObject[] obstacles;
	private bool editor;
	private string[] difficultyLevelNames = new string[] {"Stufe 1", "Stufe 2"};
	[SerializeField]
	private int difficultyLevel;
	[SerializeField]
	private string nextLevel = null;
	private bool levelEnded;
	
	void Start()
	{
		Screen.showCursor = false;
		Time.timeScale = 1f;
		this.editor = false;
		this.levelEnded = false;
		InitLevelByLevelStart();
	}
	
	void Update()
	{
		if (this.levelEnded && Input.GetKeyDown(KeyCode.Escape))
		{
			GameState.Instance.LoadNextLevel();
		}
	}
	
	private void InitLevelByLevelStart()
	{
		if (this.playground.Count == 0 || GameState.Instance.IsNewRandomLevel()) 
		{
			DeleteLevel();
			this.playgroundX = Random.Range(this.minSize, this.maxSize);
			this.playgroundZ = Random.Range(this.minSize, this.maxSize);
			this.difficultyLevel = Random.Range(0, 5);
			GeneratePlayground();
			GenerateSplineRoot();
		}
		GameState.Instance.InitLevel();
		GameState.Instance.SetPlaygroundX(this.playgroundX);
		GameState.Instance.SetPlaygroundZ(this.playgroundZ);
		GameState.Instance.SetPlayground(this.playground);
		GameState.Instance.SetExceptions(this.exceptions);
		GameState.Instance.SetDifficultyOfCurrentLevel(this.difficultyLevel);
		GameState.Instance.SetNextLevel(this.nextLevel);
		this.gameObject.AddComponent("AnimateLevelStart");
	}
	
	public void InitLevelByEditor()
	{
		this.editor = true;
		GeneratePlayground();
		GenerateSplineRoot();
	}
	
	public void InitLevelEnd()
	{
		GameObject startArea = GameObject.FindGameObjectWithTag("StartArea");
		startArea.SetActive(false);
		Destroy(this.gameObject.GetComponent("AnimateRandomBox"));
		this.gameObject.AddComponent("AnimateLevelEnd");
		this.levelEnded = true;
	}
	
	private void GenerateSplineRoot()
	{
		this.splineRoot.transform.GetChild(0).transform.position = new Vector3((float) (this.playgroundX * 4 + 8), 9f, (float) (this.playgroundZ * 4 + 4));
		this.splineRoot.transform.GetChild(0).transform.eulerAngles = new Vector3(21f, 238f, 1f);
		this.splineRoot.transform.GetChild(1).transform.position = new Vector3(-21f, 12f, -7f);
		this.splineRoot.transform.GetChild(1).transform.eulerAngles = new Vector3(21f, 74.32f, 1f);
		this.splineRoot.transform.GetChild(2).transform.position = new Vector3(-8f, 19f, (float) (this.playgroundZ * 4 + 4));
		this.splineRoot.transform.GetChild(2).transform.eulerAngles = new Vector3(35f, 138f, 1.2f);
		this.splineRoot.transform.GetChild(3).transform.position = new Vector3(12f, 4.5f, -37f);
		this.splineRoot.transform.GetChild(3).transform.eulerAngles = new Vector3(30f, 0f, 0f);
		this.splineRoot.transform.GetChild(4).transform.position = new Vector3((float) (this.playgroundX * 4 + 20), 22f, -5f);
		this.splineRoot.transform.GetChild(4).transform.eulerAngles = new Vector3(22f, 302f, 348f);
	}
	
	private void GeneratePlayground()
	{
		GameObject go;
		GameObject goal;
		GameObject obstacle;
		GameObject go_playground = GameObject.FindGameObjectWithTag("Playground");
		Vector3 position;
		Quaternion rotation;
		int stepX = 0;
		int stepZ = 0;
		int goalX = Random.Range(1, playgroundX - 1);
		int goalZ = Random.Range(playgroundZ - 2, playgroundZ - 1);
		int rdmMaterial;
		int rdmChoose;
		int positionInArray;
		BoxCollider collider = this.collider as BoxCollider;
		
		collider.center = new Vector3(this.playgroundX * 2, -50, this.playgroundZ * 2);
		collider.size = new Vector3(this.playgroundX * 4 + 80, 1, this.playgroundZ * 4 + 80);
		
		this.playground = new List<GameObject>();
		this.exceptions = new List<GameObject>();
		
		for (int z = 0; z < playgroundZ; z++) 
		{
			for (int x = 0; x < playgroundX; x++) 
			{
				position = new Vector3(transform.position.x + stepX, 0, transform.position.z + stepZ);
				rotation = transform.rotation;
				rdmMaterial = Random.Range(0,3);
				
				if ((z == 0) && (x == 0)) 
				{
					go = Instantiate(this.boxFenceCorner, position, rotation) as GameObject;
					go.transform.Rotate(new Vector3 (0, 90, 0));
				}
				else if ((z == 0) && (x == (playgroundX - 1))) 
				{
					go = Instantiate(this.boxFenceCorner, position, rotation) as GameObject;
					go.transform.Rotate(new Vector3 (0, 0, 0));
				} 
				else if ((z == (playgroundZ - 1)) && (x == 0)) 
				{
					go = Instantiate(this.boxFenceCorner, position, rotation) as GameObject;
					go.transform.Rotate(new Vector3 (0, 180, 0));
				} 
				else if ((z == (playgroundZ - 1)) && (x == (playgroundX - 1))) 
				{
					go = Instantiate(this.boxFenceCorner, position, rotation) as GameObject;
					go.transform.Rotate(new Vector3 (0, 270, 0));
				} 
				else if (z == 0) 
				{
					if ((transform.position.x + stepX) == 12) 
					{
						go = Instantiate(this.box, position, rotation) as GameObject;
						go.transform.Rotate(new Vector3 (0, 0, 0));
					} 
					else 
					{
						go = Instantiate(this.boxFence, position, rotation) as GameObject;
						go.transform.Rotate(new Vector3 (0, 0, 0));
					}
				} 
				else if (x == 0) 
				{
					go = Instantiate(this.boxFence, position, rotation) as GameObject;
					go.transform.Rotate(new Vector3 (0, 90, 0));
				} 
				else if (x == (playgroundX - 1)) 
				{
					go = Instantiate(this.boxFence, position, rotation) as GameObject;
					go.transform.Rotate(new Vector3 (0, 270, 0));
				} 
				else if (z == (playgroundZ - 1)) 
				{
					go = Instantiate(this.boxFence, position, rotation) as GameObject;
					go.transform.Rotate(new Vector3 (0, 180, 0));
				} 
				else 
				{
					go = Instantiate(this.box, position, rotation) as GameObject;
					go.transform.Rotate(new Vector3 (0, 0, 0));
				}
				
				if ((x == goalX) && (z == goalZ))
				{
					goal = Instantiate(this.goal, position, rotation) as GameObject;
					goal.transform.Rotate(new Vector3 (0, 0, 0));
					goal.transform.parent = go.transform.GetChild(0).transform.GetChild(0);
					this.exceptions.Add(go);
				}
				else
				{
					if (!((x == 3) && (z == 0)))
					{
						obstacle = GenerateObstacle(position, rotation);
						obstacle.transform.parent = go.transform.GetChild(0).transform.GetChild(0);
					}
					else
					{
						GameObject empty = new GameObject("empty");
						empty.transform.parent = go.transform.GetChild(0).transform.GetChild(0);
					}
				}
				
				if (editor)
				{
					if (rdmMaterial == 0)
					{
						go.transform.GetChild(0).transform.GetChild(0).renderer.sharedMaterial = Resources.Load("box", typeof(Material)) as Material;
					} 
					else if (rdmMaterial == 1)
					{
						go.transform.GetChild(0).transform.GetChild(0).renderer.sharedMaterial = Resources.Load("box2", typeof(Material)) as Material;
					}
					else if (rdmMaterial == 2)
					{
						go.transform.GetChild(0).transform.GetChild(0).renderer.sharedMaterial = Resources.Load("box3", typeof(Material)) as Material;
					}
				}
				else
				{
					if (rdmMaterial == 0)
					{
						go.transform.GetChild(0).transform.GetChild(0).renderer.material = Resources.Load("box", typeof(Material)) as Material;
					} 
					else if (rdmMaterial == 1)
					{
						go.transform.GetChild(0).transform.GetChild(0).renderer.material = Resources.Load("box2", typeof(Material)) as Material;
					}
					else if (rdmMaterial == 2)
					{
						go.transform.GetChild(0).transform.GetChild(0).renderer.material = Resources.Load("box3", typeof(Material)) as Material;
					}
				}
				
				go.transform.parent = go_playground.transform;
				go.tag = "Box";
				this.playground.Add(go);
				stepX = stepX + steps;
			}

			stepX = 0;
			stepZ = stepZ + steps;
			
		}
		
		if (this.difficultyLevel > 0 && this.playgroundZ >= 7)
		{
			rdmChoose = Random.Range(0, 2);
			if (rdmChoose == 1)
			{
				positionInArray = (goalZ * this.playgroundX) + goalX - this.playgroundX;
				go = this.playground[positionInArray];
				obstacle = go.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject;
				
				if (obstacle.name != "empty(Clone)")
				{
					DestroyImmediate(obstacle);
					GameObject empty = new GameObject("empty");
					empty.transform.parent = go.transform.GetChild(0).transform.GetChild(0);
				}
				
				positionInArray = positionInArray - this.playgroundX;
				go = this.playground[positionInArray];
				obstacle = go.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject;
				
				if (obstacle.name != "empty(Clone)")
				{
					DestroyImmediate(obstacle);
					GameObject empty = new GameObject("empty");
					empty.transform.parent = go.transform.GetChild(0).transform.GetChild(0);
				}
				
				positionInArray = positionInArray - this.playgroundX;
				go = this.playground[positionInArray];
				obstacle = go.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject;
				
				if (obstacle.name != "empty(Clone)")
				{
					DestroyImmediate(obstacle);
					GameObject empty = new GameObject("empty");
					empty.transform.parent = go.transform.GetChild(0).transform.GetChild(0);
				}
				
				positionInArray = positionInArray - this.playgroundX;
				go = this.playground[positionInArray];
				obstacle = go.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject;
				
				if (obstacle.name != "empty(Clone)")
				{
					DestroyImmediate(obstacle);
					GameObject empty = new GameObject("empty");
					empty.transform.parent = go.transform.GetChild(0).transform.GetChild(0);
				}
				
				go.AddComponent("AnimateGoalByTrigger");
			}
		}		
	}
	
	private GameObject GenerateObstacle(Vector3 position, Quaternion rotation)
	{
		int rdm = Random.Range(0, this.obstacles.Length);
		int rdmRotateZ = Random.Range(0,361);
		int rdmMaterial = Random.Range(0,2);
		GameObject obstacle = Instantiate(this.obstacles[rdm], position, rotation) as GameObject;
		if (editor)
		{
			if (rdmMaterial == 0 && obstacle.name != "empty(Clone)")
			{
				obstacle.renderer.sharedMaterial = Resources.Load("chess_white", typeof(Material)) as Material;
		}
			else if (rdmMaterial == 1 && obstacle.name != "empty(Clone)")
			{
				obstacle.renderer.sharedMaterial = Resources.Load("chess_black", typeof(Material)) as Material;
			}
		}
		else
		{
			if (rdmMaterial == 0 && obstacle.name != "empty(Clone)")
			{
				obstacle.renderer.material = Resources.Load("chess_white", typeof(Material)) as Material;
		}
			else if (rdmMaterial == 1 && obstacle.name != "empty(Clone)")
			{
				obstacle.renderer.material = Resources.Load("chess_black", typeof(Material)) as Material;
			}
		}
		
		
		obstacle.transform.Rotate(new Vector3 (-90, 0, rdmRotateZ));
		
		return obstacle;
	}
	
	public void DeleteLevel()
	{
		List<GameObject> deleteObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Box"));
		
		foreach (GameObject go in deleteObjects) 
		{
			DestroyImmediate(go);
		}
		this.splineRoot.transform.GetChild(0).transform.eulerAngles = new Vector3(0f, 0f, 0f);
		this.splineRoot.transform.GetChild(1).transform.eulerAngles = new Vector3(0f, 0f, 0f);
		this.splineRoot.transform.GetChild(2).transform.eulerAngles = new Vector3(0f, 0f, 0f);
		this.splineRoot.transform.GetChild(3).transform.eulerAngles = new Vector3(0f, 0f, 0f);
		this.splineRoot.transform.GetChild(4).transform.eulerAngles = new Vector3(0f, 0f, 0f);
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
	
	public void SetBoxFence (GameObject box)
	{
		this.boxFence = box;
	}
	
	public GameObject GetBoxFence ()
	{
		return this.boxFence;
	}
	
	public void SetBox (GameObject box)
	{
		this.box = box;
	}
	
	public GameObject GetBox ()
	{
		return this.box;
	}
	
	public void SetBoxFenceCorner (GameObject box)
	{
		this.boxFenceCorner = box;
	}
	
	public GameObject GetBoxFenceCorner ()
	{
		return this.boxFenceCorner;
	}
	
	public void SetGoal (GameObject goal)
	{
		this.goal = goal;
	}
	
	public GameObject GetGoal ()
	{
		return this.goal;
	}
	
	public void SetSplineRoot(GameObject splineRoot)
	{
		this.splineRoot = splineRoot;
	}
	
	public GameObject GetSplineRoot()
	{
		return this.splineRoot;
	}
	
	public GameObject[] GetObstacles()
	{
		return this.obstacles;
	}
	
	public void SetObstacles(GameObject[] obstacles)
	{
		this.obstacles = obstacles;
	}
	
	public void SetObstacle(GameObject obstacle, int index)
	{
		this.obstacles[index] = obstacle;
	}
	
	public GameObject GetObstacle(int index)
	{
		return this.obstacles[index];
	}
	
	public string[] GetDifficultyLevelNames()
	{
		return this.difficultyLevelNames;
	}
	
	public int GetDifficultyLevel()
	{
		return this.difficultyLevel;
	}
	
	public void SetDifficultyLevel(int diff)
	{
		this.difficultyLevel = diff;
	}
	
	public void SetNextLevel(string nextLevel)
	{
		this.nextLevel = nextLevel;
	}
	
	public string GetNextLevel()
	{
		return this.nextLevel;
	}
}

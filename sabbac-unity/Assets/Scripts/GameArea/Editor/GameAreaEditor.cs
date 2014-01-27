using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

[CustomEditor(typeof(GameArea))]
public class GameAreaEditor : Editor
{
	private GameArea gameArea;
	private bool showObstacles;
	
	void Awake()
	{
		this.gameArea = target as GameArea;
		if (this.gameArea.gameObject.activeInHierarchy)
		{
			PrefabUtility.DisconnectPrefabInstance(this.gameArea.gameObject);
		}
		this.showObstacles = false;
	}
	
	public override void OnInspectorGUI()
	{			
		EditorGUILayout.BeginVertical();
		
		EditorGUILayout.LabelField("Spielfeld");
		this.gameArea.SetPlaygroundX(EditorGUILayout.IntField("Groesse X", this.gameArea.GetPlaygroundX()));
		this.gameArea.SetPlaygroundZ(EditorGUILayout.IntField("Groesse Z", this.gameArea.GetPlaygroundZ()));
		
		EditorGUILayout.EndVertical();
		
		EditorGUILayout.Space();
		
		EditorGUILayout.BeginVertical();
		EditorGUILayout.LabelField("Schwierigkeit");
		this.gameArea.SetDifficultyLevel(GUI.SelectionGrid(new Rect(20, 300, 65, 40), this.gameArea.GetDifficultyLevel(), this.gameArea.GetDifficultyLevelNames(), 1, EditorStyles.radioButton));
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		
		EditorGUILayout.EndVertical();
		
		EditorGUILayout.Space();
		
		EditorGUILayout.BeginVertical();
		EditorGUILayout.LabelField("Naechstes Level");
		this.gameArea.SetNextLevel(EditorGUILayout.TextField("Name der Szene", this.gameArea.GetNextLevel()));
		
		EditorGUILayout.EndVertical();
		
		EditorGUILayout.Space();
		
		EditorGUILayout.BeginVertical();
		
		EditorGUILayout.LabelField("Kamerapfad");
		
		this.gameArea.SetSplineRoot(EditorGUILayout.ObjectField("Spline Root", this.gameArea.GetSplineRoot(), typeof(GameObject), true) as GameObject);
		
		EditorGUILayout.EndVertical();
		
		EditorGUILayout.Space();
		
		EditorGUILayout.BeginVertical();
		EditorGUILayout.LabelField("Bausteine");
		
		this.gameArea.SetBox(EditorGUILayout.ObjectField("Box", this.gameArea.GetBox(), typeof(GameObject), true) as GameObject);
		this.gameArea.SetBoxFence(EditorGUILayout.ObjectField("Box mit Zaun", this.gameArea.GetBoxFence(), typeof(GameObject), true) as GameObject);
		this.gameArea.SetBoxFenceCorner(EditorGUILayout.ObjectField("Box mit Eckzaun", this.gameArea.GetBoxFenceCorner(), typeof(GameObject), true) as GameObject);
		this.gameArea.SetGoal(EditorGUILayout.ObjectField("Ziel", this.gameArea.GetGoal(), typeof(GameObject), true) as GameObject);
		
		EditorGUILayout.EndVertical();
		
		EditorGUILayout.Space();
		
		EditorGUILayout.BeginVertical();
		
		this.showObstacles = EditorGUILayout.Foldout(this.showObstacles, "Hindernisse");
		
		if (this.showObstacles)
		{
			int length = EditorGUILayout.IntField("Anzahl Hindernisse", this.gameArea.GetObstacles().Length);
			GameObject[] newObstacles = new GameObject[length];
			int smallestLength = (this.gameArea.GetObstacles().Length <= newObstacles.Length) 
				?  this.gameArea.GetObstacles().Length 
					: newObstacles.Length;
			Array.Copy(this.gameArea.GetObstacles(), 0, newObstacles, 0, smallestLength);
			this.gameArea.SetObstacles(newObstacles);
		
			for (int i = 0; i < length; i++)
			{
				this.gameArea.SetObstacle(EditorGUILayout.ObjectField("Hindernis " + (i + 1), this.gameArea.GetObstacle(i), typeof(GameObject), true) as GameObject, i);
			}
		}
				
		EditorGUILayout.EndVertical();
		
		EditorGUILayout.Space();
		
		EditorGUILayout.BeginHorizontal();
		
		if (GUILayout.Button("Level generieren"))
		{
			this.gameArea.DeleteLevel();
			this.gameArea.InitLevelByEditor();
		}
		
		if (GUILayout.Button("Level loeschen"))
		{
			this.gameArea.DeleteLevel();
		}
		
		EditorGUILayout.EndHorizontal();
	}
}

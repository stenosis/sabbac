﻿using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	void OnTriggerEnter (Collider other)
	{
		GameState.Instance.GameOver();
	}
}

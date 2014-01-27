using UnityEngine;
using System.Collections;

public class RisingPath : MonoBehaviour
{
	
	public GameObject go;

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
		go.SetActive(true);
	}
	
}

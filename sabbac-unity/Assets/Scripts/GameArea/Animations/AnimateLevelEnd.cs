using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimateLevelEnd : MonoBehaviour {
	
	private List<GameObject> playground;
	
	// Use this for initialization
	void Start ()
	{
		this.playground = GameState.Instance.GetPlayground();
		StartCoroutine(Animate());
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			GameState.Instance.LoadNextLevel();
		}
	}
	
	private IEnumerator Animate()
	{
		int randomNumber;
		int randomSelect;
		GameObject go;
		List<GameObject> exceptions = GameState.Instance.GetExceptions();

		while (this.playground.Count > 1)
		{
			randomNumber = Random.Range(0, this.playground.Count);
			randomSelect = Random.Range(0, 2);
			go = this.playground[randomNumber] as GameObject;
			
			if (!exceptions.Contains(go))
			{
				switch (randomSelect)
				{
				case 0:
					go.transform.GetChild(0).animation["Fallen"].speed = -1;
					go.transform.GetChild(0).animation["Fallen"].time = go.transform.GetChild(0).animation["Fallen"].length;
					go.transform.GetChild(0).animation.Play("Fallen");
					this.playground.RemoveAt(randomNumber);
					
					break;
				
				case 1:
					go.transform.GetChild(0).animation["Steigen"].speed = -1;
					go.transform.GetChild(0).animation["Steigen"].time = go.transform.GetChild(0).animation["Steigen"].length;
					go.transform.GetChild(0).animation.Play("Steigen");
					this.playground.RemoveAt(randomNumber);
					
					break;
				}
				
				StartCoroutine(DeaktivateBox(go));
				
			}
			else
			{
				if (go.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).name != "goal(Clone)")
				{
					go.transform.GetChild(0).animation["Fallen"].speed = -1;
					this.playground.RemoveAt(randomNumber);
					StartCoroutine(DeaktivateBox(go));
				}
			}
			
			yield return new WaitForSeconds(0.08f);
			
		}
		
		yield return new WaitForSeconds(10f);
		GameState.Instance.LoadNextLevel();
	}
	
	private IEnumerator DeaktivateBox(GameObject go)
	{
		yield return new WaitForSeconds(go.transform.GetChild(0).animation.clip.length);
		go.SetActive(false);
	}
}

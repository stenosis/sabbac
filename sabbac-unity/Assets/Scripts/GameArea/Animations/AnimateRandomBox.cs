using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimateRandomBox : MonoBehaviour 
{
	private List<GameObject> playground;
	private int standardTime;
	
	// Use this for initialization
	void Start ()
	{
		this.playground = GameState.Instance.GetPlayground();
		this.standardTime = 6;
		StartCoroutine(Animate());
	}
	
	public IEnumerator Animate()
	{
		int randomNumber;
		int randomSelect;
		float time = (float) (this.standardTime / (this.playground.Count / 4));
		GameObject go;
		
		yield return new WaitForSeconds(4f);
		
		while (true)
		{
			randomNumber = Random.Range(0, playground.Count);
			randomSelect = Random.Range(0, 2);
			go = playground[randomNumber] as GameObject;
			
			while (go.transform.GetChild(0).animation.isPlaying || GameState.Instance.GetExceptions().Contains(go))
			{
				randomNumber = Random.Range(0, playground.Count);
				go = playground[randomNumber] as GameObject;
			}
						
			switch (randomSelect)
			{
			case 0:
				go.transform.GetChild(0).animation["Fallen"].speed = -1;
				go.transform.GetChild(0).animation["Fallen"].time = go.transform.GetChild(0).animation["Fallen"].length;
				go.transform.GetChild(0).animation.Play("Fallen");

				yield return new WaitForSeconds(go.transform.GetChild(0).animation.clip.length);
				
				go.transform.GetChild(0).animation["Fallen"].speed = 1;
				go.transform.GetChild(0).animation["Fallen"].time = 0;
				go.transform.GetChild(0).animation.Play("Fallen");
				
				break;
			
			case 1:
				go.transform.GetChild(0).animation["Steigen"].speed = -1;
				go.transform.GetChild(0).animation["Steigen"].time = go.transform.GetChild(0).animation["Steigen"].length;
				go.transform.GetChild(0).animation.Play("Steigen");

				yield return new WaitForSeconds(go.transform.GetChild(0).animation.clip.length);
				
				go.transform.GetChild(0).animation["Steigen"].speed = 1;
				go.transform.GetChild(0).animation["Steigen"].time = 0;
				go.transform.GetChild(0).animation.Play("Steigen");
				
				break;
			}				
			
			yield return new WaitForSeconds(time);
			
		}
	}
}

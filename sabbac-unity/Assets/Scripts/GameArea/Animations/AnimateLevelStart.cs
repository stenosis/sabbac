using UnityEngine;
using System.Collections;

public class AnimateLevelStart : MonoBehaviour 
{
	private ArrayList playground;
	private float timeToWait = 0.02f;
	
	// Use this for initialization
	void Start () 
	{
		GameState.Instance.SetIsIntro(true);
		showDuckRandom();
		this.playground = new ArrayList(GameObject.FindGameObjectsWithTag("Box"));
		foreach (GameObject go in this.playground) 
		{
			go.SetActive (false);
		}
		StartCoroutine(Animate());
	}
	
	private IEnumerator Animate()
	{
		int randomNumber;
		GameObject go;
		
		while (this.playground.Count > 0) 
		{
			randomNumber = Random.Range(0, this.playground.Count - 1);
			go = this.playground[randomNumber] as GameObject;
			go.SetActive(true);
			this.playground.RemoveAt(randomNumber);
			yield return new WaitForSeconds(this.timeToWait);
		}
	}
	
	private void showDuckRandom() {
	
		GameObject duck = GameObject.FindGameObjectWithTag("Duck");
		duck.SetActive(false);
		int count = Random.Range(0, 100);
		print (count);
		
		if(count == 42) {
			duck.SetActive(true);
		}	
	}
}

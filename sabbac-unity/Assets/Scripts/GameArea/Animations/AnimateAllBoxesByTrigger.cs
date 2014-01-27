using UnityEngine;
using System.Collections;

public class AnimateAllBoxesByTrigger : MonoBehaviour 
{
	private ArrayList playground;
	
	// Use this for initialization
	void Start () 
	{
		BoxCollider collider = this.gameObject.AddComponent("BoxCollider") as BoxCollider;
		this.playground = new ArrayList(GameObject.FindGameObjectsWithTag("Box"));
		collider.isTrigger = true;
		collider.center = new Vector3(0f, 4f, 0f);
		collider.size = new Vector3(4f, 8f, 4f);
	}
	
	void OnTriggerEnter(Collider other)
	{
		StartCoroutine(Animate());
	}
	
	private IEnumerator Animate()
	{
		int randomNumber;
		int randomSelect;
		GameObject go;
				
		while (this.playground.Count > 1)
		{
			randomNumber = Random.Range(0, this.playground.Count);
			randomSelect = Random.Range(0, 2);
			go = this.playground[randomNumber] as GameObject;
			
			if (go.Equals(this.gameObject))
			{
				this.playground.RemoveAt(randomNumber);
				randomNumber = Random.Range(0, this.playground.Count);
				go = this.playground[randomNumber] as GameObject;
			}
			
			while (go.transform.GetChild(0).animation.isPlaying)
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
				
//				yield return new WaitForSeconds(go.transform.GetChild(0).animation.clip.length);
//				
//				go.transform.GetChild(0).animation["Fallen"].speed = 1;
//				go.transform.GetChild(0).animation["Fallen"].time = 0;
//				go.transform.GetChild(0).animation.Play("Fallen");
			
				this.playground.RemoveAt(randomNumber);
				
				break;
			
			case 1:
				go.transform.GetChild(0).animation["Steigen"].speed = -1;
				go.transform.GetChild(0).animation["Steigen"].time = go.transform.GetChild(0).animation["Steigen"].length;
				go.transform.GetChild(0).animation.Play("Steigen");
				
//				yield return new WaitForSeconds(go.transform.GetChild(0).animation.clip.length);
//				
//				go.transform.GetChild(0).animation["Steigen"].speed = 1;
//				go.transform.GetChild(0).animation["Steigen"].time = 0;
//				go.transform.GetChild(0).animation.Play("Steigen");
				
				this.playground.RemoveAt(randomNumber);
				
				break;
			}
			
			yield return new WaitForSeconds(0.05f);
			
		}
	}
}

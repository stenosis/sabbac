using UnityEngine;
using System.Collections;

public class AnimateBoxByTrigger : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		BoxCollider collider = this.gameObject.AddComponent("BoxCollider") as BoxCollider;
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
		yield return new WaitForSeconds(0.5f);

		this.transform.GetChild(0).animation["Fallen"].speed = -0.5f;
		this.transform.GetChild(0).animation["Fallen"].time = this.transform.GetChild(0).animation["Fallen"].length;
		this.transform.GetChild(0).animation.Play("Fallen");

		yield return new WaitForSeconds(this.transform.GetChild(0).animation.clip.length + 1f);

		this.transform.GetChild(0).animation["Fallen"].speed = 0.5f;
		this.transform.GetChild(0).animation["Fallen"].time = 0;
		this.transform.GetChild(0).animation.Play("Fallen");
	}
}

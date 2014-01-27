using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimateGoalByTrigger : MonoBehaviour 
{
	
	private int i;
	private bool triggerd;
	private List<GameObject> playground;
	
	// Use this for initialization
	void Start () 
	{
		BoxCollider collider = this.gameObject.AddComponent("BoxCollider") as BoxCollider;
		List<GameObject> exceptions = new List<GameObject>();
		this.playground = GameState.Instance.GetPlayground();
		this.triggerd = false;
		collider.isTrigger = true;
		collider.center = new Vector3(0f, 4f, 2f);
		collider.size = new Vector3(12f, 8f, 16f);

		for (this.i = 0; this.i < this.playground.Count; this.i++)
		{
			if (this.playground[this.i].Equals(this.gameObject))
			{
				break;
			}
		}
		
		exceptions.Add(this.playground[this.i + GameState.Instance.GetPlaygroundX()]);
		exceptions.Add(this.playground[this.i + GameState.Instance.GetPlaygroundX() + GameState.Instance.GetPlaygroundX()]);
		exceptions.Add(this.playground[this.i + GameState.Instance.GetPlaygroundX() + GameState.Instance.GetPlaygroundX() + GameState.Instance.GetPlaygroundX()]);
		exceptions.Add(this.playground[this.i + GameState.Instance.GetPlaygroundX() + GameState.Instance.GetPlaygroundX() + GameState.Instance.GetPlaygroundX() - 1]);
		exceptions.Add(this.playground[this.i + GameState.Instance.GetPlaygroundX() + GameState.Instance.GetPlaygroundX() + GameState.Instance.GetPlaygroundX() + 1]);
		exceptions.Add(this.playground[this.i + GameState.Instance.GetPlaygroundX() + GameState.Instance.GetPlaygroundX() + GameState.Instance.GetPlaygroundX() + GameState.Instance.GetPlaygroundX() - 1]);
		exceptions.Add(this.playground[this.i + GameState.Instance.GetPlaygroundX() + GameState.Instance.GetPlaygroundX() + GameState.Instance.GetPlaygroundX() + GameState.Instance.GetPlaygroundX()]);
		exceptions.Add(this.playground[this.i + GameState.Instance.GetPlaygroundX() + GameState.Instance.GetPlaygroundX() + GameState.Instance.GetPlaygroundX() + GameState.Instance.GetPlaygroundX() + 1]);
		exceptions.Add(this.playground[this.i + GameState.Instance.GetPlaygroundX() + GameState.Instance.GetPlaygroundX() + GameState.Instance.GetPlaygroundX() + GameState.Instance.GetPlaygroundX() + GameState.Instance.GetPlaygroundX() - 1]);
		exceptions.Add(this.playground[this.i + GameState.Instance.GetPlaygroundX() + GameState.Instance.GetPlaygroundX() + GameState.Instance.GetPlaygroundX() + GameState.Instance.GetPlaygroundX() + GameState.Instance.GetPlaygroundX()]);
		exceptions.Add(this.playground[this.i + GameState.Instance.GetPlaygroundX() + GameState.Instance.GetPlaygroundX() + GameState.Instance.GetPlaygroundX() + GameState.Instance.GetPlaygroundX() + GameState.Instance.GetPlaygroundX() + 1]);
		
		GameState.Instance.SetExceptions(exceptions);
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (!this.triggerd)
		{
			StartCoroutine(Animate());
		}
	}
	
	private IEnumerator Animate()
	{
		
		GameObject go;
		GameObject go2;
		GameObject go3;
		GameObject go4;
		GameObject go5;
		GameObject go6;
		GameObject go7;
		GameObject go8;
		GameObject go9;
		
		this.triggerd = true;
		go = GameState.Instance.GetExceptions()[0];
		go.transform.GetChild(0).animation["Fallen"].speed = -1;
		go.transform.GetChild(0).animation["Fallen"].time = go.transform.GetChild(0).animation["Fallen"].length;
		go.transform.GetChild(0).animation.Play("Fallen");
				
		yield return new WaitForSeconds(0.8f);
		go.transform.GetChild(0).animation["Fallen"].speed = 0;
		
		go = GameState.Instance.GetExceptions()[1];
		go.transform.GetChild(0).animation["Fallen"].speed = -1;
		go.transform.GetChild(0).animation["Fallen"].time = go.transform.GetChild(0).animation["Fallen"].length;
		go.transform.GetChild(0).animation.Play("Fallen");
		
		yield return new WaitForSeconds(1f);
		go.transform.GetChild(0).animation["Fallen"].speed = 0;
		
		go = GameState.Instance.GetExceptions()[2];
		go.transform.GetChild(0).animation["Fallen"].speed = -1;
		go.transform.GetChild(0).animation["Fallen"].time = go.transform.GetChild(0).animation["Fallen"].length;
		go.transform.GetChild(0).animation.Play("Fallen");
				
		go2 = GameState.Instance.GetExceptions()[3];
		go2.transform.GetChild(0).animation["Fallen"].speed = -1;
		go2.transform.GetChild(0).animation["Fallen"].time = go2.transform.GetChild(0).animation["Fallen"].length;
		go2.transform.GetChild(0).animation.Play("Fallen");
		
		go3 = GameState.Instance.GetExceptions()[4];
		go3.transform.GetChild(0).animation["Fallen"].speed = -1;
		go3.transform.GetChild(0).animation["Fallen"].time = go3.transform.GetChild(0).animation["Fallen"].length;
		go3.transform.GetChild(0).animation.Play("Fallen");
		
		go4 = GameState.Instance.GetExceptions()[5];
		go4.transform.GetChild(0).animation["Fallen"].speed = -1;
		go4.transform.GetChild(0).animation["Fallen"].time = go4.transform.GetChild(0).animation["Fallen"].length;
		go4.transform.GetChild(0).animation.Play("Fallen");
		
		go5 = GameState.Instance.GetExceptions()[6];
		go5.transform.GetChild(0).animation["Fallen"].speed = -1;
		go5.transform.GetChild(0).animation["Fallen"].time = go5.transform.GetChild(0).animation["Fallen"].length;
		go5.transform.GetChild(0).animation.Play("Fallen");
		
		go6 = GameState.Instance.GetExceptions()[7];
		go6.transform.GetChild(0).animation["Fallen"].speed = -1;
		go6.transform.GetChild(0).animation["Fallen"].time = go6.transform.GetChild(0).animation["Fallen"].length;
		go6.transform.GetChild(0).animation.Play("Fallen");
		
		go7 = GameState.Instance.GetExceptions()[8];
		go7.transform.GetChild(0).animation["Fallen"].speed = -1;
		go7.transform.GetChild(0).animation["Fallen"].time = go7.transform.GetChild(0).animation["Fallen"].length;
		go7.transform.GetChild(0).animation.Play("Fallen");
		
		go8 = GameState.Instance.GetExceptions()[9];
		go8.transform.GetChild(0).animation["Fallen"].speed = -1;
		go8.transform.GetChild(0).animation["Fallen"].time = go8.transform.GetChild(0).animation["Fallen"].length;
		go8.transform.GetChild(0).animation.Play("Fallen");
		
		go9 = GameState.Instance.GetExceptions()[10];
		go9.transform.GetChild(0).animation["Fallen"].speed = -1;
		go9.transform.GetChild(0).animation["Fallen"].time = go9.transform.GetChild(0).animation["Fallen"].length;
		go9.transform.GetChild(0).animation.Play("Fallen");
		
		yield return new WaitForSeconds(1.13f);
		
		go.transform.GetChild(0).animation["Fallen"].speed = 0;
		go2.transform.GetChild(0).animation["Fallen"].speed = 0;
		go3.transform.GetChild(0).animation["Fallen"].speed = 0;
		go4.transform.GetChild(0).animation["Fallen"].speed = 0;
		go5.transform.GetChild(0).animation["Fallen"].speed = 0;
		go6.transform.GetChild(0).animation["Fallen"].speed = 0;
		go7.transform.GetChild(0).animation["Fallen"].speed = 0;
		go8.transform.GetChild(0).animation["Fallen"].speed = 0;
		go9.transform.GetChild(0).animation["Fallen"].speed = 0;
	}
}

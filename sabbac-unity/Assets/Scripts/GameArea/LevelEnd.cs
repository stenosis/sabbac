using UnityEngine;
using System.Collections;

public class LevelEnd : MonoBehaviour 
{
	private bool levelEnd;
	public float aniFlagSpeed = 0.3f;
	public GameObject firework1;
	public GameObject firework2;
	public GameObject firework3;
	public AudioClip fireworkSound;
	
	// Use this for initialization
	void Start () 
	{
		this.levelEnd = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (this.firework1.particleEmitter.particleCount == 0)
		{
			audio.clip = this.fireworkSound;
			audio.Play();
		}
	}
	
	void OnTriggerStay(Collider other) 
	{
		GameObject maincamera = GameState.Instance.GetMainCamera();
		GameObject endcamera = GameState.Instance.GetEndCamera();
		GameObject ball = GameState.Instance.GetBall();
		
		if ((other.rigidbody.velocity.magnitude == 0.0) && !levelEnd)
		{
			maincamera.SetActive(false);
			endcamera.SetActive(true);
			(ball.transform.GetChild(0).GetComponent("MoveBall") as MonoBehaviour).enabled = false;
			levelEnd = true;
			GameState.Instance.LevelEnd();
			this.gameObject.animation["FahneHissen"].speed = this.aniFlagSpeed;
			this.gameObject.animation.Play("FahneHissen");
			
			this.firework1.particleEmitter.emit = true;
			this.firework2.particleEmitter.emit = true;
			this.firework3.particleEmitter.emit = true;
		}
	}
}

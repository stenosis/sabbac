using UnityEngine;
using System.Collections;

public class MoveBall : MonoBehaviour
{
	public GameObject cameraObject;
	public int moveForce = 200;
	public int jumpForce = 400;
	public AudioClip collisionSound;
	public AudioClip jumpSound;
	private float distanceToGround;
	
	// Use this for initialization
	void Start()
	{
		this.distanceToGround = rigidbody.collider.bounds.extents.y;
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
		if (((this.cameraObject.GetComponent("MouseOrbit") as MonoBehaviour).enabled))
		{
			if (IsGrounded())
			{
				if (Input.GetButtonDown("Horizontal"))
				{
					if (Input.GetAxis("Horizontal") > 0)
					{
						rigidbody.AddForce(this.cameraObject.transform.parent.transform.right, ForceMode.Impulse);
					}
					else if (Input.GetAxis("Horizontal") < 0)
					{
						rigidbody.AddForce(-this.cameraObject.transform.parent.transform.right, ForceMode.Impulse);
					}
				}
				
				if (Input.GetButtonDown("Vertical"))
				{
					if (Input.GetAxis("Vertical") > 0)
					{
						rigidbody.AddForce(this.cameraObject.transform.parent.transform.forward, ForceMode.Impulse);
					}
					else if (Input.GetAxis("Vertical") < 0)
					{
						rigidbody.AddForce(-this.cameraObject.transform.parent.transform.forward, ForceMode.Impulse);
					}
				}
				
//				if (Input.GetButtonDown("Fire1"))
//				{
//					rigidbody.AddForce(this.cameraObject.transform.forward * this.moveForce);
//				}
				
				if (Input.GetButtonDown("Jump"))
				{
					rigidbody.AddForce(this.transform.parent.up * this.jumpForce);
					audio.PlayOneShot(this.jumpSound);
				}
			}
			
		}		
	}
	
	/**
	 * Prueft, ob der Ball sich am Boden befindet.
	 */ 
	private bool IsGrounded()
	{
		return Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.1f);
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if ((collision.gameObject.name != "Box") && (collision.gameObject.name != "box_ohneZaun"))
		{
			audio.PlayOneShot(this.collisionSound);
		}
	}
}

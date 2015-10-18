using UnityEngine;
using System.Collections;

public class AcrobatPlayer : Player
{
	private bool canAirJump = true;
	private bool canWallJump = true;
	private bool onWall = false;
	private float timer;

	public override void Start()
	{
		//First player in gets Acrobat, everyone after gets Strong.
		if(GameObject.FindGameObjectsWithTag("Player").Length > 1)
		{
			gameObject.GetComponent<StrongPlayer>().enabled = true;
			enabled = false;
		}
		base.Start ();
	}
	
	// Update is called once per frame
	public override void Update ()
	{
		canAirJump = onGround || canAirJump;
		if(onGround) canWallJump = true;
//		Debug.Log (charControl.collisionFlags);
//		if((charControl.collisionFlags & CollisionFlags.Sides) != 0)
//		{
//			Debug.Log("Test");
//			canWallJump = true;
//		}
		//Debug.Log("OnWall: "  + onWall);
		//Debug.Log("CanWallJump: " + canWallJump);
		if(!onGround)
		{
			if (onWall && canWallJump && (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.Space)))
			{
				Debug.Log("Wall Jumped");
				yVelocity = jumpForce/jumpDecayRate;
				canWallJump = false;
				onWall = false;
			}
			else if (canAirJump && (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.Space)))
			{
				yVelocity = jumpForce/jumpDecayRate;
				canAirJump = false;
			}	
		}
		timer += Time.deltaTime;
		base.Update();
	}

	void OnControllerColliderHit(ControllerColliderHit col)
	{
		Vector3 hit = col.normal;
		float angle = Vector3.Angle (hit, Vector3.up);
		if(Mathf.Approximately(angle, 90) || Mathf.Approximately(angle, 270)) 
		{
			Debug.Log("Wall Collision");
			onWall = true;
			timer = 0;
		}
		if(timer > .1f)
		{
			onWall = false;
		}
		//else onWall = false;
//		if(Mathf.Approximately(angle, 90))
//		{
//			Debug.Log ("Left");
//		}
//		else if(Mathf.Approximately(angle, 270))
//		{
//			Debug.Log ("Right");
//		}
	}

//	void OnCollisionEnter(Collision col)
//	{
//		Debug.Log("Test");
//		Vector3 hit = col.contacts[0].normal;
//		Debug.Log(hit);
//		float angle = Vector3.Angle (hit, Vector3.up);
//		if(Mathf.Approximately(angle, 90))
//		{
//			Debug.Log ("Left");
//		}
//		else if(Mathf.Approximately(angle, 270))
//		{
//			Debug.Log ("Right");
//		}
//	}
//
//	void OnCollisionExit(Collision col)
//	{
//		//this.transform.gameObject.transform.gameObject.transform.gameObject.gameObject.gameObject.
//
//	}
}

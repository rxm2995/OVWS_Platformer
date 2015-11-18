using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class AcrobatPlayer : Player
{
	private bool canAirJump = true;
	private bool canWallJump = true;
	private bool onWall = false;
	private int wallDir = 0;
	private float timer;
	private Vector3 oldPos;

	public override void Start()
	{
		//First player in gets Acrobat, everyone after gets Strong.
		if(GameObject.FindGameObjectsWithTag("Player").Length > 1)
		{
			gameObject.GetComponent<StrongPlayer>().enabled = true;
			enabled = false;
		}
		base.Start ();
		//Debug.Log ("Player Array Length: " + GameObject.FindGameObjectsWithTag ("Player").Length + ", isLocalPlayer: " + isLocalPlayer.ToString());
	}
	
	// Update is called once per frame
	public override void Update ()
	{
		if(charControl.enabled)
		{
			//Can move under own free will

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
					persistentVelocity.y = jumpForce/jumpDecayRate;
					persistentVelocity.x = (1.4f * wallDir * jumpForce)/(jumpDecayRate * 2);
					transform.position = oldPos;
					canWallJump = false;
					onWall = false;
					wallDir = 0;
				}
				else if (canAirJump && (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.Space)))
				{
					Debug.Log("Air Jumped");
					persistentVelocity.y = jumpForce/jumpDecayRate;
					transform.position = oldPos;
					canAirJump = false;
				}	
			}
			timer += Time.deltaTime;
			oldPos = transform.position;
			base.Update();
		}
		else
		{
			//Being held
			persistentVelocity = (transform.position-oldPos)/(Time.deltaTime*5);
			//Debug.Log(persistentVelocity);
			//Throwing values based on the strong player sprinting, walking, or only jumping in place
			//Moving Right
			if(persistentVelocity.x > .5f)
			{
				//Sprinting
				if(persistentVelocity.x > 1.5f)
				{
					persistentVelocity.x = 5.0f;
					persistentVelocity.y = 2f;
				}
				//Walking
				else
				{
					persistentVelocity.x = 2.5f;
					persistentVelocity.y = 1f;
				}
			}
			//Moving Left
			else if(persistentVelocity.x < -.5f)
			{
				//Sprinting
				if (persistentVelocity.x < -1.5f)
				{
					persistentVelocity.x = -5.0f;
					persistentVelocity.y = 2f;
				}
				//Walking
				else
				{
					persistentVelocity.x = -2.5f;
					persistentVelocity.y = 1f;
				}
			}
			//Standing in one spot
			else
			{
				persistentVelocity.x = 0f;
				//Jumping
				if(persistentVelocity.y > 1.0f)
					persistentVelocity.y = 3.0f;
				else
					persistentVelocity.y = 0.0f;
			}
			oldPos = transform.position;
		}
	}

	void OnControllerColliderHit(ControllerColliderHit col)
	{
		Vector3 hit = col.normal;
		float angle = Vector3.Angle (hit, Vector3.up);
		if((angle > 85f && angle < 95f))
		{
			if(col.gameObject.transform.position.x < this.transform.position.x)
			{
				wallDir = 1;
			}
			else
			{
				wallDir = -1;
			}
			onWall = true;
			timer = 0;
		}
		//else if((angle > 265f && angle < 275f))
		//{
		//	wallDir = 1;
		//	onWall = true;
		//}
		if(timer > .1f)
		{
			wallDir = 0;
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
